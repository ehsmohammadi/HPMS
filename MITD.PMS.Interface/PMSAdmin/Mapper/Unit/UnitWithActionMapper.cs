using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.Units;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class UnitWithActionMapper : BaseMapper<Unit, UnitDTOWithActions>, IMapper<Unit, UnitDTOWithActions>
    {
 
        public override UnitDTOWithActions MapToModel(Unit entity)
        {
            var res = new UnitDTOWithActions
                      {
                          Id = entity.Id.Id,
                          Name = entity.Name,
                          DictionaryName = entity.DictionaryName,
                          TransferId = entity.TransferId,
                          ActionCodes = new List<int>
                                        {
                                            (int) ActionType.AddUnit,
                                            (int) ActionType.ModifyUnit,
                                            (int) ActionType.DeleteUnit
                                        }
                      };
            return res;

        }

        public override Unit MapToEntity(UnitDTOWithActions model)
        {
            var res = new Unit { };
            return res;

        }

    }

}
