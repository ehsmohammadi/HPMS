using System;
using System.Collections.Generic;
using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.PMS.API;
using MITD.PMS.Presentation.Contracts;
using System.Threading.Tasks;

namespace MITD.PMS.Integration.Domain
{
    public class EmployeeConverter
    {
        private IEmployeeDataProvider employeeDataProvider;
        private readonly IEmployeeServiceWrapper employeeService;
        public int Result { get; set; }


        public EmployeeConverter(IEmployeeDataProvider employeeDataProvider, IEmployeeServiceWrapper employeeService)
        {
            this.employeeDataProvider = employeeDataProvider;
            this.employeeService = employeeService;
            Result = 0;
        }


        private async Task<int> RunConvertEmployeesSync(PeriodDTO periodDto)
        {
            Task<int> GetEmployees = GetEmployeesAsync(periodDto);

            int EmployeeList = await GetEmployees;
            return EmployeeList;
        }

        private async Task<int> GetEmployeesAsync(PeriodDTO periodDto)
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
                         if (e != null)
                             throw new Exception("Error In Employee Converting");
                         Result++;

                     }, desEmployee);
                 //System.Threading.Thread.Sleep(500);
             }
             return Result;
         }



        public void ConvertEmployee(PeriodDTO periodDto)
        {
            RunConvertEmployeesSync(periodDto);            
        }

        public EmployeeDTO GetEmployeeDetail(int ID)
        {
            return new EmployeeDTO();
        }
    }
}
