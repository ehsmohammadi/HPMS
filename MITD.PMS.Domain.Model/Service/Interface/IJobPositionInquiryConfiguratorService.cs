using System;
using System.Collections.Generic;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobPositions;

namespace MITD.PMS.Domain.Service
{
    public interface IJobPositionInquiryConfiguratorService
    {
        List<JobPositionInquiryConfigurationItem> Configure(JobPosition jobPosition);
        List<JobPositionInquiryConfigurationItem> GetJobPositionInquiryConfigurationItemBy(Employee inquirer);
    }
}
