using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.Units
{
    public class UnitArgumentException : UnitException, IArgumentException
    {
        public UnitArgumentException(string domainObjectName, string argumentName)
            : base("Invalid " + argumentName + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            ArgumentName = argumentName;
        }

        public string DomainObjectName { get; private set; }
        public string ArgumentName { get; private set; }
    }
}
