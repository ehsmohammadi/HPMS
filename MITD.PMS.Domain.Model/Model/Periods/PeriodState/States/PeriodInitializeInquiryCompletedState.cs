﻿using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class PeriodInitializeInquiryCompletedState : PeriodState 
    {
        public PeriodInitializeInquiryCompletedState()
            : base("4", "PeriodInitializeInquiryCompletedState")
        {

        }

        internal override void StartInquiry(Period period, IPeriodManagerService periodManagerService)
        {
                period.State = new PeriodInquiryStartedState();
        }

        internal override InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            return periodManagerService.GetCompletedInitializeInquiryProgress(period);
        }

        internal override void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            periodManagerService.DeleteAllInquiryConfigurations(period);
            period.State = new PeriodInitState();
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
        internal override void CheckAssigningUnitIndex()
        {
        }
        internal override void CheckModifingJobIndex()
        {
        }
        internal override void CheckModifingUnitIndex()
        {
        }
        internal override void CheckAssigningJob()
        {
        }
        //internal override void CheckModifyingJobIndices(Job job)
        //{
        //    throw new PMSOperationException("ویرایش شاخص های شغل باعث تغییر در وضعیت نظر سنجی می شود");
        //}
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

    }
}
