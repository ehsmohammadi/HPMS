using MITD.Core;
using MITD.Core.RuleEngine.NH;
using MITD.Data.NH;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Service;
using MITD.PMS.Persistence.NH;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MITD.PMS.Test
{
    public class JobIndexPointCalculator : IJobIndexPointCalculator
    {
        private IEventPublisher publisher;
        private bool doStop = false;
        public JobIndexPointCalculator(IEventPublisher publisher)
        {
            this.publisher = publisher;
        }

        public void Start(Calculation calculation)
        {

            Task.Factory.StartNew(() =>
            {
                publisher.RegisterHandler(new JobIndexPointPersister());
                using (var transaction = new TransactionScope())
                using (var reuow = new NHUnitOfWork(RuleEngineSession.GetSession()))
                using (var uow = new NHUnitOfWork(PMSSession.GetSession()))
                {
                    var empRep = new EmployeeRepository(uow);
                    var rebps = new RuleBasedPolicyEngineService(new LocatorProvider("PMSDb"),publisher);
                    var policyRep = new MITD.PMS.Persistence.NH.PolicyRepository(uow,
                        new PolicyConfigurator(rebps));
                    var policy = policyRep.GetById(calculation.PolicyId);
                    var periodRep = new PeriodRepository(uow);
                    var period = periodRep.GetById(calculation.PeriodId);

                    var jiRep = new JobIndexRepository(uow);
                    var jpRep = new JobPositionRepository(uow);
                    var jipRep = new JobIndexPointRepository(uow);
                    var ji = jiRep.GetAllJobIndex(period.Id).First();
                    var jp = jpRep.GetJobPositions(period.Id).First();

                    var en = calculation.EmployeeIdList.Select(i => i.EmployeeNo).ToList();
                    IList<Employee> employees = empRep.Find(e => en.Contains(e.Id.EmployeeNo) && e.Id.PeriodId == calculation.PeriodId);
                    foreach (var employee in employees)
                    {
                        if (doStop) break;
                        //var indices = policy.CalculateFor(DateTime.Now, employee, period,calculation, 
                        //    new CalculationDataProvider(empRep),publisher, );
                        //publisher.Publish(new JobIndexPointsReady(indices));
                    }

                    reuow.Commit();
                    uow.Commit();
                    transaction.Complete();
                }
            });            
        }


        public void Pause(Calculation calculation)
        {
            doStop = true;
        }

        public void Stop(Calculation calculation)
        {
            doStop = true;
        }

        public bool HasEmployeeCalculationFailed { get; private set; }

        public CalculatorSession CalculatorSession { get; private set; }

        public IReadOnlyList<string> Messages
        {
            get { throw new NotImplementedException(); }
        }

        public CalculationProgress Progress
        {
            get { throw new NotImplementedException(); }
        }


        public void Resume(Calculation calculation)
        {
            throw new NotImplementedException();
        }

        public void Handle(PathPersisteCompleted eventData)
        {
            throw new NotImplementedException();
        }
    }
}
