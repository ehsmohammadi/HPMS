using System.Linq;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Domain.Model.InquiryUnitIndexPoints
{
    public class InquiryUnitIndexPoint : IEntity<InquiryUnitIndexPoint>
    {
        #region Fields

        private readonly byte[] rowVersion;
        
        #endregion

        #region Properties

        private readonly InquiryUnitIndexPointId id;
        public virtual InquiryUnitIndexPointId Id
        {
            get { return id; }

        }

        private readonly UnitInquiryConfigurationItemId configurationItemId;
        public virtual UnitInquiryConfigurationItemId ConfigurationItemId
        {
            get { return configurationItemId; }

        }

        
        //private readonly AbstractUnitIndexId unitIndexId;
        //public virtual AbstractUnitIndexId UnitIndexId
        //{
        //    get { return unitIndexId; }
        //}

        private  string unitIndexValue;
        public virtual string UnitIndexValue
        {
            get { return unitIndexValue; }
        }
        
       


        #endregion

        #region Constructors
        public InquiryUnitIndexPoint()
        {
            //For OR mapper
        }

        public InquiryUnitIndexPoint(InquiryUnitIndexPointId id ,UnitInquiryConfigurationItem configurationItem,
            UnitIndex unitIndex, string unitIndexValue)
        {
            if (id == null)
                throw new ArgumentNullException("inquiryUnitIndexPointId");
            this.id = id;
            if (id == null)
                throw new ArgumentNullException("configurationItem");
            configurationItemId = configurationItem.Id;
            
            if (id == null)
                throw new ArgumentNullException("unitIndex");
            //unitIndexId = unitIndex.Id;
            this.unitIndexValue = unitIndexValue;
        }

        public InquiryUnitIndexPoint(InquiryUnitIndexPointId id, UnitInquiryConfigurationItem configurationItem,
        AbstractUnitIndexId unitIndex, string unitIndexValue)
        {
            if (id == null)
                throw new ArgumentNullException("inquiryUnitIndexPointId");
            this.id = id;
            if (configurationItem == null)
                throw new ArgumentNullException("configurationItem");
            configurationItemId = configurationItem.Id;

            if (id == null)
                throw new ArgumentNullException("unitIndex");
           
          //  unitIndexId = unitIndex;
            this.unitIndexValue = unitIndexValue;
        }
        public InquiryUnitIndexPoint(InquiryUnitIndexPointId id, UnitInquiryConfigurationItem configurationItem,
       string unitIndexValue)
        {
            if (id == null)
                throw new ArgumentNullException("inquiryUnitIndexPointId");
            this.id = id;
            if (configurationItem == null)
                throw new ArgumentNullException("configurationItem");
            configurationItemId = configurationItem.Id;

            this.unitIndexValue = unitIndexValue;
        }
        #endregion

        #region Public Methods

        public virtual void SetValue(string value, IPeriodManagerService periodChecker)
        {
            periodChecker.CheckSettingInquiryUnitIndexPointValueValue(this);
            unitIndexValue = value;
        }

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(InquiryUnitIndexPoint other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


       
        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (InquiryUnitIndexPoint)obj;
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
