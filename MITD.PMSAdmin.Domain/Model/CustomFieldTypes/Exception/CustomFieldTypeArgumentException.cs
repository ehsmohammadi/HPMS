using MITD.Core;

namespace MITD.PMSAdmin.Domain.Model.CustomFieldTypes
{
    public class CustomFieldTypeArgumentException : CustomFieldTypeException, IArgumentException
    {
        public CustomFieldTypeArgumentException(string domainObjectName, string argumentName)
            : base("Invalid " + argumentName + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            ArgumentName = argumentName;
        }

        public string DomainObjectName { get; private set; }
        public string ArgumentName { get; private set; }
    }
}
