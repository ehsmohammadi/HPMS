using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class CustomFieldWithActionMapper : BaseMapper<CustomFieldType, CustomFieldDTOWithActions>, IMapper<CustomFieldType, CustomFieldDTOWithActions>
    { 

        public override CustomFieldDTOWithActions MapToModel(CustomFieldType entity)
        {
            var res = new CustomFieldDTOWithActions();
            res.Id = entity.Id.Id;
            res.EntityId = Convert.ToInt32(entity.EntityId.Value);
            res.EntityName = entity.EntityId.DisplayName;
            res.MaxValue = entity.MaxValue;
            res.MinValue = entity.MinValue;
            res.Name = entity.Name;
            res.DictionaryName = entity.DictionaryName;
            res.ActionCodes = new List<int>
            {
                (int) ActionType.CreateCustomField,
                (int) ActionType.ModifyCustomField,
                (int) ActionType.DeleteCustomField
            };
            return res;

        }

        public override CustomFieldType MapToEntity(CustomFieldDTOWithActions model)
        {
            throw new Exception();

        }

    }

}
