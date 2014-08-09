using MITD.Core;

namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    public class JobIndexPointModifyException : JobIndexPointException, IModifyException
    {
        public JobIndexPointModifyException(string domainObjectName, string propertyName)
            : base("can not modify, " + domainObjectName + "that " + propertyName + " is in use")
        {
            DomainObjectName = domainObjectName;
            PropertyName = propertyName;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
