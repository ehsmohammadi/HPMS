using System.Linq;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public class UnitIndex : AbstractUnitIndex
    {
        #region Fields


        private readonly SharedUnitIndex sharedUnitIndex;
        private long calculationLevel;
        private int calculationOrder;
        
        private readonly long dbId;
        #endregion

        #region Properties

        public virtual SharedUnitIndex SharedUnitIndex { get { return sharedUnitIndex; } }

        private  UnitIndexGroup group;

        public virtual UnitIndexGroup Group
        {
            get { return group; }
        }

        public virtual long CalculationLevel
        {
            get { return calculationLevel; }
        }

        public virtual int CalculationOrder
        {
            get { return calculationOrder; }
        }

        public virtual string Name { get { return sharedUnitIndex.Name; } }

        public virtual string DictionaryName { get { return sharedUnitIndex.DictionaryName; } }

        public virtual Guid TransferId { get { return sharedUnitIndex.TransferId; } }

        private  bool isInquireable;
        public virtual bool IsInquireable { get { return isInquireable; } }

        
        public virtual SharedUnitIndexId SharedUnitIndexId { get { return sharedUnitIndex.Id; } }

        private  UnitIndex referenceIndex;
        public virtual UnitIndex ReferenceIndex
        {
            get { return referenceIndex; }
            set { referenceIndex = value; }
        }

        private readonly IDictionary<SharedUnitIndexCustomFieldId, string> customFieldValues =  new Dictionary<SharedUnitIndexCustomFieldId, string>();

        public virtual IReadOnlyDictionary<SharedUnitIndexCustomFieldId, string> CustomFieldValues
        {
            get { return customFieldValues.ToDictionary(x=>x.Key,y=>y.Value); }
        }

        

        #endregion

        #region Constructors
        protected UnitIndex():base()
        {
            //For OR mapper
        }

        public UnitIndex(AbstractUnitIndexId abstractUnitIndexId, Period period, SharedUnitIndex sharedUnitIndex, UnitIndexGroup group, bool isInquireable, long calculationLevel = 1, int calculationOrder = 1)
        {
            if (period == null)
                throw new ArgumentNullException("period");
            period.CheckAssigningUnitIndex();
            if (sharedUnitIndex == null)
                throw new ArgumentNullException("sharedUnitIndex");
            if (group == null)
                throw new ArgumentNullException("group");
            
            if (!group.PeriodId.Equals(period.Id))
                throw new UnitIndexCompareException("UnitIndex","UnitIndexGroup","Period");
            this.calculationLevel = calculationLevel;
            
            // must be check periodId
            id = abstractUnitIndexId;
            periodId = period.Id;
            this.group = group;
            this.sharedUnitIndex = sharedUnitIndex;
            this.isInquireable = isInquireable;
            this.calculationOrder = calculationOrder;
        }



        #endregion

        #region Public Methods

        public virtual void Update(UnitIndexGroup group, bool isInquireable, Dictionary<SharedUnitIndexCustomField, string> customFieldValueItems, IPeriodManagerService periodChecker, int calculationOrder, long calculationLevel = 1)
        {
            periodChecker.CheckUpdatingUnitIndex(this);
            if (!group.PeriodId.Equals(periodId))
                throw new UnitIndexCompareException("UnitIndex", "UnitIndexGroup", "Period");        
            this.group = group;
            this.isInquireable = isInquireable;
            this.calculationLevel = calculationLevel;
            this.calculationOrder = calculationOrder;
            UpdateCustomFields(customFieldValueItems);
        }

        public virtual void UpdateCustomFields(Dictionary<SharedUnitIndexCustomField, string> customFieldValueItems)
        {

            foreach (var itm in customFieldValueItems)
            {
                if (!this.customFieldValues.Select(c => c.Key).Contains(itm.Key.Id))
                    AssignAndSetValueSharedCustomField(itm);
                else
                {
                    removeSharedCustomField(itm.Key.Id);
                    AssignAndSetValueSharedCustomField(itm);
                    //customFieldValues.Single(c => c.Key == itm.Key.Id).Value = itm.Value;
                }
            }

            var keys = new List<SharedUnitIndexCustomFieldId>(customFieldValues.Keys);
            foreach (var key in keys)
            {
                if (!customFieldValueItems.Select(c => c.Key.Id).Contains(key))
                    removeSharedCustomField(key);
            }


        }

        public virtual void AssignAndSetValueSharedCustomField(KeyValuePair<SharedUnitIndexCustomField, string> itm)
        {
            customFieldValues.Add(itm.Key.Id, itm.Value);
        }

        private void removeSharedCustomField(SharedUnitIndexCustomFieldId key)
        {
            customFieldValues.Remove(key);
        }

        public virtual void RemoveSharedCustomField(SharedUnitIndexCustomField sharedUnitIndexCustomField)
        {
            removeSharedCustomField(sharedUnitIndexCustomField.Id);
        }


        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(UnitIndex other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (UnitIndex)obj;
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
