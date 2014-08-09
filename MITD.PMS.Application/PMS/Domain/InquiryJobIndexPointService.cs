using System;
using System.Collections.Generic;
using System.Linq;
using MITD.Core;
using MITD.Domain.Repository;
using MITD.PMS.Application.Contracts;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryJobIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Service;
using System.Transactions;

namespace MITD.PMS.Application
{
    public class InquiryJobIndexPointService : IInquiryJobIndexPointService
    {


        private readonly IInquiryJobIndexPointRepository inquiryJobIndexPointRep;
        private readonly IPeriodManagerService periodChecker;

        public InquiryJobIndexPointService(IInquiryJobIndexPointRepository inquiryJobIndexPointRep,IPeriodManagerService periodChecker)
        {
            this.inquiryJobIndexPointRep = inquiryJobIndexPointRep;
            this.periodChecker = periodChecker;
        }


        public void Add(JobPositionInquiryConfigurationItem itm, JobIndex jobIndex, string value)
        {
            using (var tr = new TransactionScope())
            {
                var id = inquiryJobIndexPointRep.GetNextId();
                var inquiryIndexPoint = new InquiryJobIndexPoint(new InquiryJobIndexPointId(id), itm, jobIndex,value);
                inquiryJobIndexPointRep.Add(inquiryIndexPoint);
                tr.Complete();
            }

        }

        public void Update(JobPositionInquiryConfigurationItemId configurationItemId, AbstractJobIndexId jobIndexId,
            string jobIndexValue)
        {
            using (var tr = new TransactionScope())
            {
                InquiryJobIndexPoint inquiryJobIndexPoint = inquiryJobIndexPointRep.GetBy(configurationItemId, jobIndexId);
                inquiryJobIndexPoint.SetValue(jobIndexValue,periodChecker);
                tr.Complete();
            }
        }
    }


}
