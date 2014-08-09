using MITD.Core;

namespace MITD.PMSAdmin.Domain.Model.CustomFieldTypes
{
    public class CustomFieldTypeDeleteException : CustomFieldTypeException, IDeleteException
    {
        public CustomFieldTypeDeleteException(string domainObjectName, string relatedObjectName)
            : base("can not delete, " + domainObjectName + "that has related data with" + relatedObjectName)
        {
            DomainObjectName = domainObjectName;
            RelatedObjectName = relatedObjectName;
        }

        public string DomainObjectName { get; private set; }
        public string RelatedObjectName { get; private set; }
    }
}
