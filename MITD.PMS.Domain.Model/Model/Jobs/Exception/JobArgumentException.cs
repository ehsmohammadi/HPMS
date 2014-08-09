using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Exceptions;

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
