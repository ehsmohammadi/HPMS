using System;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Interface
{
    public class FunctionMapper : BaseMapper<RuleFunctionBase, FunctionDTO>, IMapper<RuleFunctionBase, FunctionDTO>
    {

        public override FunctionDTO MapToModel(RuleFunctionBase entity)
        {
            var res = new FunctionDTO
            {
                Id = entity.Id.Id,
                Name = entity.Name,
                Content = entity.FunctionsText,

            };
            return res;

        }

        public override RuleFunctionBase MapToEntity(FunctionDTO model)
        {
            throw new Exception("Can not Create RuleFunction At this point");

        }

    }

}
