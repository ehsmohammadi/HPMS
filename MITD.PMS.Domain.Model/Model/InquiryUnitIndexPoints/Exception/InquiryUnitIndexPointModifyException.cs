using MITD.Core;

namespace MITD.PMS.Domain.Model.InquiryUnitIndexPoints
{
    public class InquiryUnitIndexPointModifyException : InquiryUnitIndexPointException, IModifyException
    {
        public InquiryUnitIndexPointModifyException(string domainObjectName, string propertyName)
            : base("can not modify, " + domainObjectName + "that " + propertyName + " is in use")
        {
            DomainObjectName = domainObjectName;
            PropertyName = propertyName;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
