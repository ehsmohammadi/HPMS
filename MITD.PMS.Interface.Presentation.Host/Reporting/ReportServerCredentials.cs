using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Net;

namespace SIC.PayRoll.ProtoTypes.Host
{
    public class ReportViewerCredentials : IReportServerCredentials
    {


        private string _username;
        private string _password;
        private string _domain;

        public Uri ReportServerUrl;

        public ReportViewerCredentials(string username, string password, string domain)
        {
            _username = username;
            _password = password;
            _domain = domain;
        }

        #region IReportServerCredentials Members

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get
            {

                return null;
            }
        }

        public System.Net.ICredentials NetworkCredentials
        {


            get
            {

                System.Net.NetworkCredential nc = new
                     NetworkCredential(_username, _password, _domain);
                //CredentialCache cc = new CredentialCache();
                //cc.Add(ReportServerUrl, "Negotiate", nc);
                //return cc;
                return nc;

            }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = _username;
            password = _password;
            authority = _domain; 
            return false;
        }
        #endregion
    }
}