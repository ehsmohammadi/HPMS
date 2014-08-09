using System.Linq;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Domain.Model.InquiryJobIndexPoints
{
    public class InquiryJobIndexPoint : IEntity<InquiryJobIndexPoint>
    {
        #region Fields

        private readonly byte[] rowVersion;
        
        #endregion

        #region Properties

        private readonly InquiryJobIndexPointId id;
        public virtual InquiryJobIndexPointId Id
        {
            get { return id; }

        }

        private readonly JobPositionInquiryConfigurationItemId configurationItemId;
        public virtual JobPositionInquiryConfigurationItemId ConfigurationItemId
        {
            get { return configurationItemId; }

        }

        
        private readonly AbstractJobIndexId jobIndexId;
        public virtual AbstractJobIndexId JobIndexId
        {
            get { return jobIndexId; }
        }

        private  string jobIndexValue;
        public virtual string JobIndexValue
        {
            get { return jobIndexValue; }
        }
        
       


        #endregion

        #region Constructors
        public InquiryJobIndexPoint()
        {
            //For OR mapper
        }

        public InquiryJobIndexPoint(InquiryJobIndexPointId id ,JobPositionInquiryConfigurationItem configurationItem,
            JobIndex jobIndex, string jobIndexValue)
        {
            if (id == null)
                throw new ArgumentNullException("inquiryJobIndexPointId");
            this.id = id;
            if (id == null)
                throw new ArgumentNullException("configurationItem");
            configurationItemId = configurationItem.Id;
            
            if (id == null)
                throw new ArgumentNullException("jobIndex");
            jobIndexId = jobIndex.Id;
            this.jobIndexValue = jobIndexValue;
        }



        #endregion

        #region Public Methods

        public virtual void SetValue(string value, IPeriodManagerService periodChecker)
        {
            periodChecker.CheckSettingInquiryJobIndexPointValueValue(this);
            jobIndexValue = value;
        }

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(InquiryJobIndexPoint other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


       
        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (InquiryJobIndexPoint)obj;
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
