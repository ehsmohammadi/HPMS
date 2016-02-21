using MITD.Core;
using MITD.PMS.Integration.Domain;

namespace MITD.PMS.Integration.Host.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Entrance

            System.Console.ReadLine();
            System.Console.WriteLine("Progress started");
            System.Console.WriteLine("***********************************************************************");
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
            System.Console.WriteLine("***********************************************************************");
            System.Console.WriteLine("Progress finished");
            System.Console.ReadLine();

            #endregion
        }

    }
}