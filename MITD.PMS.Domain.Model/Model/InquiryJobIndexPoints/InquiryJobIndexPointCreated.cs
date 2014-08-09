using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.Model;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MITD.PMS.Domain.Model.InquiryJobIndexPoints
{
    public class InquiryJobIndexPointCreated : IDomainEvent<InquiryJobIndexPointCreated>
    {
        private readonly InquiryJobIndexPoint inquiryJobIndexPoint;
        public InquiryJobIndexPointCreated(InquiryJobIndexPoint inquiryJobIndexPoint)
        {
            this.inquiryJobIndexPoint = inquiryJobIndexPoint;
        }
        public InquiryJobIndexPoint InquiryJobIndexPoint { get { return inquiryJobIndexPoint; } }
        public bool SameEventAs(InquiryJobIndexPointCreated other)
        {
            throw new NotImplementedException();
        }
    }
}
