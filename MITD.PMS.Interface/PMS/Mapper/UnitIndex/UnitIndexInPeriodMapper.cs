using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMS.Domain.Model.UnitIndices;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class UnitIndexInPeriodMapper : BaseMapper<AbstractUnitIndex, AbstractUnitIndexInPeriodDTO>, IMapper<AbstractUnitIndex, AbstractUnitIndexInPeriodDTO>
    {
        public override AbstractUnitIndexInPeriodDTO MapToModel(AbstractUnitIndex entity)
        {

            if (entity is UnitIndex)
            {
                var res = new UnitIndexInPeriodDTO()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Name = ((UnitIndex)entity).Name,
                    DictionaryName = ((UnitIndex)entity).DictionaryName,
                    ParentId = ((UnitIndex)entity).Group.Id.Id,
                    UnitIndexId = ((UnitIndex)entity).SharedUnitIndexId.Id,
                    IsInquireable = ((UnitIndex)entity).IsInquireable,
                    CalculationLevel = ((UnitIndex)entity).CalculationLevel,
                    CalculationOrder = ((UnitIndex)entity).CalculationOrder
                };
                return res;
            }
            else
            {
                var res = new UnitIndexGroupInPeriodDTO()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Name = ((UnitIndexGroup)entity).Name,
                    DictionaryName = ((UnitIndexGroup)entity).DictionaryName,
                };
                if (((UnitIndexGroup)entity).Parent != null)
                    res.ParentId = ((UnitIndexGroup)entity).Parent.Id.Id;
                return res;
            }
        }

        public override AbstractUnitIndex MapToEntity(AbstractUnitIndexInPeriodDTO model)
        {
            throw new NotSupportedException("Map AbstractIndexInPeriodDTO to AbstractUnitIndex not supported ");
        }
    }


    public class UnitIndexInPeriodWithActionsMapper : BaseMapper<AbstractUnitIndex, AbstractUnitIndexInPeriodDTOWithActions>, IMapper<AbstractUnitIndex, AbstractUnitIndexInPeriodDTOWithActions>
    {
        public override AbstractUnitIndexInPeriodDTOWithActions MapToModel(AbstractUnitIndex entity)
        {
            if (entity is UnitIndex)
            {
                var res = new UnitIndexInPeriodDTOWithActions()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Name = ((UnitIndex)entity).Name,
                    DictionaryName = ((UnitIndex)entity).DictionaryName,
                    ParentId = ((UnitIndex)entity).Group.Id.Id,
                    UnitIndexId = ((UnitIndex)entity).SharedUnitIndexId.Id,
                    IsInquireable = ((UnitIndex)entity).IsInquireable,
                    ActionCodes = new List<int>{(int) ActionType.ModifyUnitIndexInPeriod,(int) ActionType.DeleteUnitIndexInPeriod}
                };
                return res;
            }
            else
            {
                var res = new UnitIndexGroupInPeriodDTOWithActions()
                {
                    Id = entity.Id.Id,
                    PeriodId = entity.PeriodId.Id,
                    Name = ((UnitIndexGroup)entity).Name,
                    DictionaryName = ((UnitIndexGroup)entity).DictionaryName,
                    ActionCodes = new List<int>{
                        (int) ActionType.AddUnitIndexInPeriod,
                        (int) ActionType.AddUnitIndexGroupInPeriod,
                        (int) ActionType.ModifyUnitIndexGroupInPeriod,
                        (int) ActionType.DeleteUnitIndexGroupInPeriod
                    },
                };
                if (((UnitIndexGroup)entity).Parent != null)
                    res.ParentId = ((UnitIndexGroup)entity).Parent.Id.Id;
                return res;
            }
        }

        public override AbstractUnitIndex MapToEntity(AbstractUnitIndexInPeriodDTOWithActions model)
        {
            throw new NotSupportedException("Map AbstractIndexInPeriodDTOWithActions to AbstractUnitIndex not supported ");
            
        }
    }
}
