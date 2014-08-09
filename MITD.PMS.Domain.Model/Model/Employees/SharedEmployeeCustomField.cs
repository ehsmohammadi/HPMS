using System;
using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Employees
{
    public class SharedEmployeeCustomField : IEntity<SharedEmployeeCustomField>
    {
        #region Fields

        private readonly byte[] rowVersion;
        

        #endregion

        #region Properties

        private readonly SharedEmployeeCustomFieldId id;
        public virtual SharedEmployeeCustomFieldId Id
        {
            get { return id; }

        }

        private readonly string name;
        public virtual string Name { get { return name; } }

        private readonly string dictionaryName;
        public virtual string DictionaryName { get { return dictionaryName; } }

        private readonly long minValue;
        public virtual long MinValue { get { return minValue; } }

        private readonly long maxValue;
        public virtual long MaxValue { get { return maxValue; } }


        #endregion

        #region Constructors
        protected SharedEmployeeCustomField()
        {
            //For OR mapper
        }

        public SharedEmployeeCustomField(SharedEmployeeCustomFieldId id,
                           string name,
                           string dictionaryName, long minValue, long maxValue)
        {
            if (id == null)
                throw new ArgumentNullException("id");
            this.id = id;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");
            this.name = name;

            if (string.IsNullOrWhiteSpace(dictionaryName))
                throw new ArgumentNullException("dictionaryName");
            this.dictionaryName = dictionaryName;
            
            if (minValue>maxValue)
                throw new ArgumentException("minvalue is greater than maxValue");
            this.minValue = minValue;
            this.maxValue = maxValue;

        }



        #endregion

        #region Public Methods

        #endregion

        #region IEntity Member

        public virtual bool SameIdentityAs(SharedEmployeeCustomField other)
        {
            return (other != null) && Id.Equals(other.Id);
        }


        #endregion

        #region Object override

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedEmployeeCustomField)obj;
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
