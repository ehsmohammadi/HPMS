using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MITD.PMS.Integration.Data.EF;
using MITD.PMS.Integration.Domain;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConvertEmployeeTest()
        {
            var empConverter =new EmployeeConverter(new EmployeeDataProvider(),new EmployeeServiceWrapper(new UserProvider()));

            var period = new PeriodDTO();
            period.Id = 10;
            period.Name = "name";


            empConverter.ConvertEmployee(period);

        }
    }
}
