using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using System.Transactions;

namespace MITD.PMS.Application
{
    public class InquiryUnitIndexPointService : IInquiryUnitIndexPointService
    {


        private readonly IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep;
        private readonly IPeriodManagerService periodChecker;

        public InquiryUnitIndexPointService(IInquiryUnitIndexPointRepository inquiryUnitIndexPointRep, IPeriodManagerService periodChecker)
        {
            this.inquiryUnitIndexPointRep = inquiryUnitIndexPointRep;
            this.periodChecker = periodChecker;
        }


        public void Add(UnitInquiryConfigurationItem itm, UnitIndex unitIndex, string value)
        {
            using (var tr = new TransactionScope())
            {
                var id = inquiryUnitIndexPointRep.GetNextId();
                var inquiryIndexPoint = new InquiryUnitIndexPoint(new InquiryUnitIndexPointId(id), itm, unitIndex,value);
                inquiryUnitIndexPointRep.Add(inquiryIndexPoint);
                tr.Complete();
            }

        }

        public void Update(UnitInquiryConfigurationItemId configurationItemId, AbstractUnitIndexId unitIndexId,
            string unitIndexValue)
        {
            using (var tr = new TransactionScope())
            {
                InquiryUnitIndexPoint inquiryUnitIndexPoint = inquiryUnitIndexPointRep.GetBy(configurationItemId, unitIndexId);
                inquiryUnitIndexPoint.SetValue(unitIndexValue,periodChecker);
                tr.Complete();
            }
        }
    }


}
