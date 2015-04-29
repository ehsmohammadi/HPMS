using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using MITD.Core;
using MITD.Core.Exceptions;
using MITD.PMS.Application;
using MITD.PMS.Interface;
using MITD.PMSSecurity.Domain;  
using MITD.PMSSecurity.Domain.Logs;

namespace MITD.PMS.Service.Host
{
    public static class PMSApiExceptionAdapter
    {

        public static HttpResponseMessage ConverToHttpResponse(HttpActionExecutedContext context)
        {

            var exception = context.Exception; 
            var error = new HttpError();
            var dic=ExceptionConvertorService.Convert(exception);
            foreach (var item in dic)
            {
                error.Add(item.Key, item.Value);
            }
            return context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error);
        }

        private static void logException(Exception exp)
        {
            User user;
            var securityService = SecurityServiceFacadeFactory.Create();
            try
            {
                user = securityService.GetLogonUser();
            }
            finally
            {
                SecurityServiceFacadeFactory.Release(securityService);
            }

            var logServicelc = LogServiceFactory.Create();
            try
            {
                var logService = logServicelc.GetService();
                string code = "ServiceHost_HandleExp";
                string title = exp.Message;
                string messages = ExceptionHelper.GetExceptionTextInfo(exp, true);
                logService.AddExceptionLog(code, LogLevel.Error, user, "", "", title, messages);
            }
            finally
            {
                LogServiceFactory.Release(logServicelc);
            }

        }



    }
}