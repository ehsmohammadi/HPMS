using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Claims;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Exceptions;


namespace MITD.PMS.Domain.Service
{
    public class PeriodManagerService : IPeriodManagerService
    {
        private readonly IPeriodRepository periodRep;
        private readonly IInquiryConfiguratorService inquiryConfiguratorService;
        private readonly IPeriodBasicDataCopierService periodCopierService;
        private readonly IEventPublisher publisher;
        private readonly ICalculationRepository calcRep;
        private readonly IJobIndexPointRepository jobIndexPointRep;
        private readonly IJobPositionRepository jobPositionRep;
        private readonly IInquiryJobIndexPointRepository inquiryJobIndexPointRep;
        private readonly IClaimRepository claimRep;
        private readonly IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep;

        public PeriodManagerService(IPeriodRepository periodRep, IInquiryConfiguratorService inquiryConfiguratorService,
            IPeriodBasicDataCopierService periodCopierService, IEventPublisher publisher
            , ICalculationRepository calcRep, IJobIndexPointRepository jobIndexPointRep, IJobPositionRepository jobPositionRep, IInquiryJobIndexPointRepository inquiryJobIndexPointRep
            , IClaimRepository claimRep, IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep
            )
        {
            this.periodRep = periodRep;
            this.inquiryConfiguratorService = inquiryConfiguratorService;
            this.periodCopierService = periodCopierService;
            this.publisher = publisher;
            this.calcRep = calcRep;
            this.jobIndexPointRep = jobIndexPointRep;
            this.jobPositionRep = jobPositionRep;
            this.inquiryJobIndexPointRep = inquiryJobIndexPointRep;
            this.claimRep = claimRep;
            this.inquiryUnitIndexPointRep = inquiryUnitIndexPointRep;
        }

        public bool CanActivate(Period period)
        {
            var periods = periodRep.GetAll();
            return periods.Where(itm => !itm.Id.Equals(period.Id)).All(itm => !itm.Active);
        }

        public void InitializeInquiry(Period period)
        {
            inquiryConfiguratorService.Configure(period, publisher);

        }

        public InquiryInitializingProgress GetInitializeInquiryProgress(Period period)
        {
            if (inquiryConfiguratorService.IsRunning)
                return inquiryConfiguratorService.InquiryInitializingProgress;
            inquiryConfiguratorService.Configure(period, publisher);
            return inquiryConfiguratorService.InquiryInitializingProgress;
        }

        public InquiryInitializingProgress GetCompletedInitializeInquiryProgress(Period period)
        {
            var inquiryInitializingProgress = new InquiryInitializingProgress { State = period.State };
            var totalcount = inquiryConfiguratorService.GetNumberOfConfiguredJobPosition(period) +
                             inquiryConfiguratorService.GetNumberOfConfiguredUnit(period);
            inquiryInitializingProgress.Messages.Add("شروع آماده سازی دوره");
            inquiryInitializingProgress.Messages.Add("تعداد " + totalcount + " پست و واحد برای پیکر بندی نظر سنجی آماده می باشد");
            inquiryInitializingProgress.Messages.Add("اتمام آماده سازی دوره برای نظر سنجی");
            inquiryInitializingProgress.SetProgress(totalcount, totalcount);
            return inquiryInitializingProgress;
        }

        public void CopyBasicData(Period currentPeriod, Period sourcePeriod)
        {
            periodCopierService.CopyBasicData(currentPeriod, sourcePeriod, publisher);
        }

        public void DeleteBasicData(Period period)
        {
            periodRep.DeleteBasicData(period.Id);
        }

        public BasicDataCopyingProgress GetCopyingStateProgress(Period period)
        {
            if (periodCopierService.IsCopying)
                return periodCopierService.BasicDataCopyingProgress;
            return GetBasicDataCopyStatus(period);

        }

        public BasicDataCopyingProgress GetBasicDataCopyStatus(Period period)
        {
            if (periodCopierService.LastPeriod.Key.Id == period.Id.Id)
            {
                var progress = new BasicDataCopyingProgress
                {

                    State = period.State
                };
                progress.SetProgress(100, 100);
                foreach (var message in periodCopierService.LastPeriod.Value)
                {
                    progress.SetMessage(message);
                }
                return progress;
            }

            return new BasicDataCopyingProgress { State = period.State };
        }

        public Period GetCurrentPeriod()
        {
            var res = periodRep.GetBy(c => c.Active);
            if (res == null)
                throw new PeriodException((int)ApiExceptionCode.DoesNotExistAnyActivePeriod, ApiExceptionCode.DoesNotExistAnyActivePeriod.DisplayName);
            return res;
        }

