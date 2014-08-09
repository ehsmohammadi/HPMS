using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public abstract class PeriodState : Enumeration, IValueObject<PeriodState>, IPeriodState
    {

        public static readonly PeriodState Init = new PeriodInitState();
        public static readonly PeriodState BasicDataCopying = new PeriodBasicDataCopying();
        public static readonly PeriodState InitializingForInquiry = new PeriodInitializingForInquiryState();
        public static readonly PeriodState InitializingForInquiryCompleted = new PeriodInitializeInquiryCompletedState();
        public static readonly PeriodState InquiryStarted = new PeriodInquiryStartedState();
        public static readonly PeriodState InquiryCompleted = new PeriodInquiryCompletedState();    
        public static readonly PeriodState ClaimingStarted = new PeriodClaimingStartedState();
        public static readonly PeriodState ClaimingFinished = new PeriodClaimingFinishedState();
        public static readonly PeriodState Closed = new PeriodClosedState();

        private readonly string description;
        public virtual string Description
        {
            get { return description; }
        }

        protected PeriodState(string value, string name)
            : base(value, name)
        {

        }

        public PeriodState(string value, string displayName, string description)
            : base(value, displayName)
        {
            this.description = description;
        }

        public bool SameValueAs(PeriodState other)
        {
            return Equals(other);
        }
        public static bool operator ==(PeriodState left, PeriodState right)
        {
            return object.Equals(left, right);
        }
        public static bool operator !=(PeriodState left, PeriodState right)
        {
            return !(left == right);
        }

        public static explicit operator int(PeriodState x)
        {
            if (x == null)
            {
                throw new InvalidCastException();

            }

            return Convert.ToInt32(x.Value);

        }

       

        internal virtual void InitializeInquiry(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period",DisplayName, "InitializeInquiry");
        }

        internal virtual void CompleteIntializingForInquiry(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CompleteIntializingForInquiry");
        }

        internal virtual void CompleteCopyingBasicData(Period period, IPeriodManagerService periodManagerService, PeriodState preState)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CompleteCopyingBasicData");
        }


        internal virtual void CopyPeriodBasicData(Period currentPeriod,Period sourcePeriods, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CopyPeriodBasicData");
        }

        internal virtual void StartInquiry(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "StartInquiry");
        }
        internal virtual void CompleteInquiry(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CompleteInquiry");
        }

        internal virtual void StartClaiming(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "StartClaiming");
        }

        internal virtual void FinishClaiming(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "FinishClaiming");
        }

        internal virtual void Close(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "Close");
        }
        internal virtual InquiryInitializingProgress GetInitializeInquiryProgress(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "GetInitializeInquiryProgress");
        }

        internal virtual BasicDataCopyingProgress GetCopyingStateProgress(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "GetCopyingStateProgress");
        }

        internal virtual void RollBack(Period period, IPeriodManagerService periodManagerService)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "RollBack");
        }

        internal virtual void ChangeActiveStatus(Period period,IPeriodManagerService periodManagerService,bool activeStatus)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "ChangeActiveStatus");
        }

        internal virtual void CheckAssigningUnit()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckAssigningUnit");
        }

        internal virtual void CheckRemovingUnit()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckRemovingUnit");
        }

        internal virtual void CheckAssigningJobIndex()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckAssigningJobIndex");
        }

        internal virtual void CheckModifingJobIndex()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckModifingJobIndex");
        }

        internal virtual void CheckAssigningJob()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckAssigningJob");
        }

        internal virtual void CheckModifyingJobCustomFields()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckModifyingJobCustomFields");
        }

        internal virtual void CheckModifyingJobIndices(Job job)
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckModifyingJobIndices");
        }



        internal virtual void CheckAssigningJobPosition()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckAssigningJobPosition");
        }

        internal virtual void CheckModifyingJobPositionInquirers()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckModifyingJobPositionInquirers");
        }

        internal virtual void CheckCreatingEmployee()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckCreatingEmployee");
        }

        internal virtual void CheckModifyingEmployeeCustomFieldsAndValues()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckModifyingEmployeeCustomFieldsAndValues");
        }

        internal virtual void CheckModifyingEmployeeJobPositions()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckModifyingEmployeeJobPositions");
        } 

        internal virtual void CheckCreatingCalculation()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckCreatingCalculation");
        }

        internal virtual void CheckShowingInquirySubject()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckShowingInquirySubject");
        }

        internal virtual void CheckSettingInquiryJobIndexPointValue()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckSettingInquiryJobIndexPointValue");
        }
        internal virtual void CheckAddClaim()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckAddClaim");
        }
        internal virtual void CheckReplyClaim()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckReplyClaim");
        }

        internal virtual void CheckCancelClaim()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckCancelClaim");
        }

        internal virtual void  CheckChangeCalculationDeterministicStatus()
        {
            throw new PeriodInvalidStateOperationException("Period", DisplayName, "CheckChangeCalculationDeterministicStatus");
        }

        

       
    }
}
