using System.Collections.Generic;
using System.Net;
using MITD.PMS.ACL.SSO.UserManagement;
using MITD.Services;
using IUserManagementService = MITD.PMS.Application.Contracts.IUserManagementService;

namespace MITD.PMS.ACL.SSO
{
    public class UserManagementService : IUserManagementService
    {
        #region Fields
        private readonly IFaultExceptionAdapter errorAdapter;

        #endregion

        #region Constructors
        public UserManagementService(IFaultExceptionAdapter errorAdapter)
        {
            this.errorAdapter = errorAdapter;
        }
        #endregion

        #region Methods

        public IList<string> GetRolesForUser(string userName)
        {
            var client = new UserManagementServiceClient();
            return WcfClientHelper.CallMethod((c, u) => c.GetRolesForUser(userName), client, userName, errorAdapter);

        }

        public void ChangePassword(string username, string newPassword, string oldPassword)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;
            var client = new UserManagementServiceClient();

            WcfClientHelper.CallMethod((c, u, n, o) => c.SetPassword(username, newPassword, oldPassword), client,
                username, newPassword, oldPassword, errorAdapter);
        }


        #endregion

    }
}
