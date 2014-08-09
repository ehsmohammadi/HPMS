using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Transactions;
using MITD.PMSSecurity.Application.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Service;

namespace MITD.PMSSecurity.Application
{
    public class SecurityService : ISecurityService
    {
        private IUserRepository userRep;
        private ISecurityCheckerService securityCheckerService;

        public SecurityService(IUserRepository userRep,ISecurityCheckerService securityCheckerService
            )
        {
            this.userRep = userRep;
            this.securityCheckerService = securityCheckerService;
        }

        public bool IsAuthorize(List<ActionType> userActions, List<ActionType> methodRequiredActions)
        {
            return securityCheckerService.IsAuthorize(userActions, methodRequiredActions);
        }

        public List<ActionType> GetAllAuthorizedActions(List<User> pmsUsers)
        {
            return securityCheckerService.GetAllAuthorizedActions(pmsUsers);
        }
        
        public User GetUser(PartyId userId)
        {
            User u = userRep.GetUserById(userId);
            return u;
        }

        public User AddUpdate(PartyId partyId, string firstName, string lastName, string email)
        {
            User u;
            u = GetUser(partyId) == null ? AddUser(partyId,firstName,lastName,email) : UpdateUser(partyId,firstName,lastName,email);
            return u;
        }

        public User AddUser(PartyId partyId, string firstName, string lastName, string email)
        {
            using (var scope = new TransactionScope())
            {
                var u = new User(partyId, firstName, lastName, email);
                userRep.Add(u);
                scope.Complete();
                return u;
            }
        }

        public User AddUser(PartyId partyId, string firstName, string lastName, string email, bool isActive
            , Dictionary<int, bool> customActions, List<PartyId> groups,List<PartyId> permittedWorkListUsers)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var u = new User(partyId, firstName, lastName, email, isActive);
                    assignCustomActionsToParty(u, customActions);
                    assignUserGroupsToUser(u, groups);
                    assignWorkListUsersToUser(u, permittedWorkListUsers);
                    userRep.Add(u);
                    scope.Complete();
                    return u;
                }
            }
            catch (Exception exp)
            {
                var res = userRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        private void assignWorkListUsersToUser(User u, List<PartyId> permittedWorkListUsers)
        {
            foreach (var userId in permittedWorkListUsers)
            {
                var group = userRep.GetUserById(userId);
                u.AssignWorkListUser(group);
            }
        }

        public User UpdateUser(PartyId partyId, string firstName, string lastName, string email)
        {
            using (var scope = new TransactionScope())
            {
                var u = userRep.GetUserById(partyId);
                u.Update(firstName, lastName, email);
                scope.Complete();
                return u;
            }
        }

        public User UpdateUser(PartyId partyId, string firstName, string lastName, string email, bool isActive,
            Dictionary<int, bool> customActions, List<PartyId> groups,List<PartyId> permittedWorkListUsers)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var u = userRep.GetUserById(partyId);
                    var validSelectedActions =ActionType.GetAll<ActionType>()
                                  .Where(c => customActions.Keys.Contains(int.Parse(c.Value))).ToList();
                    var validCustomActions = validSelectedActions.ToDictionary(c => c,c => customActions[int.Parse(c.Value)]);
                    var validGroups = userRep.GetAllUserGroup().Where(g => groups.Contains(g.Id)).ToList();
                    var validWorkListUsers =
                        userRep.GetAllUsers().Where(g => permittedWorkListUsers.Contains(g.Id)).ToList();
                    u.Update(firstName, lastName, email, isActive, validCustomActions, validGroups, validWorkListUsers);
                    scope.Complete();
                    return u;
                }
            }
            catch (Exception exp)
            {
                var res = userRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public void Delete(PartyId partyId)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var u = userRep.GetById(partyId);
                    userRep.Delete(u);
                    scope.Complete();
                }
            }
            catch (Exception exp)
            {
                var res = userRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Group UpdateUserGroup(PartyId partyId, string description, Dictionary<int, bool> customActions)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var ug = userRep.GetUserGroupById(partyId);
                    var validSelectedActions =
                        ActionType.GetAll<ActionType>()
                                  .Where(c => customActions.Keys.Contains(int.Parse(c.Value)))
                                  .ToList();
                    var validCustomActions = validSelectedActions.ToDictionary(c => c,
                                                                               c => customActions[int.Parse(c.Value)]);
                    ug.Update(description, validCustomActions);
                    scope.Complete();
                    return ug;
                }
            }
            catch (Exception exp)
            {
                var res = userRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public Group AddUserGroup(PartyId partyId, string description, Dictionary<int, bool> customActions)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var ug = new Group(partyId, description);
                    assignCustomActionsToParty(ug, customActions);
                    userRep.Add(ug);
                    scope.Complete();
                    return ug;
                }
            }
            catch (Exception exp)
            {
                var res = userRep.TryConvertException(exp);
                if (res == null)
                    throw;
                throw res;
            }
        }

        public User GetLogonUser()
        {
            if (ClaimsPrincipal.Current == null)
                return null;
            return GetUser(new PartyId(ClaimsPrincipal.Current.Identity.Name));
        }

        public User GetCurrentUser()
        {
            if (ClaimsPrincipal.Current == null)
                return null;
            return GetUser(new PartyId(ClaimsPrincipal.Current.Identity.Name));
        }

        public List<User> GetPermittedWorkListFor(User user)
        {
            return userRep.GetPermittedWorkListFor(user);
        }


        private void assignUserGroupsToUser(User user, IList<PartyId> groupIdList)
        {
            foreach (var groupId in groupIdList)
            {
                var group = userRep.GetUserGroupById(groupId);
                user.AssignGroup(group);
            }
        }

        private void assignCustomActionsToParty(Party party, Dictionary<int,bool> customActionIdList)
        {
            foreach (var actId in customActionIdList)
            {
                ActionType act = ActionType.FromValue<ActionType>(actId.Key.ToString());
                party.AssignCustomAction(act,actId.Value);
            }
        }

       

    }
}
