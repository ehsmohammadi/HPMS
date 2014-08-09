using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class FunctionWithActionMapper : BaseMapper<RuleFunctionBase, FunctionDTODescriptionWithActions>, IMapper<RuleFunctionBase, FunctionDTODescriptionWithActions>
    {

        public override FunctionDTODescriptionWithActions MapToModel(RuleFunctionBase entity)
        {
            var res = new FunctionDTODescriptionWithActions
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                ActionCodes = new List<int>
                {
                    (int) ActionType.AddFunction,
                    (int) ActionType.ModifyFunction,
                    (int) ActionType.DeleteFunction,
                }
            };
            return res;

        }

        public override RuleFunctionBase MapToEntity(FunctionDTODescriptionWithActions model)
        {
            throw new Exception("Can not Create RuleFunction At this point");

        }

    }

}
