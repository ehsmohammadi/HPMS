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

        public List<ActionType> GetAllAuthorizedActions(List<User> pmsUsers)
        {
            if (pmsUsers == null || pmsUsers.Count == 0)
                throw new PMSSecurityAccessException("You are not Authorize to access to system");

            var userId = pmsUsers[0].Id;
            if (pmsUsers.Any(u => u.Id != userId))
                throw new Exception("username in all pms users must be same");

            var user = userRep.GetUserById(userId);
            if (user == null)
                throw new NullReferenceException("user");

            var authorizedActionsUser = new List<ActionType>();
            foreach (var u in pmsUsers)
                authorizedActionsUser.AddRange(u.Actions);
            authorizedActionsUser = authorizedActionsUser.Distinct().ToList();

            foreach (var itm in user.CustomActions)
            {
                var custAct = ActionType.GetAll<ActionType>().Where(a => a.Value == itm.Key.ToString()).Single();
                if (itm.Value)
                {
                    if (!authorizedActionsUser.Contains(custAct))
                        authorizedActionsUser.Add(custAct);
                }
                else if (authorizedActionsUser.Contains(custAct))
                {
                    if (authorizedActionsUser.Contains(custAct))
                        authorizedActionsUser.Remove(custAct);
                }
            }

            return authorizedActionsUser;


        }

        public bool IsAuthorize(List<ActionType> authorizedActionsUser, List<ActionType> actions)
        {
            return actions.All(authorizedActionsUser.Contains);
        }
    }
}
