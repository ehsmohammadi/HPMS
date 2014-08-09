using MITD.PMS.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.ACL.SSO
{
    public class UserManagementService : IUserManagementService
    {

        public IList<string> GetRolesForUser(string userName)
        {
            var client = new UserManagement.UserManagementServiceClient();
            try
            {
                return client.GetRolesForUser(userName);
            }
            catch (CommunicationException)
            {
                client.Abort();
                throw;
            }
            catch (TimeoutException)
            {
                client.Abort();
                throw;
            }
            catch
            {
                if (client.State == CommunicationState.Faulted)
                {
                    client.Abort();
                }

                throw;
            }
            finally
            {
                try
                {
                    if (client.State != CommunicationState.Faulted)
                    {
                        client.Close();
                    }
                }
                catch
                {
                    client.Abort();
                }
            }
        }
    }
}
