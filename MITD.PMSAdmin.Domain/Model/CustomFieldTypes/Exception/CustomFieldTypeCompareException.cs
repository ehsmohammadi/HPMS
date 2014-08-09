using MITD.Core;

namespace MITD.PMSAdmin.Domain.Model.CustomFieldTypes
{
    public class CustomFieldTypeCompareException : CustomFieldTypeException, ICompareException
    {
        public CustomFieldTypeCompareException(string domainObjectName, string propertyNameSource, string propertyNameCompare)
            : base(propertyNameSource + " and " + propertyNameCompare + " is not match " + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            PropertyNameCompare = propertyNameSource;
            PropertyNameCompare = propertyNameCompare;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyNameSource { get; private set; }
        public string PropertyNameCompare { get; private set; }
    }
}
