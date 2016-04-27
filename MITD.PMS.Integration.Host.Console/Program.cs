using System;
using MITD.Core;
using MITD.PMS.Integration.Domain;

namespace MITD.PMS.Integration.Host.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Bootsrapper

            var bootstrapper = new Bootstrapper();
            bootstrapper.Execute();

            #endregion

            #region Entrance

            System.Console.ReadLine();
            System.Console.WriteLine("Progress started");
            System.Console.WriteLine("***********************************************************************");

            #endregion

            #region Manager Progress
            try
            {

                var manager = ServiceLocator.Current.GetInstance<ConverterManager>();
                manager.Init();
                manager.Run();
                System.Console.WriteLine("***********************************************************************");
                System.Console.WriteLine("Progress finished");
                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("********************* Exception ****************************************");
                System.Console.WriteLine(ex.Message+Environment.NewLine+ex.InnerException.Message);
                System.Console.ReadLine();
            }
            #endregion

        }

    }
}