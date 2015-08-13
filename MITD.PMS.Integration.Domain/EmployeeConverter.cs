using System;
using System.Collections.Generic;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class EmployeeConverter
    {
        private IEmployeeDataProvider employeeDataProvider;//= new EmployeeDataProvider();
        private readonly IEmployeeServiceWrapper employeeService;
        public int Result { get; set; }


        public EmployeeConverter(IEmployeeDataProvider employeeDataProvider, IEmployeeServiceWrapper employeeService)
        {
            this.employeeDataProvider = employeeDataProvider;
            this.employeeService = employeeService;
            Result = 0;
        }

        public void ConvertEmployee(PeriodDTO periodDto)
        {

            var idList = employeeDataProvider.GetEmployeeIds();

            foreach (var id in idList)
            {


                var personDetail = employeeDataProvider.GetEmployeeDetails(id);
                var desEmployee = new EmployeeDTO();
                desEmployee.FirstName = personDetail.Name;
                desEmployee.LastName = personDetail.Family;
                desEmployee.PersonnelNo = personDetail.PersonnelCode;
                desEmployee.PeriodId = periodDto.Id;

                desEmployee.CustomFields = new List<CustomFieldValueDTO>();

                employeeService.AddEmployee(
                    (r, e) =>
                    {
                        if(e!=null)
                            throw new Exception("bad shod");
                        Result++;

                    }, desEmployee);
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
