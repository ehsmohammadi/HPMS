using System;
using MITD.Core;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class JobPositionAssignmentMapper : BaseMapper<JobPosition, JobPositionInPeriodAssignmentDTO>, IMapper<JobPosition, JobPositionInPeriodAssignmentDTO>
    {

        public override JobPositionInPeriodAssignmentDTO MapToModel(JobPosition entity)
        {
            var res = new JobPositionInPeriodAssignmentDTO
            {
                JobPositionId = entity.Id.SharedJobPositionId.Id,
                PeriodId = entity.Id.PeriodId.Id,
            };
            return res;

        }

        public override JobPosition MapToEntity(JobPositionInPeriodAssignmentDTO model)
        {
            throw new InvalidOperationException();
        }

    }

}
