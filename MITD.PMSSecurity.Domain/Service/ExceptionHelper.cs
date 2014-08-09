using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.Core.Exceptions
{
    public class ExceptionHelper
    {
        public static string GetExceptionTextInfo(Exception exp, bool withInnerException)
        {
            Dictionary<string, object> expInfo = GetExceptionInfo(exp, withInnerException);
            return GetLineStringExpInfo(expInfo);
        }

        private static string GetLineStringExpInfo(Dictionary<string, object> expInfo)
        {
            string res = "";
            foreach (var k in expInfo.Keys)
            {
                if (k.ToLower() != "InnerException")
                    res += "\r\n" + k + ":" + "\n" + expInfo[k];
                else
                    res += "\r\n" + k + ":" + "\n" + GetLineStringExpInfo(expInfo[k] as Dictionary<string, object>);
            }
            return res + "\n\r==========================================\n\r";
        }

        public static Dictionary<string, object> GetExceptionInfo(Exception exp, bool withInnerException)
        {
            if (exp == null)
                return new Dictionary<string, object>();

            var expFields = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(exp.Message))
                expFields.Add("Title", exp.Message);

            if (!string.IsNullOrWhiteSpace(exp.HelpLink))
                expFields.Add("HelpLink", exp.HelpLink);

            if (!string.IsNullOrWhiteSpace(exp.HResult.ToString()))
                expFields.Add("HResult", exp.HResult.ToString());

            if (!string.IsNullOrWhiteSpace(exp.Source))
                expFields.Add("Source", exp.Source);

            if (!string.IsNullOrWhiteSpace(exp.StackTrace))
                expFields.Add("StackTrace", exp.StackTrace);

            if (exp.TargetSite != null)
                expFields.Add("TargetSite", exp.TargetSite.ToString());

            if (withInnerException && exp.InnerException != null)
                expFields.Add("InnerException", GetExceptionInfo(exp.InnerException, withInnerException));
            return expFields;

        }

    }
}
