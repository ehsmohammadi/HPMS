using MITD.Core;
using MITD.PMS.Common;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodInquiryStartedState : PeriodState
    {
        public PeriodInquiryStartedState()
            : base("5", "PeriodInquiryStartedState")
        {

        }

        internal override void CompleteInquiry(Period period, IPeriodManagerService periodManagerService)
        {
            if (periodManagerService.CanCompleteInquiry(period))
            {
                period.State = new PeriodInquiryCompletedState();
            }
            else
                throw new PeriodException((int)ApiExceptionCode.CouldNotCompleteInquiryWithNotFilledInquiryForms, ApiExceptionCode.CouldNotCompleteInquiryWithNotFilledInquiryForms.DisplayName);
        }

        internal override InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetCompletedInitializeInquiryProgress(period);
        }

        internal override void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            periodManagerService.ResetAllInquiryPoints(period);
            period.State = new PeriodInitializeInquiryCompletedState();
        }

        internal override void CheckAssigningUnit()
        {
        }

        internal override void CheckRemovingUnit()
        {
        }
        internal override void CheckAssigningJobIndex()
        {
        }
        internal override void CheckModifingJobIndex()
        {
        }
        internal override void CheckAssigningJob()
        {
        }
        internal override void CheckModifyingJobIndices(Job job)
        {
            throw new PeriodException((int)ApiExceptionCode.CouldNotModifyJobIndicesInInquiryStartedState, ApiExceptionCode.CouldNotModifyJobIndicesInInquiryStartedState.DisplayName);
        }
        internal override void CheckModifyingJobCustomFields()
        {
        }
        internal override void CheckAssigningJobPosition()
        {
        }

        internal override void CheckCreatingEmployee()
        {
        }
        //internal override void CheckModifyingJobPositionInquirers()
        //{
        //    throw new PMSOperationException("ویرایش نظردهنده ها باعث تغییر در وضعیت نظر سنجی می شود");
        //}

        //internal override void CheckModifyingEmployeeJobPositions()
        //{
        //    throw new PMSOperationException("خطا،  ویرایش پست های سازمانی کارمند در این وضعیت دوره امکان پذیر نمی باشد زیرا تغییر در پست سازمانی کارمند باعث تغییر در وضعیت نظر سنجی می شود");
        //} 

        internal override void CheckModifyingEmployeeCustomFieldsAndValues()
        {
        }

        internal override void CheckShowingInquirySubject()
        {
        }
        internal override void CheckSettingInquiryJobIndexPointValue()
        {
        }



    }
}
