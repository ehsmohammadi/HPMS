using System.Collections.Generic;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;

namespace MITD.PMS.Domain.Service
{
    public interface IInquiryJobIndexPointCreatorService
    {
        void Create(JobPositionInquiryConfigurationItemId configurationItemId);
    }
}
