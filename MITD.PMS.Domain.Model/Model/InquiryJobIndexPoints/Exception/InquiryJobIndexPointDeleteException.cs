﻿using MITD.Core;

namespace MITD.PMS.Domain.Model.InquiryJobIndexPoints
{
    public class InquiryJobIndexPointDeleteException : InquiryJobIndexPointException, IDeleteException
    {
        public InquiryJobIndexPointDeleteException(string domainObjectName, string relatedObjectName)
            : base("can not delete, " + domainObjectName + "that has related data with" + relatedObjectName)
        {
            DomainObjectName = domainObjectName;
            RelatedObjectName = relatedObjectName;
        }

        public string DomainObjectName { get; private set; }
        public string RelatedObjectName { get; private set; }
    }
}
