using MITD.Core;

namespace MITD.PMSAdmin.Domain.Model.Jobs
{
    public class JobModifyException : JobException, IModifyException
    {
        public JobModifyException(string domainObjectName, string propertyName)
            : base("can not modify, " + domainObjectName + "that " + propertyName + " is in use")
        {
            DomainObjectName = domainObjectName;
            PropertyName = propertyName;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
