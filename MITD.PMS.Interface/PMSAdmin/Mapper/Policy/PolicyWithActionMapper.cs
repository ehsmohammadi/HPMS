using System.Collections.Generic;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSAdmin.Domain.Model.Policies;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class PolicyWithActionMapper : BaseMapper<Policy, PolicyDTOWithActions>, IMapper<Policy, PolicyDTOWithActions>
    {
 
        public override PolicyDTOWithActions MapToModel(Policy entity)
        {
            var res = new PolicyDTOWithActions();
            res.Id = entity.Id.Id;
            res.Name = entity.Name;
            res.DictionaryName = entity.DictionaryName;
            res.ActionCodes = new List<int>
            {
                (int) ActionType.AddPolicy,
                (int) ActionType.ModifyPolicy,
                (int) ActionType.DeletePolicy,
                (int) ActionType.ManageFunctions,
                (int) ActionType.ManageRules
            };
            return res;

        }
        public override Policy MapToEntity(PolicyDTOWithActions model)
        {
            throw new System.NotImplementedException();
        }


    }

}
