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
    public class ObjectWithDbId<T>
    {
        public virtual ObjectWithDbId<T> BID { get { return this; }}

        public T DbId{ get; set; }    
    }

    public class EntityWithDbId<T1, T2> : ObjectWithDbId<T1> where T2: ObjectWithDbId<T1>
    {
        
    }

    public class EmployeId: ObjectWithDbId<long>
    {
        public EmployeId(string EmpNo, int testID)
        {
            
        }
        public string EmpNo;
        public int TestId;

        //public override EmployeId BID { get { return this; } }
    }

    public class Employe : EntityWithDbId<long, EmployeId>
    {
        public Employe(string EmpNo, int testID)
        {
            //BID.   
        }
        
    }

    class Program
    {

        //static PeriodDTO period=new PeriodDTO();
        static void Main(string[] args)
        {
            //EntityWithDbId<long, ObjectWithDbId<long>> Emp = new EntityWithDbId<long, ObjectWithDbId<long>>("932074097",7);
            //Emp.DbId = 5;
            // Emp.DbId = 10;



            //var period = IntegrationHttpClient.Post<PeriodDTO, PeriodDTO>(new Uri("http://localhost:10653/"),
            //    "api/periods",
            //    new PeriodDTO
            //    {
            //        Name = "دوره مهر",
            //        StartDate = DateTime.Now,
            //        EndDate = DateTime.Now
            //    });

            //Console.WriteLine(period.Id);
            ////}



            //Console.ReadLine();


        }

        //public void AddPeriod(Action<PeriodDTO, Exception> action, PeriodDTO period)
        //{
        //    var url = string.Format(baseAddress);
        //    IntegrationWebClient.Post(new Uri(url, UriKind.Absolute), action, period, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}
    }
}
