using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Presentation.Contracts;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Interface
{
    public class UserGroupDtoMapper : BaseMapper<Group, UserGroupDTO>, IMapper<Group, UserGroupDTO>
    {

        public override UserGroupDTO MapToModel(Group entity)
        {
            var res = new UserGroupDTO
            {
                PartyName = entity.Id.PartyName,
                Description = entity.Description,
                CustomActions = entity.CustomActions.ToDictionary(s => s.Key, s => s.Value)
            };
            return res;

        }

        public override Group MapToEntity(UserGroupDTO model)
        {
            var res = new Group(new PartyId(model.PartyName), model.Description);
            return res;
        }

    }


    public class UserGroupDescriptionDtoMapper : BaseMapper<Group, UserGroupDescriptionDTO>, IMapper<Group, UserGroupDescriptionDTO>
    {

        public override UserGroupDescriptionDTO MapToModel(Group entity)
        {
            var res = new UserGroupDescriptionDTO
            {
                PartyName = entity.Id.PartyName,
                Description = entity.Description,
            };
            return res;

        }

        public override Group MapToEntity(UserGroupDescriptionDTO model)
        {
            throw new NotSupportedException("map UserGroupDescriptionDTO to Group not supported");
        }

    }
        
}
