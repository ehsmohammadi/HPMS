using MITD.Core;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationModifyException : CalculationException, IModifyException
    {
        public CalculationModifyException(string domainObjectName, string propertyName)
            : base("can not modify, " + domainObjectName + "that " + propertyName + " is in use")
        {
            DomainObjectName = domainObjectName;
            PropertyName = propertyName;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
