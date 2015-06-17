using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSAdmin.Domain.Model.UnitIndices;

namespace MITD.PMS.Interface.Mappers
{
    public class UnitIndexMapper : BaseMapper<AbstractUnitIndex, AbstractIndex>, IMapper<AbstractUnitIndex, AbstractIndex>
    {
        public override AbstractIndex MapToModel(AbstractUnitIndex entity)
        {
            if (entity is UnitIndex)
            {
                var res = new UnitIndexDTO()
                    {
                        Id = entity.Id.Id,
                        Name = entity.Name,
                        DictionaryName = entity.DictionaryName,
                        ParentId = ((UnitIndex)entity).Category.Id.Id,
                    };
                return res;
            }
            else
            {
                var res = new UnitIndexCategoryDTO()
                {
                    Id = entity.Id.Id,
                    Name = entity.Name,
                    DictionaryName = entity.DictionaryName,
                };
                if (((UnitIndexCategory)entity).Parent != null)
                    res.ParentId = ((UnitIndexCategory)entity).Parent.Id.Id;
                return res;
            }
        }

        public override AbstractUnitIndex MapToEntity(AbstractIndex model)
        {
            // check parentid must not be null
            //var res = new UnitIndex(new AbstractUnitIndexId(model.Id), new UnitIndexCategoryId(model.ParentId.Value), model.Name, model.DictionaryName);
            
            //return res;
            throw new NotSupportedException("Map to entity not supported");

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
