using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Transactions;
using MITD.Core;
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

        public bool IsAuthorized(List<ActionType> userActions, List<ActionType> methodRequiredActions)
        {
            return securityCheckerService.IsAuthorized(userActions, methodRequiredActions);
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
            using (var scope = new TransactionScope())
            {
                var u = userRep.GetUserById(partyId);
                ((User)u).Update(firstName, lastName, email);
                scope.Complete();
                return u as User;
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
                    //var validSelectedActions =
                    //    ActionType.GetAll<ActionType>()
                    //              .Where(c => customActions.Keys.Contains(int.Parse(c.Value)))
                    //              .ToList();
                    //var validCustomActions = validSelectedActions.ToDictionary(c => c,
                    //                                                           c => customActions[int.Parse(c.Value)]);
                    var validSelectedActions = ActionTypeHelper.SelectActionTypes(customActions.Keys);
                    var validCustomActions = validSelectedActions.ToDictionary(c => c, c => customActions[(int)c]);

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
                ActionType act = (ActionType)actId.Key;
                party.AssignCustomAction(act,actId.Value);
            }
        }

        public void UpdateUserAccess(PartyId id, Dictionary<int, bool> customActions)
        {
            using (var scope = new TransactionScope())
            {
                //var ums = new UserManagementServiceClient();

                User user = userRep.GetUserById(id);
                user.Actions = new AdminUser(id, "", "", "").Actions;

                //var rols = ums.GetRolesForUser(u.PartyName);
                //var roleActions = securityCheckerService.GetAllAuthorizedActionTypesForRole(rols.ToList());
                var actionsFromRole = user.Actions;
                //var rep = ServiceLocator.Current.GetInstance<IPartyCustomActionRepository>();
                //using (var scope1 = new TransactionScope())
                //{
                //    rep.DeleteAllByPartyId(id);
                //    _unitOfWorkScope.Commit();
                //    scope1.Complete();
                //}

                user.UpdateCustomActions(customActions, user.Id, actionsFromRole);
                //user.Update();
                //_unitOfWorkScope.Commit();
                //scope.Complete();
            //}
                //userRep.Update(user);
                scope.Complete();
            }
        }

    }
}
