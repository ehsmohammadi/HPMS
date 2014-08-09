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
    public class CalculationExceptionPersister : ICalculationExceptionPersister
    {
        private ICalculatorEngineFactory calculatorEngineFactory;
        private readonly IEventPublisher publisher;
        private ConcurrentQueue<CalculationExceptionReady> queue = new ConcurrentQueue<CalculationExceptionReady>();
        private AutoResetEvent trigger = new AutoResetEvent(false);
        private bool calcEnded = false;
        private bool pathCompleted = false;

        public CalculationExceptionPersister(ICalculatorEngineFactory calculatorEngineFactory,IEventPublisher publisher)
        {
            this.calculatorEngineFactory = calculatorEngineFactory;
            this.publisher = publisher;
            Task.Factory.StartNew(() =>
            {
                CalculationExceptionReady data;
                while (!calcEnded)
                {
                    while (queue.TryDequeue(out data))
                    {
                        var engine = calculatorEngineFactory.Create();
                        try
                        {
                            engine.AddCalculationException(data.CalculationId,data.EmployeeId,data.CalculationPathNo,data.Messages);
                        }
                        finally
                        {
                            calculatorEngineFactory.Release(engine);
                        }
                    }
                    trigger.WaitOne();
                }
                var x = 1;
            }
            );
        }

        public void Handle(CalculationExceptionReady eventData)
        {
            queue.Enqueue(eventData);
            trigger.Set();
        }

        public void Handle(CalculationCompleted eventData)
        {
            calcEnded = true;
            trigger.Set();
        }

       
    }
}
