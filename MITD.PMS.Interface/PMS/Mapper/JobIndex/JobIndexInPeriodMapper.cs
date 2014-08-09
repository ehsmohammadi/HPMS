using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class JobIndexInPeriodMapper : BaseMapper<AbstractJobIndex, AbstractIndexInPeriodDTO>, IMapper<AbstractJobIndex, AbstractIndexInPeriodDTO>
    {
        public override AbstractIndexInPeriodDTO MapToModel(AbstractJobIndex entity)
        {

            if (entity is JobIndex)
            {
                var res = new JobIndexInPeriodDTO()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Name = ((JobIndex)entity).Name,
                    DictionaryName = ((JobIndex)entity).DictionaryName,
                    ParentId = ((JobIndex)entity).Group.Id.Id,
                    JobIndexId = ((JobIndex)entity).SharedJobIndexId.Id,
                    IsInquireable = ((JobIndex)entity).IsInquireable,
                    CalculationLevel = ((JobIndex)entity).CalculationLevel,
                    CalculationOrder = ((JobIndex)entity).CalculationOrder
                };
                return res;
            }
            else
            {
                var res = new JobIndexGroupInPeriodDTO()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Name = ((JobIndexGroup)entity).Name,
                    DictionaryName = ((JobIndexGroup)entity).DictionaryName,
                };
                if (((JobIndexGroup)entity).Parent != null)
                    res.ParentId = ((JobIndexGroup)entity).Parent.Id.Id;
                return res;
            }
        }

        public override AbstractJobIndex MapToEntity(AbstractIndexInPeriodDTO model)
        {
            throw new NotSupportedException("Map AbstractIndexInPeriodDTO to AbstractJobIndex not supported ");
        }
    }


    public class JobIndexInPeriodWithActionsMapper : BaseMapper<AbstractJobIndex, AbstractIndexInPeriodDTOWithActions>, IMapper<AbstractJobIndex, AbstractIndexInPeriodDTOWithActions>
    {
        public override AbstractIndexInPeriodDTOWithActions MapToModel(AbstractJobIndex entity)
        {
            if (entity is JobIndex)
            {
                var res = new JobIndexInPeriodDTOWithActions()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Name = ((JobIndex)entity).Name,
                    DictionaryName = ((JobIndex)entity).DictionaryName,
                    ParentId = ((JobIndex)entity).Group.Id.Id,
                    JobIndexId = ((JobIndex)entity).SharedJobIndexId.Id,
                    IsInquireable = ((JobIndex)entity).IsInquireable,
                    ActionCodes = new List<int>{(int) ActionType.ModifyJobIndexInPeriod,(int) ActionType.DeleteJobIndexInPeriod}
                };
                return res;
            }
            else
            {
                var res = new JobIndexGroupInPeriodDTOWithActions()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Name = ((JobIndexGroup)entity).Name,
                    DictionaryName = ((JobIndexGroup)entity).DictionaryName,
                    ActionCodes = new List<int>{
                        (int) ActionType.AddJobIndexInPeriod,
                        (int) ActionType.AddJobIndexGroupInPeriod,
                        (int) ActionType.ModifyJobIndexGroupInPeriod,
                        (int) ActionType.DeleteJobIndexGroupInPeriod
                    },
                };
                if (((JobIndexGroup)entity).Parent != null)
                    res.ParentId = ((JobIndexGroup)entity).Parent.Id.Id;
                return res;
            }
        }

        public override AbstractJobIndex MapToEntity(AbstractIndexInPeriodDTOWithActions model)
        {
            throw new NotSupportedException("Map AbstractIndexInPeriodDTOWithActions to AbstractJobIndex not supported ");
            
        }
    }
}
