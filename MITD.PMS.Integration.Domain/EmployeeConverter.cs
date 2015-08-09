using MITD.PMS.Integration.Data.Contract.DataProvider;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.Domain
{
    public class EmployeeConverter
    {
        private IEmployeeDataProvider employeeDataProvider;//= new EmployeeDataProvider();
        private readonly IPMSDataPusher _employeeDataPusher;


        public EmployeeConverter(IEmployeeDataProvider employeeDataProvider,IPMSDataPusher employeeDataPusher)
        {
            this.employeeDataProvider = employeeDataProvider;
            _employeeDataPusher = employeeDataPusher;
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
                _employeeDataPusher.insertEmployee(desEmployee);
            }
        }
    }
}
