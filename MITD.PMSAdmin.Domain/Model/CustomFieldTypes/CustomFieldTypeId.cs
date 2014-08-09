using MITD.Domain.Model;

namespace MITD.PMSAdmin.Domain.Model.CustomFieldTypes
{
    public class CustomFieldTypeId : IValueObject<CustomFieldTypeId>
    { 

        #region Properties

        private readonly long id;
        public long Id { get { return id; } } 
        #endregion

        #region Constructors
        // for Or mapper
        public CustomFieldTypeId()
        {

        }

        public CustomFieldTypeId(long id)
        {
            this.id = id;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(CustomFieldTypeId other)
        {
            return Id.Equals(other.Id);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (CustomFieldTypeId)obj;
            return SameValueAs(other);
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
