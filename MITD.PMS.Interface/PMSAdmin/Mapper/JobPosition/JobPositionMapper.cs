using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.JobPositions;

namespace MITD.PMS.Interface
{
    public class JobPositionMapper : BaseMapper<JobPosition, JobPositionDTO>, IMapper<JobPosition, JobPositionDTO>
    { 

        public override JobPositionDTO MapToModel(JobPosition entity)
        {
            var res = new JobPositionDTO
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName
                };
            return res;

        }

        public override JobPosition MapToEntity(JobPositionDTO model)
        {
            var res = new JobPosition(new JobPositionId(model.Id), model.Name, model.DictionaryName);
            return res;

        }

    }

}
