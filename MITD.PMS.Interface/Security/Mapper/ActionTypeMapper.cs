﻿using System;
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
    //public class ActionTypeDtoMapper // : BaseMapper<ActionType, ActionTypeDTO>, IMapper<ActionType, ActionTypeDTO>
    //{

    //    public ActionTypeDTO MapToModel(ActionType entity)
    //    {
    //        var res = new ActionTypeDTO
    //        {
    //            Id =(int)entity,
    //            ActionName = entity.GetAttribute<ActionInfoAttribute>().DisplayName,
    //            Description = entity.GetAttribute<ActionInfoAttribute>().Description,
    //        };
    //        return res;
    //    }

    //    public  ActionType MapToEntity(ActionTypeDTO model)
    //    {
    //        return (ActionType) model.Id;
    //    }
    //}

    public class UserGroupDtoWithActionsMapper : BaseMapper<Group, UserGroupDTOWithActions>, IMapper<Group, UserGroupDTOWithActions>
    {

        public override UserGroupDTOWithActions MapToModel(Group entity)
        {
            var res = new UserGroupDTOWithActions
            {
                PartyName = entity.Id.PartyName,
                Description = entity.Description,
                //Privileges = entity.CustomActions
                ActionCodes = new List<int>(){(int)ActionType.AddUserGroup,(int)ActionType.ModifyUserGroup,(int)ActionType.DeleteUserGroup}
            };
            return res;

        }

        public override Group MapToEntity(UserGroupDTOWithActions model)
        {
            throw new Exception("UserDTOWithActions to User mapping not supported");

        }

    }
    

        
}
