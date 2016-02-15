using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.JobIndices;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface.Mappers
{
    public class JobIndexWithActionsMapper : BaseMapper<AbstractJobIndex, AbstractJobIndexDTOWithActions>, IMapper<AbstractJobIndex, AbstractJobIndexDTOWithActions>
    {
        public override AbstractJobIndexDTOWithActions MapToModel(AbstractJobIndex entity)
        {
            if (entity is JobIndex)
            {
                var res = new JobIndexDTOWithActions()
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                    TransferId=entity.TransferId ,
                    ParentId = ((JobIndex) entity).Category.Id.Id,
                    ActionCodes = new List<int> {(int) ActionType.ModifyJobIndex, (int) ActionType.DeleteJobIndex}
                };
                return res;
            }
            else
            {
                var res = new JobIndexCategoryDTOWithActions()
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                    ActionCodes =
                        new List<int>
                        {
                            (int) ActionType.AddJobIndex,
                            (int) ActionType.AddJobIndexCategory,
                            (int) ActionType.ModifyJobIndexCategory,
                            (int) ActionType.DeleteJobIndexCategory
                        }
                };
                
                if (((JobIndexCategory) entity).Parent != null)
                    res.ParentId = ((JobIndexCategory) entity).Parent.Id.Id;

                return res;
            }
        }

        public override AbstractJobIndex MapToEntity(AbstractJobIndexDTOWithActions model)
        {
            //var res = new JobIndex(new JobIndexId(model.Id),new AbstractJobIndexId(model.ParentId.Value), model.Name);
            //return res;
            throw new NotSupportedException("can not map JobIndexDTOWithActions to  JobIndex Entity");
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
