using System.Collections.Generic;
using System.Security.Claims;
using MITD.Core;
using MITD.PMSSecurity.Domain;

namespace MITD.PMSSecurity.Application.Contracts
{
    public interface ISecurityService : IService
    {
        bool IsAuthorized(List<ActionType> userActions, List<ActionType> methodRequiredActions);
        List<ActionType> GetAllAuthorizedActions(List<User> pmsUsers);
        User GetUser(PartyId userId);
        User AddUpdate(PartyId partyId, string firstName, string lastName, string email);
        User AddUser(PartyId partyId, string firstName, string lastName, string email, bool isActive, Dictionary<int, bool> customActions, List<PartyId> groups,List<PartyId> permittedWorkListUsers);
        User UpdateUser(PartyId partyId, string firstName, string lastName, string email, bool isActive,Dictionary<int,bool> customActions,List<PartyId> groups,List<PartyId> permittedWorkListUsers);
        void Delete(PartyId partyId);
        Group UpdateUserGroup(PartyId partyId, string description, Dictionary<int, bool> customActions);
        Group AddUserGroup(PartyId partyId, string description, Dictionary<int, bool> customActions);
        User GetLogonUser();
        User GetCurrentUser();
        List<User> GetPermittedWorkListFor(User user);
        
    }
}
