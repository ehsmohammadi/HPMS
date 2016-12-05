using System;
using MITD.Core;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class JobPositionInPeriodMapper : BaseMapper<JobPosition, JobPositionInPeriodDTO>, IMapper<JobPosition, JobPositionInPeriodDTO>
    {

        public override JobPositionInPeriodDTO MapToModel(JobPosition entity)
        {
            var res = new JobPositionInPeriodDTO
                {

                    Name = entity.Name,
                    JobId = entity.JobId.SharedJobId.Id,
                    Unitid = entity.UnitId.SharedUnitId.Id,
                    JobPositionId = entity.Id.SharedJobPositionId.Id,
                    
                };
            if (entity.Parent != null)
                res.ParentId = entity.Parent.Id.SharedJobPositionId.Id;
            return res;

        }

        public override JobPosition MapToEntity(JobPositionInPeriodDTO model)
        {
            throw new InvalidOperationException();

        }

    }

}
