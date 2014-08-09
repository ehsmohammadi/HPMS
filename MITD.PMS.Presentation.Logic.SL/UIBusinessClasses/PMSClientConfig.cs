using System;
using System.Collections.Generic;
using System.Windows;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic
{
    public class PMSClientConfig
    {
        public static string BaseApiAddress { get { return String.Format("{0}/api/", BaseApiSiteAddress); } }
        public static string BaseApiSiteAddress;
        public readonly static UriKind UriKind = UriKind.Absolute;
        public readonly static WebClientHelper.MessageFormat MsgFormat = WebClientHelper.MessageFormat.Json;

        public static Dictionary<string, string> CreateHeaderDic(string token)
        {
            return new Dictionary<string, string> { { "SilverLight", "1" }, { "Authorization", "Session " + token } };
        }

        public static string BaseAddress
        {
            get
            {
                return String.Format("{0}://{1}:{2}",
                    Application.Current.Host.Source.Scheme,
                    Application.Current.Host.Source.Host,
                    Application.Current.Host.Source.Port);
            }
        }
    }
}
