using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.UnitIndices;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface.Mappers
{
    public class UnitIndexWithActionsMapper : BaseMapper<AbstractUnitIndex, AbstractUnitIndexDTOWithActions>, IMapper<AbstractUnitIndex, AbstractUnitIndexDTOWithActions>
    {
        public override AbstractUnitIndexDTOWithActions MapToModel(AbstractUnitIndex entity)
        {
            if (entity is UnitIndex)
            {
                var res = new UnitIndexDTOWithActions()
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                    ParentId = ((UnitIndex) entity).Category.Id.Id,
                    TransferId=entity.TransferId ,
                    ActionCodes = new List<int> {(int) ActionType.ModifyUnitIndex, (int) ActionType.DeleteUnitIndex}
                };
                return res;
            }
            else
            {
                var res = new UnitIndexCategoryDTOWithActions()
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                    ActionCodes =
                        new List<int>
                        {
                            (int) ActionType.AddUnitIndex,
                            (int) ActionType.AddUnitIndexCategory,
                            (int) ActionType.ModifyUnitIndexCategory,
                            (int) ActionType.DeleteUnitIndexCategory
                        }
                };
                
                if (((UnitIndexCategory) entity).Parent != null)
                    res.ParentId = ((UnitIndexCategory) entity).Parent.Id.Id;

                return res;
            }
        }

        public override AbstractUnitIndex MapToEntity(AbstractUnitIndexDTOWithActions model)
        {
            //var res = new UnitIndex(new UnitIndexId(model.Id),new AbstractUnitIndexId(model.ParentId.Value), model.Name);
            //return res;
            throw new NotSupportedException("can not map UnitIndexDTOWithActions to  UnitIndex Entity");
        }

        //public IEnumerable<UnitDTO> MapToModel(IEnumerable<Unit> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<Unit> MapToEntity(IEnumerable<UnitDTO> models)
        //{
        //    throw new NotImplementedException();
        //}

        //public UnitDTO RemapModel(UnitDTO model)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
