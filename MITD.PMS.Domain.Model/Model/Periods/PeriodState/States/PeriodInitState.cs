using MITD.Core;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodInitState : PeriodState
    {
        public PeriodInitState()
            : base("1", "PeriodInitState")
        {

        }

        internal override void InitializeInquiry(Period period, IPeriodManagerService periodManagerService)
        {
            if (period.Active)
            {
                periodManagerService.InitializeInquiry(period);
                period.State = new PeriodInitializingForInquiryState();
            }
            else
                throw new PeriodException((int)ApiExceptionCode.CouldNotInitializeInquiryForInactivePeriod, ApiExceptionCode.CouldNotInitializeInquiryForInactivePeriod.DisplayName);

        }

        internal override InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return new InquiryInitializingProgress { State = new PeriodInitState() };
        }

        internal override BasicDataCopyingProgress GetCopyingStateProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetBasicDataCopyStatus(period);
        }

        internal override void CopyPeriodBasicData(Period currentPeriod, Period sourcePeriod, IPeriodManagerService periodManagerService)
        {
            periodManagerService.CopyBasicData(currentPeriod, sourcePeriod);
            currentPeriod.State = new PeriodBasicDataCopying();
        }

        internal override void ChangeActiveStatus(Period period,IPeriodManagerService periodManagerService,bool activeStatus)
        {
            periodManagerService.ChangeActiveStatus(period, activeStatus);
        }

        internal override void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            periodManagerService.DeleteBasicData(period);
        }

        internal override void CheckAssigningUnit()
        {
        }
        internal override void CheckRemovingUnit()
        {
        }
        internal override void CheckAssigningJob()
        {
        }
        internal override void CheckModifyingJobCustomFields()
        {
        }
        internal override void CheckModifyingJobIndices(Job job)
        {
        }
        internal override void CheckAssigningUnitIndex()
        {
        }
        internal override void CheckAssigningJobIndex()
        {
        }
        internal override void CheckModifingJobIndex()
        {
        }
        internal override void CheckModifingUnitIndex()
        {
        }
        internal override void CheckAssigningJobPosition()
        {
        }
        internal override void CheckCreatingEmployee()
        {
        }
        internal override void CheckModifyingEmployeeCustomFieldsAndValues()
        {
        }
        internal override void CheckModifyingEmployeeJobPositions()
        {
        }
        internal override void CheckModifyingJobPositionInquirers()
        {
        }
    }
}
