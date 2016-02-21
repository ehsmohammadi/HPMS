using System;
using System.Collections.Generic;
using MITD.PMS.Integration.Core;

namespace MITD.PMS.Integration.PMS.API
{
    public class PMSClientConfig
    {
        public static string BaseApiAddress { get { return String.Format("{0}/api/", BaseApiSiteAddress); } }

        public static string BaseApiSiteAddress = "http://localhost:10653";
        public readonly static UriKind UriKind = UriKind.Absolute;
        public readonly static IntegrationWebClient.MessageFormat MsgFormat = IntegrationWebClient.MessageFormat.Json;

        public static Dictionary<string, string> CreateHeaderDic(string token)
        {
            return new Dictionary<string, string> { { "SilverLight", "1" }, { "Authorization", "Session " + token } };
        }

        //public static string BaseAddress
        //{
        //    get
        //    {
        //        return String.Format("{0}://{1}:{2}",
        //            MediaTypeNames.Application..Host.Source.Scheme,
        //            MediaTypeNames.Application.Current.Host.Source.Host,
        //            MediaTypeNames.Application.Current.Host.Source.Port);
        //    }
        //}
    }
}
