using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class JobPositionInPeriodWithActionMapper : BaseMapper<JobPosition, JobPositionInPeriodDTOWithActions>, IMapper<JobPosition, JobPositionInPeriodDTOWithActions>
    {
     
        public override JobPositionInPeriodDTOWithActions MapToModel(JobPosition entity)
        {
            var res = new JobPositionInPeriodDTOWithActions
            {
                Name = entity.Name,
                JobId=entity.JobId.SharedJobId.Id,
                Unitid = entity.UnitId.SharedUnitId.Id,
                JobPositionId = entity.Id.SharedJobPositionId.Id,
                ActionCodes = new List<int>
                {
                    (int) ActionType.AddJobPositionInPeriod,
                    (int) ActionType.DeleteJobPositionInPeriod,
                    (int)ActionType.ManageJobPositionInPeriodInquiry
                }
            };
            if (entity.Parent != null)
                res.ParentId = entity.Parent.Id.SharedJobPositionId.Id;
            return res;

        }

        public override JobPosition MapToEntity(JobPositionInPeriodDTOWithActions model)
        {
            throw new InvalidOperationException();
        }

    }

}
