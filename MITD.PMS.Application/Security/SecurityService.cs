using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Transactions;
using MITD.PMS.Common.Utilities;
using MITD.PMSSecurity.Application.Contracts;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Service;

namespace MITD.PMSSecurity.Application
{
    public class SecurityService : ISecurityService
    {
        private readonly IUserRepository userRep;
        private readonly ISecurityCheckerService securityCheckerService;
        private readonly IEmailManager emailManager;

        public SecurityService(IUserRepository userRep,ISecurityCheckerService securityCheckerService,IEmailManager emailManager )
        {
            this.userRep = userRep;
            this.securityCheckerService = securityCheckerService;
            this.emailManager = emailManager;
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
            var u = GetUser(partyId) == null ? AddUser(partyId,firstName,lastName,email) : UpdateUser(partyId,firstName,lastName,email);
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

        public void UpdateUserProfile(PartyId partyId, string email)
        {
            using (var scope = new TransactionScope())
            {
                var u = userRep.GetUserById(partyId);
                ((User)u).UpdateProfile( email,emailManager);
                scope.Complete();

            }

        }

        public bool VerifyEmail(string veriCode)
        {
            using (var scope = new TransactionScope())
            {
                
                var user = userRep.GetUserByVerificationCode(veriCode);
                if (user == null)
                    return false;
                user.VerifyEmail();
                //((User)u).UpdateProfile(email, emailManager);
                scope.Complete();
                return true;

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
                User user = userRep.GetUserById(id);
                user.Actions = new SuperAdminUser(id, "", "", "").Actions;
                var actionsFromRole = user.Actions;
                user.UpdateCustomActions(customActions, user.Id, actionsFromRole);
                scope.Complete();
            }
        }


    }
}
