using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.Jobs;

namespace MITD.PMS.Interface.Mappers
{
    public class JobMapper : BaseMapper<Job,JobDTO>, IMapper<Job,JobDTO>
    { 
        public override JobDTO MapToModel(Job entity)
        {
            var res = new JobDTO()
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                    TransferId = entity.TransferId
                    
                };
            return res;
        }

        public override Job MapToEntity(JobDTO model)
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
