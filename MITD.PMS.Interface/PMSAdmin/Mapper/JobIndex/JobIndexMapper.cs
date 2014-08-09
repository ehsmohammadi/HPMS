using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.JobIndices;

namespace MITD.PMS.Interface.Mappers
{
    public class JobIndexMapper : BaseMapper<AbstractJobIndex, AbstractIndex>, IMapper<AbstractJobIndex, AbstractIndex>
    {
        public override AbstractIndex MapToModel(AbstractJobIndex entity)
        {
            if (entity is JobIndex)
            {
                var res = new JobIndexDTO()
                    {
                        Id = entity.Id.Id,
                        Name = entity.Name,
                        DictionaryName = entity.DictionaryName,
                        ParentId = ((JobIndex)entity).Category.Id.Id,
                    };
                return res;
            }
            else
            {
                var res = new JobIndexCategoryDTO()
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                };
                if (((JobIndexCategory)entity).Parent != null)
                    res.ParentId = ((JobIndexCategory)entity).Parent.Id.Id;
                return res;
            }
        }

        public override AbstractJobIndex MapToEntity(AbstractIndex model)
        {
            // check parentid must not be null
            //var res = new JobIndex(new AbstractJobIndexId(model.Id), new JobIndexCategoryId(model.ParentId.Value), model.Name, model.DictionaryName);
            
            //return res;
            throw new NotSupportedException("Map to entity not supported");

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
