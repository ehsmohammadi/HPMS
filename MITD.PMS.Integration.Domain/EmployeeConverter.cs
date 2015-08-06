using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.Data.Contract.DTO;
using MITD.PMS.Integration.Data.EF;


namespace MITD.PMS.Integration.Domain
{
    public class EmployeeConverter
    {
        private IEmployeeDataProvider _EmployeeService;//= new EmployeeDataProvider();


        public EmployeeConverter(IEmployeeDataProvider EmployeeService)
        {
            _EmployeeService = EmployeeService;
        }

        public void InsertEmployees()
        {
            IList<long> IDs = _EmployeeService.GetEmployeeIds();

            foreach (var id in IDs)
            {
                EmployeeDTO PersonDetail = _EmployeeService.GetEmployeeDetails(id);

                // We can call PMS APIs for insert employee data to PMS Database
            }
        }
    }
}
