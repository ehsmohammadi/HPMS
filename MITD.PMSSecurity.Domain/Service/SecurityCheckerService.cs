using System;
using System.Collections.Generic;
using System.Linq;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMSSecurity.Domain.Service
{
    public class SecurityCheckerService : ISecurityCheckerService
    {
        private readonly IUserRepository userRep;

        public SecurityCheckerService(IUserRepository userRep)
        {
            this.userRep = userRep;
        }

        public List<ActionType> GetAllAuthorizedActions(List<User> users) // Old: (List<User> pmsUsers)
        {
            #region Old

            //if (pmsUsers == null || pmsUsers.Count == 0)
            //    throw new PMSSecurityAccessException("You are not Authorize to access to system");

            //var userId = pmsUsers[0].Id;
            //if (pmsUsers.Any(u => u.Id != userId))
            //    throw new Exception("username in all pms users must be same");

            //var user = userRep.GetUserById(userId);
            //if (user == null)
            //    throw new NullReferenceException("user");

            //var authorizedActionsUser = new List<ActionType>();
            //foreach (var u in pmsUsers)
            //    authorizedActionsUser.AddRange(u.Actions);
            //authorizedActionsUser = authorizedActionsUser.Distinct().ToList();

            //foreach (var itm in user.CustomActions)
            //{
            //    ActionType custAct = (ActionType) itm.Key;
            //    if (itm.Value)
            //    {
            //        if (!authorizedActionsUser.Contains(custAct))
            //            authorizedActionsUser.Add(custAct);
            //    }
            //    else if (authorizedActionsUser.Contains(custAct))
            //    {
            //        if (authorizedActionsUser.Contains(custAct))
            //            authorizedActionsUser.Remove(custAct);
            //    }
            //}

            //return authorizedActionsUser;

            #endregion Old

            if (users == null || users.Count == 0)
                throw new PMSSecurityAccessException( "You are not authorized to access the system.");

            var userId = users[0].Id;
            if (users.Any(c => c.Id != userId))
                throw new Exception("user name must same");

            var user = userRep.GetUserById(users[0].Id);
            // user = _userRepository.GetUserById(users[0].Id);
            if (user == null)
                throw new NullReferenceException("User");

            var authorizedActionsUser = new List<ActionType>();
            users.ForEach(c => authorizedActionsUser.AddRange(c.Actions));

            user.CustomActions.Where(ca => ca.Value).ToList().ForEach(ca => authorizedActionsUser.Add((ActionType)ca.Key));

            user.CustomActions.Where(ca => !ca.Value).ToList().ForEach(ca => authorizedActionsUser.RemoveAll(at => (int)at == ca.Key));

            authorizedActionsUser = authorizedActionsUser.Distinct().ToList();

            return authorizedActionsUser;

        }

        public bool IsAuthorized(List<ActionType> authorizedActionsUser, List<ActionType> actions)
        {
            return actions.All(authorizedActionsUser.Contains);
        }

        //public List<ActionType> GetAllAuthorizedActionTypesForRole(List<string> rols)
        //{
        //    var res = new List<ActionType>();
        //    Type t;
        //    t = typeof(User);
        //    foreach (var rol in rols)
        //    {
        //        if (rol == "Admin")
        //        {
        //            // t = typeof (AdminUser);
        //            foreach (var act in new AdminUser().Actions)
        //            {
        //                if (res.All(c => c.Id != act.Id))
        //                    res.Add(act);
        //            }


        //        }

        //        if (rol == "Commercial")
        //        {
        //            //  t = typeof(CommercialUser);
        //            foreach (var act in new CommercialUser().GetAllActions())
        //            {
        //                if (res.All(c => c.Id != act.Id))
        //                    res.Add(act);
        //            }

        //        }
               
        //    }
        //    return res;
        //}
    }
}
