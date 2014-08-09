using MITD.Core;

namespace MITD.PMS.Domain.Model.Claims
{
    public class ClaimDeleteException : ClaimException, IDeleteException
    {
        public ClaimDeleteException(string domainObjectName, string relatedObjectName)
            : base("can not delete, " + domainObjectName + "that has related data with" + relatedObjectName)
        {
            DomainObjectName = domainObjectName;
            RelatedObjectName = relatedObjectName;
        }

        public string DomainObjectName { get; private set; }
        public string RelatedObjectName { get; private set; }
    }
}
