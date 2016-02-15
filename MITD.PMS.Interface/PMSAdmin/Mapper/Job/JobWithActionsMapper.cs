using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Jobs;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface.Mappers
{
    public class JobWithActionsMapper : BaseMapper<Job, JobDTOWithActions>, IMapper<Job, JobDTOWithActions>
    {
        public override JobDTOWithActions MapToModel(Job entity)
        {
            var res = new JobDTOWithActions()
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                    ActionCodes = new List<int> { (int)ActionType.CreateJob, (int)ActionType.ModifyJob, (int)ActionType.DeleteJob }
                };
            return res;
        }

        public override Job MapToEntity(JobDTOWithActions model)
        {
            var res = new Job(new JobId(model.Id),model.Name,model.DictionaryName);
            return res;

        }

        //public IEnumerable<JobDTO> MapToModel(IEnumerable<Job> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Job> MapToEntity(IEnumerable<JobDTO> models)
        //{
        //    throw new NotImplementedException();
        //}

        //public JobDTO RemapModel(JobDTO model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
