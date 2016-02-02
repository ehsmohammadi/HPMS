using System;
using MITD.PMS.Integration.Data.Contract;
using MITD.PMS.Integration.Domain;
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

            #region Prepare Manager

            var userProvider = new UserProvider();

            var period = new Period
            {
                ID = 10,
                Name = "name"
            };

            #region Create UnitIndexConverter 

            var unitIndexService = new UnitIndexServiceWrapper(userProvider);
            var unitIndexInPeriodService = new UnitIndexInPeriodServiceWrapper(userProvider);
            var unitIndexDataProvider = Activator.CreateInstance<IUnitIndexDataProvider>();
            var unitIndexConverter = new UnitIndexConverter(unitIndexDataProvider, unitIndexService,
                unitIndexInPeriodService); 

            #endregion

            #endregion

            #region Manager Progress

            var manager = new ConverterManager(unitIndexConverter);
            manager.Init(period);
            manager.Run(); 

            #endregion

            #region End

            Console.WriteLine("This progress finished");
            Console.ReadLine(); 

            #endregion


        }
    }
}
