using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using MITD.PMS.Interface.Presentation.Host.Helper;

namespace MITD.PMS.Interface.Presentation.Host
{
    public partial class ReportPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            // Uncomment for local report
            var reportPath = Request["ReportPath"];
            //reportPath = "Reporting/Files/" + reportPath;
            //ReportViewer1.LocalReport.ReportPath = reportPath;
            //return;

            // Uncomment for server report
           var url = ConfigurationManager.AppSettings["ReportServerUrl"];
           ReportViewer1.ServerReport.ReportServerUrl = new Uri(url, UriKind.Absolute); 
            
            // 

           //var rvc = new ReportViewerCredentials("Administrator", "P@ssw0rd", "");
           //rvc.ReportServerUrl = ReportViewer1.ServerReport.ReportServerUrl;
           //ReportViewer1.ServerReport.ReportServerCredentials = rvc;

           // Uncomment for server report
           reportPath = Request["ReportPath"];
           reportPath = "/MITD.PMS.Reports/" + reportPath;
           ReportViewer1.ServerReport.ReportPath = reportPath;
            //

            #region Transaction
            //var transactionId = Request["TransactionId"];
            //var enUS = new System.Globalization.CultureInfo("en-US");
            ////if (transactionId.Length > 19)
            ////{
            ////    transactionId = transactionId.Substring(0, 19);
            ////    transactionId = transactionId.Trim();
            ////}
            //var resultingDate = string.IsNullOrEmpty(transactionId) ? "" : DateTime.ParseExact(transactionId, "M/d/yyyy h:m:s tt", enUS).ToString();
            //var employeeId = Request["EmployeeId"];
            //ReportViewer1.ServerReport.SetParameters(
            //    new ReportParameter[2] { new ReportParameter("TransactionId", resultingDate), new ReportParameter("EmployeeId", employeeId) });
            #endregion

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ReportPrintDocument rp = new ReportPrintDocument(ReportViewer1.ServerReport);
            rp.Print();  
        }
    }
}