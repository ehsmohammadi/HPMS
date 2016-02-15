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
            var res = new JobPositionDTOWithActions
                      {
                          Id = entity.Id.Id,
                          Name = entity.Name,
                          DictionaryName = entity.DictionaryName,
                          TransferId = entity.TransferId,
                          ActionCodes = new List<int>
                                        {
                                            (int) ActionType.AddJobPosition,
                                            (int) ActionType.ModifyJobPosition,
                                            (int) ActionType.DeleteJobPosition
                                        }
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
