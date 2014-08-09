using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Exceptions;



namespace MITD.PMS.Domain.Model.Claims
{
    public class Claim : IEntity<Claim>
    {
        #region Fields

        private readonly byte[] rowVersion;
       
        #endregion

        #region Properties
        
        private readonly ClaimId id;
        public virtual ClaimId Id
        {
            get { return id; }
            
        }

        private readonly PeriodId periodId;
        public virtual PeriodId PeriodId
        {
            get { return periodId; }

        }

        private  ClaimState state;
        public virtual ClaimState State
        {
            get { return state; }
            protected internal set { state = value; }
        }

        private readonly string employeeNo;
        public virtual string EmployeeNo { get { return employeeNo; } }

        private readonly string title;
        public virtual string Title { get { return title; } }

        private readonly DateTime claimDate;
        public virtual DateTime ClaimDate { get { return claimDate; } }

        private DateTime? responseDate;
        public virtual DateTime? ResponseDate { get { return responseDate; } }

        private readonly ClaimTypeEnum claimTypeId;
        public virtual ClaimTypeEnum ClaimTypeId { get { return claimTypeId; } }

        private readonly string request;
        public virtual string Request { get { return request; } }

        private string response;
        public virtual string Response { get { return response; } }
      
        #endregion

        #region Constructors
        protected Claim()
        {
           //For OR mapper
        }

        public Claim(ClaimId claimId,Period period,string employeeNo, string title,DateTime claimDate,
             ClaimTypeEnum claimTypeId, string request)
        {
            period.CheckAddClaim();
            if (claimId == null)
                throw new ArgumentNullException("claimId");
            if (period == null)
                throw new ArgumentNullException("period");

            id = claimId;
            this.periodId = period.Id;
            if (string.IsNullOrWhiteSpace(employeeNo))
                throw new ClaimArgumentException("Claim","EmployeeNo");
            this.employeeNo = employeeNo;
            this.title = title;
            this.claimDate = claimDate;
            this.claimTypeId = claimTypeId;
            this.request = request;
            state = new ClaimOpenedState();
        }

       
       
        #endregion

        #region Public Methods

        public virtual void Accept(DateTime responseDate, string response, Period period)
        {
            period.CheckReplyClaim();
            State.AcceptClaim(this);
            this.responseDate = responseDate;
            this.response = response;
        }

        public virtual void Reject(DateTime responseDate, string response, Period period)
        {
            period.CheckReplyClaim();
            State.RejectClaim(this);
            this.responseDate = responseDate;
            this.response = response;
        }

        public virtual void Cancel(Period period)
        {
            period.CheckCancelClaim();
            State.CancelClaim(this);
        }
       
        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(Claim other)
        {
            return (other != null) && Id.Equals(other.Id);
        }
      
        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (Claim)obj;
            return SameIdentityAs(other);
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

    }
}
