using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMSAdmin.Exceptions;

namespace MITD.PMSAdmin.Domain.Model.Units
{
    public class UnitDuplicateException : UnitException, IDuplicateException
    {
        public UnitDuplicateException(string domainObjectName, string propertyName)
            : base("Duplicate " + propertyName + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            PropertyName = propertyName;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
