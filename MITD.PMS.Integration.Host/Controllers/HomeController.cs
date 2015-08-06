using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MITD.PMS.Integration.Contract.Period;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.EF;

namespace MITD.PMS.Integration.Host.Controllers
{
    public class HomeController : Controller
    {

        IPeriodDataProvider PeriodService = new PeriodDataProvider();
        

        //
        // GET: /Home/
        public ActionResult Index()
        {
            List<PeriodProperties> PeriodListModel = PeriodService.GetPeriodList();
            return View(PeriodListModel);
        }


        public ActionResult GetDataForConvert(string id)
        {
            //GetCurrentPeriodDetail
            var PeriodID = Convert.ToInt64(id);
            var PeriodInfo = PeriodService.GetPeriodInformation(PeriodID);

            return View(PeriodInfo);
        }


        
        public ActionResult GetEmployeeList(string id)
        {
            //GetCurrentPeriodDetail
            var PeriodID = Convert.ToInt64(id);
            var PeriodInfo = PeriodService.GetPeriodInformation(PeriodID);
            IEmployeeDataProvider EmployeeService= new EmployeeDataProvider();
            IOrganChartDataProvider OrganchartService = new OrganChartDataPrivider();
            var Service = new PeriodFunctions(PeriodInfo, EmployeeService, OrganchartService);

            Service.InsertEmployees();

            return View(PeriodInfo);
        }
	}
}