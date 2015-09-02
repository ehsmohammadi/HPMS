using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Exceptions;

namespace MITD.PMS.Domain.Model.InquiryUnitIndexPoints
{
    public class InquiryUnitIndexPointDuplicateException : InquiryUnitIndexPointException, IDuplicateException
    {
        public InquiryUnitIndexPointDuplicateException(string domainObjectName, string propertyName)
            : base("Duplicate " + propertyName + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            PropertyName = propertyName;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
