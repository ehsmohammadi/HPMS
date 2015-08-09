using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.PMS.Domain.Model.Units;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class UnitInPeriodMapper : BaseFilterMapper<Unit, UnitInPeriodDTO>, IFilterMapper<Unit, UnitInPeriodDTO>
    {

        protected override UnitInPeriodDTO MapToModel(Unit entity)
        {
            var res = new UnitInPeriodDTO
                {

                    Name = entity.Name,
                    UnitId = entity.Id.SharedUnitId.Id,
                    
                };
            if (entity.Parent != null)
                res.ParentId = entity.Parent.Id.SharedUnitId.Id;
            return res;

        }    
       
    }

    public class UnitInPeriodWithActionsMapper : BaseFilterMapper<Unit, UnitInPeriodDTOWithActions>, IFilterMapper<Unit, UnitInPeriodDTOWithActions>
    {
        protected override UnitInPeriodDTOWithActions MapToModel(Unit entity)
        {
            var res = new UnitInPeriodDTOWithActions()
            {
                UnitId = entity.Id.SharedUnitId.Id,
                Name = entity.Name,
               
                ActionCodes = new List<int> { (int)ActionType.AddUnitInPeriod, (int)ActionType.ModifyUnitInPeriod, (int)ActionType.DeleteUnitInPeriod ,(int)ActionType.ManageUnitInPeriodInquiry}
            };
            if (entity.Parent != null)
                res.ParentId = entity.Parent.Id.SharedUnitId.Id;
            return res;
        }
    }


}
