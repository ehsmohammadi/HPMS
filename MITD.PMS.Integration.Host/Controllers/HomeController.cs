using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MITD.PMS.Integration.Data.EF;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Presentation.Contracts;


namespace MITD.PMS.Integration.Host.Controllers
{
    public class HomeController : Controller
    {
        //PeriodDataProvider periodService = new PeriodDataProvider();
        EmployeeConverter empConverter = new EmployeeConverter(new EmployeeDataProvider(), new EmployeeServiceWrapper(new UserProvider()));
        PeriodDataProvider periodService = new PeriodDataProvider(new PeriodServiceWrapper(new UserProvider()));
        JobConverter jobService = new JobConverter(new JobDataProvider(), new JobServiceWrapper(new UserProvider()), new JobInPeriodServiceWrapper(new UserProvider()), new PeriodServiceWrapper(new UserProvider()));
        JobIndexConverter jobIndexConverter = new JobIndexConverter(new JobIndexDataProvider(), new JobIndexServiceWrapper(new UserProvider()), new JobIndexInPeriodServiceWrapper(new UserProvider()), new PeriodServiceWrapper(new UserProvider()));


        private Boolean IsPeriodReadyForConvertData(Period PeriodInfo)
        {

            if (PeriodInfo.State.Name == "جدید")
                return true;
            else
                return false;
        }

        //
        // GET: /Home/
        public ActionResult Index()
        {
            var PeriodListModel = new List<Period> 
            {
                new Period
                {
                    ID=11,
                    Name="jhsdkfjhskdf",
                    State=new PeriodState{Name="جدید"}
                    
                },
            };
            //var PeriodListModel = periodService.GetAllPeriods().Result.Select(a => new Period { ID = a.Id, Name = a.Name, State = new PeriodState { Name=a.StateName} }).ToList();
            return View(PeriodListModel);
        }


        public ActionResult PeriodDetail(string id)
        {
            //GetCurrentPeriodDetail
            var PeriodID = Convert.ToInt64(id);
            var PeriodInfo = new Period
            {
                ID = 11,
                Name = "jhsdkfjhskdf",
                State = new PeriodState { Name = "جدید" }

            };

            return View(PeriodInfo);
        }

        public ActionResult GetJobs(string id)
        {
            //GetCurrentPeriodDetail
            var PeriodID = Convert.ToInt64(id);
            var PeriodInfo = new Period
            {
                ID = 10,
                Name = "jhsdkfjhskdf",
                State = new PeriodState { Name = "جدید" }

            };

            if (IsPeriodReadyForConvertData(PeriodInfo))
            {
                PeriodDTO pmsPeriodInfo = new PeriodDTO();
                pmsPeriodInfo.Name = PeriodInfo.Name;
                pmsPeriodInfo.Id = PeriodInfo.ID;
                pmsPeriodInfo.StateName = PeriodInfo.State.Name;
                try
                {
                    //var converterManager = new ConverterManager(new CustomFieldServiceWrapper(new UserProvider()));
                    //converterManager.Init(PeriodInfo);
                    //converterManager.CreateCustomFields();
                    //jobService.ConvertJob(pmsPeriodInfo);

                    //ViewBag.ResultMessage = jobService.ProgressCount.ToString() + " Job Has Been Converted!";
                }
                catch (Exception)
                {

                    ViewBag.ResultMessage = "Error In Converting!";
                }
            }

            return View("PeriodDetail", PeriodInfo);
        }


        public ActionResult GetEmployeeList(string id)
        {
            //GetCurrentPeriodDetail
            var PeriodID = Convert.ToInt64(id);
            var PeriodInfo = periodService.GetPeriodInformation(PeriodID);

            if (IsPeriodReadyForConvertData(PeriodInfo))
            {
                PeriodDTO pmsPeriodInfo = new PeriodDTO();
                pmsPeriodInfo.Name = PeriodInfo.Name;
                pmsPeriodInfo.Id = PeriodInfo.ID;
                pmsPeriodInfo.StateName = PeriodInfo.State.Name;

                try
                {
                    empConverter.ConvertEmployee(pmsPeriodInfo);

                    ViewBag.ResultMessage = empConverter.Result.ToString() + " Employee Has Been Converted!";

                }
                catch (Exception e)
                {
                    ViewBag.ResultMessage = "Error In Converting!";
                }
                
            }

            return View("PeriodDetail", PeriodInfo);
        }

        public ActionResult ConvertGeneralJobIndexes(string id)
        {

            //GetCurrentPeriodDetail
            var PeriodID = Convert.ToInt64(id);
            var PeriodInfo = periodService.GetPeriodInformation(PeriodID);

            if (IsPeriodReadyForConvertData(PeriodInfo))
            {
                PeriodDTO pmsPeriodInfo = new PeriodDTO();
                pmsPeriodInfo.Name = PeriodInfo.Name;
                pmsPeriodInfo.Id = PeriodInfo.ID;
                pmsPeriodInfo.StateName = PeriodInfo.State.Name;

                try
                {
                    jobIndexConverter.ConvertGeneralJobexes(pmsPeriodInfo);

                    ViewBag.ResultMessage = jobIndexConverter.ProgressCount.ToString() + " General Job Index Has Been Converted!";

                }
                catch (Exception e)
                {
                    ViewBag.ResultMessage = "Error In Converting!";
                }

            }

            return View("PeriodDetail", PeriodInfo);
        }


        public ActionResult ConvertExclusiveJobIndexes(string id)
        {

            //GetCurrentPeriodDetail
            var PeriodID = Convert.ToInt64(id);
            var PeriodInfo = periodService.GetPeriodInformation(PeriodID);

            if (IsPeriodReadyForConvertData(PeriodInfo))
            {
                PeriodDTO pmsPeriodInfo = new PeriodDTO();
                pmsPeriodInfo.Name = PeriodInfo.Name;
                pmsPeriodInfo.Id = PeriodInfo.ID;
                pmsPeriodInfo.StateName = PeriodInfo.State.Name;

                try
                {
                    jobIndexConverter.ConvertExclusiveJobexes(pmsPeriodInfo);

                    ViewBag.ResultMessage = jobIndexConverter.ProgressCount.ToString() + " Exclusive Job Index Has Been Converted!";

                }
                catch (Exception e)
                {
                    ViewBag.ResultMessage = "Error In Converting!";
                }

            }

            return View("PeriodDetail", PeriodInfo);
        }
	}
}