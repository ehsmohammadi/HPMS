using MITD.Core;

namespace MITD.PMSSecurity.Domain
{
    public class PartyModifyException : PartyException, IModifyException
    {
        public PartyModifyException(string domainObjectName, string propertyName)
            : base("can not modify, " + domainObjectName + "that " + propertyName + " is in use")
        {
            DomainObjectName = domainObjectName;
            PropertyName = propertyName;
        }

        public string DomainObjectName { get; private set; }
        public string PropertyName { get; private set; }
    }
}
