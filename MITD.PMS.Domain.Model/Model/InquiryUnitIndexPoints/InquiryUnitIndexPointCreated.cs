using MITD.Core.RuleEngine;
using MITD.Core.RuleEngine.Model;
using MITD.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MITD.PMS.Domain.Model.InquiryUnitIndexPoints
{
    public class InquiryUnitIndexPointCreated : IDomainEvent<InquiryUnitIndexPointCreated>
    {
        private readonly InquiryUnitIndexPoint inquiryUnitIndexPoint;
        public InquiryUnitIndexPointCreated(InquiryUnitIndexPoint inquiryUnitIndexPoint)
        {
            this.inquiryUnitIndexPoint = inquiryUnitIndexPoint;
        }
        public InquiryUnitIndexPoint InquiryUnitIndexPoint { get { return inquiryUnitIndexPoint; } }
        public bool SameEventAs(InquiryUnitIndexPointCreated other)
        {
            throw new NotImplementedException();
        }
    }
}