        public bool CanCompleteInquiry(Period period)
        {

            var allJobIndexPointHaveValue = inquiryJobIndexPointRep.IsAllInquiryJobIndexPointsHasValue(period);
            var allUnitIndexPointHaveValue = inquiryUnitIndexPointRep.IsAllInquiryUnitIndexPointsHasValue(period);
            return allUnitIndexPointHaveValue && allJobIndexPointHaveValue;
        }

        public void CheckUpdatingJobIndex(JobIndex jobIndex)
        {
            var period = periodRep.GetById(jobIndex.PeriodId);
            period.CheckModifingJobIndex();
        }

        public void CheckModifyingJobCustomFields(Job job)
        {
            var period = periodRep.GetById(job.Id.PeriodId);
            period.CheckModifyingJobCustomFields();
        }

        public void CheckModifyingJobIndices(Job job)
        {
            var period = periodRep.GetById(job.Id.PeriodId);
            period.CheckModifyingJobIndices(job);
        }

        public void CheckModifyingJobPositionInquirers(Employee inquirySubject)
        {
            var period = periodRep.GetById(inquirySubject.Id.PeriodId);
            period.CheckModifyingJobPositionInquirers();
        }

        public void CheckModifyingEmployeeCustomFieldsAndValues(Employee employee)
        {
            var period = periodRep.GetById(employee.Id.PeriodId);
            period.CheckModifyingEmployeeCustomFieldsAndValues();
        }

        public void CheckModifyingEmployeeJobPositions(Employee employee)
        {
            var period = periodRep.GetById(employee.Id.PeriodId);
            period.CheckModifyingEmployeeJobPositions();
        }

        public void CheckShowingInquirySubject(Employee inquirer)
        {
            if (inquirer == null)
                return;
            var period = periodRep.GetById(inquirer.Id.PeriodId);
            //todo bz
            // period.CheckShowingInquirySubject();
        }

        public void CheckShowingInquiryJobIndexPoint(JobPosition jobPosition)
        {
            var period = periodRep.GetById(jobPosition.Id.PeriodId);

            //todo bz
            //  period.CheckShowingInquirySubject();
        }

        public void CheckSettingInquiryJobIndexPointValueValue(InquiryJobIndexPoint inquiryJobIndexPoint)
        {
            var period = periodRep.GetById(inquiryJobIndexPoint.ConfigurationItemId.InquirerId.PeriodId);
            period.CheckSettingInquiryJobIndexPointValueValue();
        }



        public bool AllowRollBackToInquiryCompletedState(Period period)
        {
            var periods = periodRep.GetAll();
            return periods.Where(itm => !itm.Id.Equals(period.Id)).All(itm => !itm.Active);
        }

        public void DeleteAllCalculations(Period period)
        {
            calcRep.DeleteAllCalculation(period);
        }

        public void ResetAllInquiryPoints(Period period)
        {
            jobIndexPointRep.ResetAllInquiryPoints(period);
        }

        public void DeleteAllInquiryConfigurations(Period period)
        {
            jobPositionRep.DeleteAllJobPositionConfigurations(period);
        }

        public void ChangeActiveStatus(Period period, bool activeStatus)
        {
            if (activeStatus)
                PeriodActivatorService.Activate(periodRep, period);
            else
                period.DeActive();
        }

        public void DeleteAllCalims(Period period)
        {
            claimRep.DeleteAll(period);
        }

        public bool HasDeterministicCalculation(Period period)
        {
            return calcRep.HasDeterministicCalculation(period);
        }

        public bool HasOpenClaim(Period period)
        {
            return claimRep.HasOpenClaim(period);
        }


        public void CheckUpdatingUnitIndex(UnitIndex unitIndex)
        {

            var period = periodRep.GetById(unitIndex.PeriodId);
            period.CheckModifingJobIndex();

        }


        public void CheckModifyingUnitCustomFields(Unit unit)
        {
            var period = periodRep.GetById(unit.Id.PeriodId);
            period.CheckModifyingUnitCustomFields();
        }

        public void CheckModifyingUnitIndices(Unit unit)
        {
            var period = periodRep.GetById(unit.Id.PeriodId);
            period.CheckModifyingUnitIndices(unit);
        }


        public void CheckSettingInquiryUnitIndexPointValueValue(InquiryUnitIndexPoint inquiryUnitIndexPoint)
        {
            var period = periodRep.GetById(inquiryUnitIndexPoint.ConfigurationItemId.InquirerId.PeriodId);
            //todo bz
            // period.CheckSettingInquiryJobIndexPointValueValue();
        }
    }
}
