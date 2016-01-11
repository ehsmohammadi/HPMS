using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Castle.DynamicProxy;
using MITD.PMS.Presentation.Contracts;
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
                List<ActionType> requiredActions = GetRequiredActionsForInvocation(invocation);
                if (requiredActions.Count == 0 ||
                    securityService.IsAuthorized(user, requiredActions))
                {
                    invocation.Proceed();

                    FilterActionsFromPageResultDtosBasedOnUserPermissions(
                        invocation.ReturnValue, securityService.GetUserAuthorizedActions(user));

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

        private List<ActionType> GetRequiredActionsForInvocation(IInvocation invocation)
        {
            RequiredPermissionAttribute[] actionAttrList =
                   (RequiredPermissionAttribute[])invocation.MethodInvocationTarget.GetCustomAttributes(
                   typeof(RequiredPermissionAttribute), false);
            List<ActionType> requiredActions = new List<ActionType>();
            foreach (RequiredPermissionAttribute actionAttr in actionAttrList)
            {
                requiredActions.Add(actionAttr.ActionType);
            }
            return requiredActions;
        }

        private void FilterActionsFromPageResultDtosBasedOnUserPermissions(object pageResult, List<ActionType> authorizedActionsForUser)
        {
            if (pageResult != null && pageResult.GetType().IsGenericType &&
                        pageResult.GetType().GetGenericTypeDefinition() == typeof(PageResultDTO<>))
            {
                Type pageResultDtoType = pageResult.GetType().GetGenericTypeDefinition();
                Type dtoWithActionType = pageResult.GetType().GetGenericArguments()[0];
                Type finalType = pageResultDtoType.MakeGenericType(dtoWithActionType);

                var result = finalType.GetProperty("Result").GetValue(pageResult);
                foreach (IActionDTO item in (IEnumerable)result)
                {
                    item.ActionCodes = item.ActionCodes.Where(a => authorizedActionsForUser.Contains((ActionType)a)).ToList();
                }
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
