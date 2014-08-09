using MITD.Core;

namespace MITD.PMSAdmin.Domain.Model.JobPositions
{
    public class JobPositionDeleteException : JobPositionException, IDeleteException
    {
        public JobPositionDeleteException(string domainObjectName, string relatedObjectName)
            : base("can not delete, " + domainObjectName + "that has related data with" + relatedObjectName)
        {
            DomainObjectName = domainObjectName;
            RelatedObjectName = relatedObjectName;
        }

        public string DomainObjectName { get; private set; }
        public string RelatedObjectName { get; private set; }
    }
}
