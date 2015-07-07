using MITD.Core;

namespace MITD.PMS.Domain.Model.UnitIndices
{
    public class UnitIndexCompareException : UnitIndexException, ICompareException
    {
        public UnitIndexCompareException(string domainObjectName, string propertyNameSource, string propertyNameCompare)
            : base( propertyNameSource + " and "+propertyNameCompare + " is not match " + " in " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            PropertyNameSource = propertyNameSource;
            PropertyNameCompare = propertyNameCompare;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyNameSource { get; private set; }
        public string PropertyNameCompare { get; private set; }
    }
}
