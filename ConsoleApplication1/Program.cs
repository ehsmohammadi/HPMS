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
            var empConverter = new EmployeeConverter(new EmployeeDataProvider(), new EmployeeServiceWrapper(new UserProvider()));

            var period = new PeriodDTO();
            period.Id = 10;
            period.Name = "name";
            int convertedEmployee = 0;

            string res = "";

            var task2 = new Task(act =>
            {
                while (string.IsNullOrEmpty(res))
                {
                    if (convertedEmployee != empConverter.Result)
                        Console.WriteLine(empConverter.Result + "employee converted");
                    convertedEmployee = empConverter.Result;

                }
            }, null);

            task2.Start();

            var task = new Task(act =>
            {
                empConverter.ConvertEmployee(period);
            }, null);

            task.Start();



            res = Console.ReadLine();
            Console.ReadLine();


        }
    }
}
