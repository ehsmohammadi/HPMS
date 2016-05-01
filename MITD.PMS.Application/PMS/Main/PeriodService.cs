using System;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using System.Transactions;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Exceptions;


namespace MITD.PMS.Application
{
    public class PeriodService : IPeriodService
    {
        private readonly IPeriodRepository periodRep;
        private readonly IPeriodManagerService periodManagerService;
        private readonly IJobIndexServiceFactory jobIndexServiceFactory;
        private readonly IUnitIndexServiceFactory unitIndexServiceFactory;
        private readonly IUnitIndexRepository unitIndexRepository;
        private readonly IJobIndexRepository jobIndexRepository;


        public PeriodService(IPeriodRepository periodRep, IPeriodManagerService periodManagerService,
            IJobIndexServiceFactory jobIndexServiceFactory, IUnitIndexServiceFactory unitIndexServiceFactory, IUnitIndexRepository unitIndexRepository, IJobIndexRepository jobIndexRepository)
        {
            this.periodRep = periodRep;
            this.periodManagerService = periodManagerService;
            this.jobIndexServiceFactory = jobIndexServiceFactory;
            this.unitIndexServiceFactory = unitIndexServiceFactory;
            this.unitIndexRepository = unitIndexRepository;
            this.jobIndexRepository = jobIndexRepository;
        }

        private void setInitData(Period period)
        {
            //var unitIndexServiceManger = unitIndexServiceFactory.Create();
            //try
            //{
            //    var unitIndexService = unitIndexServiceManger.GetService();
            //    unitIndexService.AddUnitIndexGroup(period.Id, null, "گروه شاخص های سازمانی", "OrganizationUnitGroup");
            //}
            //finally
            //{
            //    unitIndexServiceFactory.Release(unitIndexServiceManger);
            //}

            //todo: very bad code for this section 
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

        public Period AddPeriod(string name, DateTime startDate, DateTime endDate)
        {
            using (var scope = new TransactionScope())
            {
                var id = periodRep.GetNextId();
                var period = new Period(new PeriodId(id), name, startDate, endDate);
                periodRep.Add(period);
                scope.Complete();
                return period;
            }
        }

        public Period UpdatePeriod(PeriodId periodId, string name, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var period = periodRep.GetById(periodId);
                    period.Update(name, startDate, endDate);
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

        //public void Activate(PeriodId periodId)
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        var period = periodRep.GetById(periodId);
        //        period.Activate(periodManagerService);
        //        scope.Complete();
        //    }
        //}

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

        public void Close(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.Close(periodManagerService);
                scope.Complete();
            }
        }

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

        public void RollBack(PeriodId periodId)
        {
            using (var scope = new TransactionScope())
            {
                var period = periodRep.GetById(periodId);
                period.RollBack(periodManagerService);
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
    }
}
