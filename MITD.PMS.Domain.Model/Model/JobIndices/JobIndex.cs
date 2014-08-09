using System.Linq;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.JobIndices
{
    public class JobIndex : AbstractJobIndex
    {
        #region Fields


        private readonly SharedJobIndex sharedJobIndex;
        private long calculationLevel;
        private int calculationOrder;
        
        private readonly long dbId;
        #endregion

        #region Properties

        public virtual SharedJobIndex SharedJobIndex { get { return sharedJobIndex; } }

        private  JobIndexGroup group;

        public virtual JobIndexGroup Group
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

        public virtual string Name { get { return sharedJobIndex.Name; } }

        public virtual string DictionaryName { get { return sharedJobIndex.DictionaryName; } }

        private  bool isInquireable;
        public virtual bool IsInquireable { get { return isInquireable; } }

        
        public virtual SharedJobIndexId SharedJobIndexId { get { return sharedJobIndex.Id; } }

        private  JobIndex referenceIndex;
        public virtual JobIndex ReferenceIndex
        {
            get { return referenceIndex; }
            set { referenceIndex = value; }
        }

        private readonly IDictionary<SharedJobIndexCustomFieldId, string> customFieldValues =  new Dictionary<SharedJobIndexCustomFieldId, string>();

        public virtual IReadOnlyDictionary<SharedJobIndexCustomFieldId, string> CustomFieldValues
        {
            get { return customFieldValues.ToDictionary(x=>x.Key,y=>y.Value); }
        }

        

        #endregion

        #region Constructors
        protected JobIndex():base()
        {
            //For OR mapper
        }

        public JobIndex(AbstractJobIndexId abstractJobIndexId, Period period, SharedJobIndex sharedJobIndex, JobIndexGroup group, bool isInquireable, long calculationLevel = 1, int calculationOrder = 1)
        {
            if (period == null)
                throw new ArgumentNullException("period");
            period.CheckAssigningJobIndex();
            if (sharedJobIndex == null)
                throw new ArgumentNullException("sharedJobIndex");
            if (group == null)
                throw new ArgumentNullException("group");
            
            if (!group.PeriodId.Equals(period.Id))
                throw new JobIndexCompareException("JobIndex","JobIndexGroup","Period");
            this.calculationLevel = calculationLevel;
            
            // must be check periodId
            id = abstractJobIndexId;
            periodId = period.Id;
            this.group = group;
            this.sharedJobIndex = sharedJobIndex;
            this.isInquireable = isInquireable;
            this.calculationOrder = calculationOrder;
        }



        #endregion

        #region Public Methods

        public virtual void Update(JobIndexGroup group, bool isInquireable, Dictionary<SharedJobIndexCustomField, string> customFieldValueItems, IPeriodManagerService periodChecker, int calculationOrder, long calculationLevel = 1)
        {
            periodChecker.CheckUpdatingJobIndex(this);
            if (!group.PeriodId.Equals(periodId))
                throw new JobIndexCompareException("JobIndex", "JobIndexGroup", "Period");        
            this.group = group;
            this.isInquireable = isInquireable;
            this.calculationLevel = calculationLevel;
            this.calculationOrder = calculationOrder;
            UpdateCustomFields(customFieldValueItems);
        }

        public virtual void UpdateCustomFields(Dictionary<SharedJobIndexCustomField, string> customFieldValueItems)
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

            var keys = new List<SharedJobIndexCustomFieldId>(customFieldValues.Keys);
            foreach (var key in keys)
            {
                if (!customFieldValueItems.Select(c => c.Key.Id).Contains(key))
                    removeSharedCustomField(key);
            }


        }

        public virtual void AssignAndSetValueSharedCustomField(KeyValuePair<SharedJobIndexCustomField, string> itm)
        {
            customFieldValues.Add(itm.Key.Id, itm.Value);
        }

        private void removeSharedCustomField(SharedJobIndexCustomFieldId key)
        {
            customFieldValues.Remove(key);
        }

        public virtual void RemoveSharedCustomField(SharedJobIndexCustomField sharedJobIndexCustomField)
        {
            removeSharedCustomField(sharedJobIndexCustomField.Id);
        }


        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(JobIndex other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        

        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (JobIndex)obj;
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
