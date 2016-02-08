using System;
using System.Threading.Tasks;
using MITD.Core;
using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.Host.Console;
using MITD.PMS.Integration.PMS.API;
using Nito.AsyncEx;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Entrance

            Console.ReadLine();
            Console.WriteLine("Progress started");
            #endregion

            #region Bootsrapper

            var bootstrapper = new Bootstrapper();
            bootstrapper.Execute();

            #endregion

            Task.Run(() => mainAsync(args)).Wait();
            //AsyncContext.Run(
            //    () => mainAsync(args));



            #region End

            Console.WriteLine("Progress started");
            Console.ReadLine();

            #endregion
        }

        static async void mainAsync(string[] args)
        {

            var period = new Period
            {
                Id = 1
            };

            #region Manager Progress

            var manager = ServiceLocator.Current.GetInstance<ConverterManager>();
            manager.Init(period);
            await manager.Run();

            

            #endregion
        }
    }
}