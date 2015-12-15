using System.Collections.Generic;
using System.Security.Claims;
using Castle.DynamicProxy;
using MITD.PMSSecurity.Domain;
using MITD.PMSSecurity.Domain.Model;
using MITD.PMSSecurity.Exceptions;

namespace MITD.PMS.Interface
{
    public class Interception : IInterceptor
    {

        public void Intercept(IInvocation invocation)
        {
            var securityService = SecurityServiceFacadeFactory.Create();
            try
            {
                var user = ClaimsPrincipal.Current;
                RequiredPermissionAttribute[] actionAttrList = (RequiredPermissionAttribute[])invocation.MethodInvocationTarget.GetCustomAttributes(typeof (RequiredPermissionAttribute), true);
                List<ActionType> requiredActions = new List<ActionType>();
                foreach (RequiredPermissionAttribute actionAttr in actionAttrList)
                {
                    requiredActions.Add(actionAttr.ActionType);
                }

                //if (securityService.IsAuthorized(invocation.Method.DeclaringType.Name, invocation.Method.Name, user))
                if (requiredActions.Count == 0 || securityService.IsAuthorized(user, requiredActions))
                {
                    invocation.Proceed();
                    //logServicesAccess(invocation, user);
                }
                else
                    throw new PMSSecurityAccessException();
            }
            finally
            {
                SecurityServiceFacadeFactory.Release(securityService);
            }

        }

        private void logServicesAccess(IInvocation invocation, ClaimsPrincipal user)
        {
            var srvManagerLog = LogServiceFacadeFactory.Create();
            try
            {
                var logSrv = srvManagerLog.GetService();

                string code = "Interceptor_AfterProceed";
                string title = "Clalling Facade Service " + invocation.Method.DeclaringType.Name;
                string userName = (user != null) ? user.Identity.Name : "";
                string logLevel = "AccessControl";
                string className = invocation.Method.DeclaringType.Name;
                string methodName = invocation.Method.Name;
                string messages = "user Authorized to call this method";
                logSrv.AddEventLog(title, code, logLevel, className, methodName, messages, userName);
            }
            finally
            {
                LogServiceFacadeFactory.Release(srvManagerLog);
            }
        }
    }
}
