using MITD.Core;

namespace MITD.PMS.Domain.Model.Jobs
{
    public class JobArgumentException : JobException, IArgumentException
    {
        public JobArgumentException(string domainObjectName, string argumentName)
            : base("Invalid " + argumentName + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            ArgumentName = argumentName;
        }

        public string DomainObjectName { get; private set; }
        public string ArgumentName { get; private set; }
    }
}
