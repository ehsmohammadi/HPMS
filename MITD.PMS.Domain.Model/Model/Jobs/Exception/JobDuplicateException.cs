using MITD.Core;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class JobDuplicateException : JobException, IDuplicateException
    {
        public JobDuplicateException(string domainObjectName, string propertyName)
            : base("Duplicate " + propertyName + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            PropertyName = propertyName;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
