using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Application.Contracts
{
    public interface IInquiryJobIndexPointService : IService
    {
        void Add(JobPositionInquiryConfigurationItem itm, JobIndex jobIndex, string empty);
        void Update(JobPositionInquiryConfigurationItemId configurationItemId, AbstractJobIndexId jobIndexId, string jobIndexValue);
    }
}
