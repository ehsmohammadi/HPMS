using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Data.EF;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            var res = Console.ReadLine();
            Console.ReadLine();

            var empConverter = new EmployeeConverter(new EmployeeDataProvider(), new EmployeeServiceWrapper(new UserProvider()));
            var jobConverter = new JobConverter(new JobDataProvider(), new JobServiceWrapper(new UserProvider()), new JobInPeriodServiceWrapper(new UserProvider()), new PeriodServiceWrapper(new UserProvider()));
            var periodService = new PeriodDataProvider(new PeriodServiceWrapper(new UserProvider()));//new PeriodServiceWrapper(new UserProvider()));
            //var CnManager = new ConverterManager(new CustomFieldServiceWrapper(new UserProvider()));



            var period = new Period
            {
                Id = 10,
                Name = "name"
            };




            //CnManager.Run(period);

            int convertedEmployee = 0;
            int convertedJob=0;

            //string res = "";

            //var ShowProgressTask = new Task(act =>
            //{
            //    while (string.IsNullOrEmpty(res))
            //    {
            //        if (convertedEmployee != empConverter.Result)
            //            Console.WriteLine(empConverter.Result + "employee converted");
            //        convertedEmployee = empConverter.Result;

            //        if (convertedJob != jobConverter.ProgressCount)
            //            Console.WriteLine(jobConverter.ProgressCount + "Job Converted");
            //        convertedJob = jobConverter.ProgressCount;
            //    }
            //}, null);

            //ShowProgressTask.Start();

            //var employeeConvertTask = new Task(act2 =>
            //{
            //    empConverter.ConvertEmployee(period);
            //}, null);

            ////employeeConvertTask.Start();

            //var jobConvertTask = new Task(act =>
            //{
            //    jobConverter.ConvertJob(period);
            //}, null);
            ////jobConvertTask.Start();


            //var GetAllPeriodsTask = new Task(act =>
            //{
            //    periodService.GetAllPeriods(
                    
            //        );
            //}, null);
            //GetAllPeriodsTask.Start();



            res = Console.ReadLine();
            Console.ReadLine();


        }
    }
}
