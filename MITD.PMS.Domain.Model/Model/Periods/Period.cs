using System;
using System.Xml.Serialization;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.Periods
{
    public class Period : IEntity<Period>
    {
        #region Fields
        private readonly byte[] rowVersion;
        #endregion

        #region Properties
        private readonly PeriodId id;
        public virtual PeriodId Id
        {
            get { return id; }
        }

        private string name;
        public virtual string Name { get { return name; } }

        private readonly string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }

        private DateTime startDate;
        public virtual DateTime StartDate { get { return startDate; } }

        private DateTime endDate;
        public virtual DateTime EndDate { get { return endDate; } }

        private bool active;
        public virtual bool Active { get { return active; } }

        private PeriodState state;
        public virtual PeriodState State
        {
            get { return state; }

            //protected internal set { state = value; }
            set { state = value; }
        }

        #endregion

        protected Period()
        {

        }

        public Period(PeriodId periodId, string name, DateTime startDate, DateTime endDate)
        {
            if (periodId == null)
                throw new ArgumentNullException("periodId");
            this.id = periodId;
            if (string.IsNullOrWhiteSpace(name))
                throw new PeriodArgumentException("Period","Name");
            this.name = name;

            if (startDate == null)
                throw new PeriodArgumentException("Period","StartDate");
            this.startDate = startDate;

            if (endDate == null)
                throw new PeriodArgumentException("Period","EndDate");
            this.endDate = endDate;
            state = new PeriodInitState();
            active = false;
        }

        #region Public Method

        public virtual void Update(string name, DateTime startDate, DateTime endDate)
        {
            this.name = name;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        protected internal virtual void Activate()
        {
            active = true;
        }
        public virtual void DeActive()
        {
            active = false;
        }

        public virtual void CopyPeriodBasicData(Period sourcePeriod, IPeriodManagerService periodManagerService)
        {
            State.CopyPeriodBasicData(this, sourcePeriod, periodManagerService);
        }


        public virtual void InitializeInquiry(IPeriodManagerService periodManagerService)
        {
            State.InitializeInquiry(this, periodManagerService);
        }

        public virtual void StartInquiry(IPeriodManagerService periodManagerService)
        {
            State.StartInquiry(this, periodManagerService);
        }

        public virtual void CompleteInquiry(IPeriodManagerService periodManagerService)
        {
            State.CompleteInquiry(this, periodManagerService);
        }

        public virtual void StartClaiming(IPeriodManagerService periodManagerService)
        {
            State.StartClaiming(this, periodManagerService);
        }

        public virtual void FinishClaiming(IPeriodManagerService periodManagerService)
        {
            State.FinishClaiming(this,periodManagerService);
        }

        public virtual void Close(IPeriodManagerService periodManagerService)
        {
            State.Close(this, periodManagerService);
        }

        public virtual InquiryInitializingProgress GetInitializeInquiryProgress(IPeriodManagerService periodManagerService)
        {
            return State.GetInitializeInquiryProgress(this, periodManagerService);
        }

        public virtual void CompleteInitializeInquiry(IPeriodManagerService periodManagerService)
        {
            State.CompleteIntializingForInquiry(this, periodManagerService);
        }

        public virtual void CompleteCopyingBasicData(IPeriodManagerService periodManagerService, PeriodState preState)
        {
            State.CompleteCopyingBasicData(this, periodManagerService, preState);
        }

        public virtual BasicDataCopyingProgress GetCopyingStateProgress(IPeriodManagerService periodManagerService)
        {
            return State.GetCopyingStateProgress(this, periodManagerService);
        }

        public virtual void CheckAssigningUnit()
        {
            State.CheckAssigningUnit();
        }

        public virtual void CheckRemovingUnit()
        {
            State.CheckRemovingUnit();
        }



        #endregion




        #region IEntity Member
        public virtual bool SameIdentityAs(Period other)
        {
            return (other != null) && Id.Equals(other.Id);
        }
        #endregion

        #region Override Object
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var period = (Period)obj;
            return SameIdentityAs(period);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return Id.ToString();
        }
        #endregion


        public virtual void CheckAssigningUnitIndex()
        {
            State.CheckAssigningUnitIndex();
        }

        public virtual void CheckModifingUnitIndex()
        {
            State.CheckModifingUnitIndex();
        }

        
        public virtual void CheckAssigningJobIndex()
        {
            State.CheckAssigningJobIndex();
        }

        public virtual void CheckModifingJobIndex()
        {
            State.CheckModifingJobIndex();
        }

        public virtual void CheckAssigningJob()
        {
            State.CheckAssigningJob();
        }

        public virtual void CheckModifyingJobCustomFields()
        {
            State.CheckModifyingJobCustomFields();
        }

        public virtual void CheckModifyingUnitCustomFields()
        {
            State.CheckModifyingJobCustomFields();
        }

        public virtual void CheckModifyingUnitIndices(Unit unit)
        {
            State.CheckModifyingUnitIndices(unit);
        }

        public virtual void CheckModifyingJobIndices(Job job)
        {
            State.CheckModifyingJobIndices(job);
        }

        public virtual void CheckAssigningJobPosition()
        {
            State.CheckAssigningJobPosition();
        }

        public virtual void CheckModifyingJobPositionInquirers()
        {
            State.CheckModifyingJobPositionInquirers();
        }

        public virtual void CheckCreatingEmployee()
        {
            State.CheckCreatingEmployee();
        }

        public virtual void CheckModifyingEmployeeCustomFieldsAndValues()
        {
            State.CheckModifyingEmployeeCustomFieldsAndValues();
        }

        public virtual void CheckModifyingEmployeeJobPositions()
        {
            State.CheckModifyingEmployeeJobPositions();
        }

        public virtual void CheckCreatingCalculation()
        {
            State.CheckCreatingCalculation();
        }

        public virtual void CheckShowingInquirySubject()
        {
            State.CheckShowingInquirySubject();
        }

        public virtual void CheckSettingInquiryJobIndexPointValueValue()
        {
            State.CheckSettingInquiryJobIndexPointValue();
        }

        public virtual void RollBack(IPeriodManagerService periodManagerService)
        {
            State.RollBack(this, periodManagerService);
        }

        public virtual void ChangeActiveStatus(IPeriodManagerService periodManagerService,bool activeStatus)
        {
            State.ChangeActiveStatus(this,periodManagerService, activeStatus);
        }

        public virtual void CheckAddClaim()
        {
            State.CheckAddClaim();
        }

        public virtual void CheckReplyClaim()
        {
            State.CheckReplyClaim();
        }

        public virtual void CheckCancelClaim()
        {
            State.CheckCancelClaim();
        }

        public virtual void CheckChangeCalculationDeterministicStatus()
        {
            State.CheckChangeCalculationDeterministicStatus();
        }

        
    }
}
