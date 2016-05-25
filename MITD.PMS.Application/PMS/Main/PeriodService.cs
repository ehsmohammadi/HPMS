using System;
using System.Transactions;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Application
{
    public class PeriodService : IPeriodService
    {
        #region Fields
        private readonly IPeriodRepository periodRep;
        private readonly IPeriodManagerService periodManagerService;
        private readonly IJobIndexServiceFactory jobIndexServiceFactory;
        private readonly IUnitIndexServiceFactory unitIndexServiceFactory;
        private readonly IUnitIndexRepository unitIndexRepository;
        private readonly IJobIndexRepository jobIndexRepository;
        #endregion

        #region Constructors
        public PeriodService(
            IPeriodRepository periodRep,
            IUnitIndexRepository unitIndexRepository,
            IJobIndexRepository jobIndexRepository,
            IPeriodManagerService periodManagerService,
            IJobIndexServiceFactory jobIndexServiceFactory,
            IUnitIndexServiceFactory unitIndexServiceFactory
            )
        {
            this.periodRep = periodRep;
            this.periodManagerService = periodManagerService;
            this.jobIndexServiceFactory = jobIndexServiceFactory;
            this.unitIndexServiceFactory = unitIndexServiceFactory;
            this.unitIndexRepository = unitIndexRepository;
            this.jobIndexRepository = jobIndexRepository;
        }
        #endregion

        #region Period Operation
        public void Delete(PeriodId periodId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var period = periodRep.GetById(periodId);
                    periodRep.Delete(period);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = periodRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }

        }

        public Period AddPeriod(string name, DateTime startDate, DateTime endDate, decimal maxFinalPoint)
        {
            using (var scope = new TransactionScope())
            {
                var id = periodRep.GetNextId();
                var period = new Period(new PeriodId(id), name, startDate, endDate,maxFinalPoint);
                periodRep.Add(period);
                scope.Complete();
                return period;
            }
        }

        public Period UpdatePeriod(PeriodId periodId, string name, DateTime startDate, DateTime endDate, decimal maxFinalPoint)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var period = periodRep.GetById(periodId);
                    period.Update(name, startDate, endDate,maxFinalPoint);
                    scope.Complete();
                    return period;
                }
            }
            catch (Exception exp)
            {
                var res = periodRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Period GetCurrentPeriod()
        {
            using (var scope = new TransactionScope())
            {
                var res = periodManagerService.GetCurrentPeriod();
                scope.Complete();

                return res;
            }
        }

        public void ChangePeriodActiveStatus(PeriodId periodId, bool active)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                setInitData(period);
                period.ChangeActiveStatus(periodManagerService, active);

                scope.Complete();

            }

        }

        private void setInitData(Period period)
        {
            //todo:Damn fu.... very bad code for this section 
            var behaviaralId = jobIndexRepository.GetNextId();
            var behaviaralJobIndexGroup = new JobIndexGroup(behaviaralId, period, null, "شاخص های رفتاری", "BehaviouralGroup");
            jobIndexRepository.Add(behaviaralJobIndexGroup);

            var performanceId = jobIndexRepository.GetNextId();
            var performanceJobIndexGroup = new JobIndexGroup(performanceId, period, null, "شاخص های عملکردی", "PerformanceGroup");
            jobIndexRepository.Add(performanceJobIndexGroup);

            var uniGroupId = unitIndexRepository.GetNextId();
            var unitIndexGroup = new UnitIndexGroup(uniGroupId, period, null, "گروه شاخص های سازمانی", "OrganizationUnitGroup");
            unitIndexRepository.Add(unitIndexGroup);
        }

        public void Close(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.Close(periodManagerService);
                scope.Complete();
            }
        }

        public void RollBack(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.RollBack(periodManagerService);
                scope.Complete();
            }
        }
        #endregion

        #region Period Inquiry Operation
        public InquiryInitializingProgress GetIntializeInquiryState(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                InquiryInitializingProgress initState = period.GetInitializeInquiryProgress(periodManagerService);
                scope.Complete();
                return initState;
            }
        }

        public InquiryInitializingProgress GetPeriodInitializeInquiryState(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                var res = period.GetInitializeInquiryProgress(periodManagerService);
                scope.Complete();
                return res;
            }
        }

        public void InitializeInquiry(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.InitializeInquiry(periodManagerService);
                scope.Complete();
            }
        }

        public void CompleteInitializeInquiry(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.CompleteInitializeInquiry(periodManagerService);
                scope.Complete();
            }
        }

        public void StartInquiry(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.StartInquiry(periodManagerService);
                scope.Complete();
            }
        }

        public void CompleteInquiry(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.CompleteInquiry(periodManagerService);
                scope.Complete();
            }
        } 
        #endregion

        #region Confirmation Operation
        public void StartConfirmation(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.StartConfirmation(periodManagerService);
                scope.Complete();
            }
        }

        public void Confirm(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.Confirm(periodManagerService);
                scope.Complete();
            }
        } 
        #endregion

        #region Not Use
        public void CopyBasicData(long sourcePeriodId, long destionationPeriodId)
        {
            using (var scope = new TransactionScope())
            {
                var sourcePeriod = periodRep.GetById(new PeriodId(sourcePeriodId));
                var desPeriod = periodRep.GetById(new PeriodId(destionationPeriodId));
                desPeriod.CopyPeriodBasicData(sourcePeriod, periodManagerService);
                scope.Complete();
            }
        }

        public void CompleteCopyingBasicData(PeriodId periodId, PeriodState preState)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.CompleteCopyingBasicData(periodManagerService, preState);
                scope.Complete();
            }
        }

        public BasicDataCopyingProgress GetPeriodCopyingStateProgress(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                var res = period.GetCopyingStateProgress(periodManagerService);
                scope.Complete();
                return res;
            }
        }

        public void StartClaiming(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.StartClaiming(periodManagerService);
                scope.Complete();
            }
        }

        public void FinishClaiming(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.FinishClaiming(periodManagerService);
                scope.Complete();
            }
        } 
        #endregion

    }
}
