using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;


namespace MITD.PMSSecurity.Domain
{
    public class PartyArgumentException : PartyException, IArgumentException
    {
        public PartyArgumentException(string domainObjectName, string argumentName)
            : base("Invalid " + argumentName + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            ArgumentName = argumentName;
        }

        public string DomainObjectName { get; private set; }
        public string ArgumentName { get; private set; }
    }
}
