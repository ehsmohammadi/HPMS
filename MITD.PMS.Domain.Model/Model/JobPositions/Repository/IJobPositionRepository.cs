using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MITD.Domain.Repository;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.JobPositions
{
    public interface IJobPositionRepository : IRepository
    {
        List<JobPosition> GetJobPositions(PeriodId periodId);
        void Add(JobPosition jobPosition);
        void DeleteJobPosition(JobPosition jobPosition);
        JobPosition GetBy(JobPositionId jobPositionId);
        List<JobPosition> Find(Expression<Func<JobPosition, bool>> predicate);
        List<JobPosition> GetAllInquirySubjectJobPositions(EmployeeId InquirerId);
        List<JobPositionId> GetAllJobPositionId(Period period);

        List<JobPosition> GetAllJobPositionByParentId(JobPositionId jobPositionId);

        List<JobPosition> GetAllParentJobPositions(Period sourcePeriod);
        void DeleteAllJobPositionConfigurations(Period period);

        Exception ConvertException(Exception exp);
        Exception TryConvertException(Exception exp);
    }
}
