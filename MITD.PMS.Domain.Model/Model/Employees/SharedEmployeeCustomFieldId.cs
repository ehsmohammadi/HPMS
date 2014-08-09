using MITD.Domain.Model;

namespace MITD.PMS.Domain.Model.Employees
{
    public class SharedEmployeeCustomFieldId:IValueObject<SharedEmployeeCustomFieldId>
    {
        private long id;
        
        #region Properties
        public long Id
        {
            get { return id; }
        } 
        #endregion

        #region Constructors
        // for Or mapper
        protected SharedEmployeeCustomFieldId()
        {

        }

        public SharedEmployeeCustomFieldId(long id)
        {
            this.id = id;
        } 
        #endregion

        #region IValueObject Member
        public bool SameValueAs(SharedEmployeeCustomFieldId other)
        {
            return Id.Equals(other.Id);
        } 
        #endregion

        #region Object Override 
        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (SharedEmployeeCustomFieldId)obj;
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

        public static bool operator ==(SharedEmployeeCustomFieldId left, SharedEmployeeCustomFieldId right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SharedEmployeeCustomFieldId left, SharedEmployeeCustomFieldId right)
        {
            return !(left == right);
        }

        #endregion
    }
}
