using System;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.Host.Console;
using MITD.PMS.Integration.PMS.API;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Entrance

            Console.ReadLine();

            #endregion

            #region Bootsrapper

            var bootstrapper=new Bootstrapper();
            bootstrapper.Execute();

            #endregion

            var period = new Period
            {
                Id = 1
            };

            #region Manager Progress

            var manager = ServiceLocator.Current.GetInstance<ConverterManager>();
            manager.Init(period);
            manager.Run();

            #endregion

            #region End

            Console.WriteLine("Progress started");
            Console.ReadLine();

            #endregion


        }
    }
}