using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.Data.EF;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    class Program
    {
        //static PeriodDTO period=new PeriodDTO();
        static void Main(string[] args)
        {


            var period = IntegrationHttpClient.Post<PeriodDTO, PeriodDTO>(new Uri("http://localhost:10653/"),
                "api/periods",
                new PeriodDTO
                {
                    Name = "دوره مهر",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now
                });

            Console.WriteLine(period.Id);
            //}



            Console.ReadLine();


        }

        //public void AddPeriod(Action<PeriodDTO, Exception> action, PeriodDTO period)
        //{
        //    var url = string.Format(baseAddress);
        //    IntegrationWebClient.Post(new Uri(url, UriKind.Absolute), action, period, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}
    }
}
