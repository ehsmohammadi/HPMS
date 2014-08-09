
using MITD.Core;
using MITD.Domain.Model;

namespace MITD.PMSAdmin.Domain.Model.CustomFieldTypes
{
    public class EntityTypeEnum : Enumeration, IValueObject<EntityTypeEnum>
    {
        public static readonly EntityTypeEnum Job = new EntityTypeEnum("1", "Job");
        public static readonly EntityTypeEnum JobIndex = new EntityTypeEnum("2", "JobIndex");
        public static readonly EntityTypeEnum Employee = new EntityTypeEnum("3", "Employee");

        public EntityTypeEnum(string value)
            : base(value)
        {
        }

        public EntityTypeEnum(string value, string displayName)
            : base(value, displayName)
        {
        }



        public bool SameValueAs(EntityTypeEnum other)
        {
            return Equals(other);
        }
    }
}
