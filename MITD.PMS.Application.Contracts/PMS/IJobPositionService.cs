
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMS.Domain.Model.Units;
using MITD.PMSReport.Domain.Model;

namespace MITD.PMS.Application.Contracts
{
    public interface IJobPositionService : IService
    {
        void RemoveJobPosition(PeriodId periodId, SharedJobPositionId sharedJobPositionId);
        JobPosition AssignJobPosition(PeriodId periodId, SharedJobPositionId sharedJobPositionId1, SharedJobPositionId parentJobPositionId, Domain.Model.Jobs.JobId jobId, Domain.Model.Units.UnitId unitId);

        JobPosition ModifyJobPosition(PeriodId periodId, SharedJobPositionId sharedJobPositionId, SharedJobPositionId sharedJobPositionId1, JobId jobId, UnitId unitId);
        List<JobPositionInquiryConfigurationItem> GetInquirySubjectWithInquirer(JobPositionId jobPositionId);
        void UpdateInquirers(EmployeeId employeeId, JobPositionId jobPositionId, List<EmployeeIdWithJobPositionId> inquirerEmployeeIdList);
        JobPosition ConfigureInquiry(JobPositionId jobPositionId);
        List<JobPositionId> GetAllJobPositionId(Period period);
        JobPosition GetBy(JobPositionId jobPositionId);
        List<JobPosition> GetAllJobPositionByParentId(JobPositionId jobPositionId);
        List<JobPosition> GetAllParentJobPositions(Period sourcePeriod);

    }
}
