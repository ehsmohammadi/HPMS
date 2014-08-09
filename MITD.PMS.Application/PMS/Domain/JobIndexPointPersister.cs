using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.JobIndexPoints;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace MITD.PMS.Application
{
    public class JobIndexPointPersister : IJobIndexPointPersister
    {
        private ICalculatorEngineFactory calculatorEngineFactory;
        private readonly IEventPublisher publisher;
        private ConcurrentQueue<JobIndexPointsReady> queue = new ConcurrentQueue<JobIndexPointsReady>();
        private AutoResetEvent trigger = new AutoResetEvent(false);
        private bool calcEnded = false;
        private bool pathCompleted = false;
        private bool hasExceptionInPersist = false;

        public JobIndexPointPersister(ICalculatorEngineFactory calculatorEngineFactory,IEventPublisher publisher)
        {
            this.calculatorEngineFactory = calculatorEngineFactory;
            this.publisher = publisher;
            Task.Factory.StartNew(() =>
            {
                JobIndexPointsReady data;
                while (!calcEnded)
                {
                    while (queue.TryDequeue(out data))
                    {
                        var engine = calculatorEngineFactory.Create();
                        try
                        {
                            if (data.PointsHolder.EmployeePointsForAdd != null)
                                engine.AddEmployeePoints(data.PointsHolder.EmployeePointsForAdd);
                            if (data.PointsHolder.EmployeePointsForUpdate != null)
                                engine.UpdateEmployeePoints(data.PointsHolder.EmployeePointsForUpdate);
                        }
                        catch (Exception ex)
                        {
                            hasExceptionInPersist = true;
                            publisher.Publish(new CalculationExceptionReady(data.CalculationId, data.EmployeeId, data.CalculationPathNo, ex));
                        }
                        finally
                        {
                            calculatorEngineFactory.Release(engine);
                        }
                    }
                    if (pathCompleted)
                    {
                        publisher.Publish(new PathPersisteCompleted(hasExceptionInPersist));
                        pathCompleted = false;

                    }
                       
                    trigger.WaitOne();
                }
            }
            );
        }

        public void Handle(JobIndexPointsReady eventData)
        {
            queue.Enqueue(eventData);
            trigger.Set();
        }

        public void Handle(CalculationCompleted eventData)
        {
            calcEnded = true;
            trigger.Set();
        }

        public void Handle(CalculationForPathCompleted eventData)
        {
            pathCompleted = true;
            trigger.Set();
        }
    }
}
