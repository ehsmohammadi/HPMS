using MITD.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace MITD.PMS.Interface.Presentation.Host.Controllers
{
    public class DeploymentController : Controller
    {
        public ActionResult GetXapVersion(string fileName)
        {
            return this.Content(DeploymentServiceHelper.GetXapFileVersion(fileName));
        }

        [HttpPost]
        public ActionResult GetXapVersions(string fileNames)
        {
            var j = JArray.Parse(fileNames);
            var res = JsonConvert.DeserializeObject<string[]>(j.ToString());
            return this.Json(DeploymentServiceHelper.GetXapFileVersion(res));
        }
    }
}
