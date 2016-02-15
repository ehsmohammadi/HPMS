using System;
using MITD.Core;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.Host.Console;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Entrance

            Console.ReadLine();
            Console.WriteLine("Progress started");
            Console.WriteLine("***********************************************************************");
            #endregion

            #region Bootsrapper

            var bootstrapper = new Bootstrapper();
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

            //Task.Run(() => mainAsync(args)).Wait();
            //AsyncContext.Run(
            //    () => mainAsync(args));

            #region End
            Console.WriteLine("***********************************************************************");
            Console.WriteLine("Progress finished");
            Console.ReadLine();

            #endregion
        }

        static async void mainAsync(string[] args)
        {

            
        }
    }
}