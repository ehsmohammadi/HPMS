using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.JobPositions;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class JobPositionWithActionMapper : BaseMapper<JobPosition, JobPositionDTOWithActions>, IMapper<JobPosition, JobPositionDTOWithActions>
    { 

        public override JobPositionDTOWithActions MapToModel(JobPosition entity)
        {
            var res = new JobPositionDTOWithActions();
            res.Id = entity.Id.Id;
            res.Name = entity.Name;
            res.DictionaryName = entity.DictionaryName;
            res.ActionCodes = new List<int>
            {
                (int) ActionType.AddJobPosition,
                (int) ActionType.ModifyJobPosition,
                (int) ActionType.DeleteJobPosition
            };
            return res;

        }

        public override JobPosition MapToEntity(JobPositionDTOWithActions model)
        {
            var res = new JobPosition { };
            return res;

        }

    }

}
