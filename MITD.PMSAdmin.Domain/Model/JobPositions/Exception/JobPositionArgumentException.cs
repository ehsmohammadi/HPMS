using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.JobPositions
{
    public class JobPositionArgumentException : JobPositionException, IArgumentException
    {
        public JobPositionArgumentException(string domainObjectName, string argumentName)
            : base("Invalid " + argumentName + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            ArgumentName = argumentName;
        }

        public string DomainObjectName { get; private set; }
        public string ArgumentName { get; private set; }
    }
}
