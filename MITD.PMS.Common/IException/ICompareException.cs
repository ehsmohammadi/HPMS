using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core
{
    public interface ICompareException : IException
    {
        string DomainObjectName { get; }
        string PropertyNameSource { get; }
        string PropertyNameCompare { get; }

    }

    public class CompareException : Exception, ICompareException
    {
        public CompareException(string message, string domainObjectName, string propertyNameSource, string propertyNameCompare)
            : base(message)
        {
            Code = int.Parse(ApiExceptionCode.InvalidCompare.Value);
            DomainObjectName = domainObjectName;
            PropertyNameSource = propertyNameSource;
            PropertyNameCompare = propertyNameCompare;
        }

        public string DomainObjectName { private set; get; }
        public string PropertyNameSource { get; private set; }
        public string PropertyNameCompare { get; private set; }
        public int Code { private set; get; }
    }
}
