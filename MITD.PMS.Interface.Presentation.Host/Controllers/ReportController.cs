using MITD.PMS.Presentation.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MITD.PMS.Interface.Presentation.Host.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/

        public ActionResult Index()
        {
            ReportingService2010.ReportingService2010 rs = new ReportingService2010.ReportingService2010();
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Url = System.Configuration.ConfigurationManager.AppSettings["ReportServerUrl"] + "/ReportService2010.asmx";
            var rootPath = System.Configuration.ConfigurationManager.AppSettings["ReportRootPath"];
            ReportingService2010.CatalogItem[] items = rs.ListChildren(rootPath, true);


            return Json(items.Where(i => i.TypeName == "Folder" || i.TypeName == "Report").Select(i => new ReportDTO
            {
                Name = i.Name,
                TypeName = i.TypeName,
                Description = i.Description,
                Path = i.Path.Substring(rootPath.Length, i.Path.Length - i.Name.Length - rootPath.Length)
            }), JsonRequestBehavior.AllowGet);

            //List<ReportDTO> reports = new List<ReportDTO>();
            //foreach (string reportFilePath in Directory.GetFiles(Server.MapPath("~/Reporting/Files")))
            //{
            //    reports.Add(new ReportDTO() { Name = Path.GetFileName(reportFilePath), Path = "/", TypeName = "Report", Description = Path.GetFileNameWithoutExtension(reportFilePath) });
            //}
            //return Json(reports, JsonRequestBehavior.AllowGet);
        }

    }
}
