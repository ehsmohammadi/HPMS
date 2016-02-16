using System;
using MITD.PMS.Integration.Core;
using MITD.PMS.Integration.PMS.Contract;
using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Integration.PMS.API
{
    public class EmployeeServiceWrapper : IEmployeeServiceWrapper
    {
        private readonly IUserProvider userProvider;

        private Uri apiUri = new Uri(PMSClientConfig.BaseApiAddress);

        private string endpoint = "Employes";

        public EmployeeServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/Employees";
        }
        private string makeEmployeeJobPositionsApiAdress(long periodId, string employeeNo)
        {
            return "Periods/" + periodId + "/Employees/" + employeeNo + "/JobPositions";
        }

        public EmployeeDTO AddEmployee(EmployeeDTO employee)
        {
            endpoint = makeApiAdress(employee.PeriodId);
            return IntegrationHttpClient.Post<EmployeeDTO, EmployeeDTO>(apiUri, endpoint, employee);
        }

        public EmployeeJobPositionsDTO AssignJobPositionsToEmployee(long periodId, string employeeNo,
            EmployeeJobPositionsDTO employeeJobPositions)
        {
            endpoint = makeEmployeeJobPositionsApiAdress(periodId, employeeNo);
            return IntegrationHttpClient.Put<EmployeeJobPositionsDTO, EmployeeJobPositionsDTO>(apiUri, endpoint, employeeJobPositions);
            //var url = string.Format(baseAddress + makeEmployeeJobPositionsApiAdress(periodId, employeeNo));
            //IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, employeeJobPositions, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }




















        //private string makeEmployeeJobPositionsApiAdress(long periodId, string employeeNo)
        //{
        //    return "Periods/" + periodId + "/Employees/" + employeeNo + "/JobPositions";
        //}

        //public void GetAllEmployees(Action<PageResultDTO<EmployeeDTOWithActions>, Exception> action, long periodId, int pageSize, int pageIndex)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllEmployees(Action<List<EmployeeDTO>, Exception> action, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId));
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}



















        //public void GetAllEmployees(Action<PageResultDTO<EmployeeDTOWithActions>, Exception> action, long periodId, EmployeeCriteria employeeCriteria, int pageSize, int pageIndex)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex +
        //                      getFilterEmployee(employeeCriteria, "&"));
        //    WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetAllEmployeeNo(Action<List<string>, Exception> action, long periodId, EmployeeCriteria employeeCriteria)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) +
        //                     getFilterEmployee(employeeCriteria, "?"));
        //    WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //private string getFilterEmployee(EmployeeCriteria employeeCriteria, string x)
        //{
        //    var qs = string.Empty;
        //    if (employeeCriteria == null)
        //        return string.Empty;
        //    if (!string.IsNullOrEmpty(employeeCriteria.FirstName))
        //    {
        //        qs += "FirstName:" + employeeCriteria.FirstName + ";";
        //    }
        //    if (!string.IsNullOrEmpty(employeeCriteria.LastName))
        //    {
        //        qs += "LastName:" + employeeCriteria.LastName + ";";
        //    }
        //    if (!string.IsNullOrEmpty(employeeCriteria.EmployeeNo))
        //    {
        //        qs += "EmployeeNo:" + employeeCriteria.EmployeeNo + ";";
        //    }
        //    if (employeeCriteria.UnitId != 0)
        //    {
        //        qs += "UnitId:" + employeeCriteria.UnitId + ";";
        //    }
        //    if (employeeCriteria.JobPositionId != 0)
        //    {
        //        qs += "JobPositionId:" + employeeCriteria.JobPositionId + ";";
        //    }
        //    if (string.IsNullOrEmpty(qs))
        //        return x + "Filter=''";
        //    return x + "Filter=" + qs;

        //}









        //public void DeleteEmployee(Action<string, Exception> action, long periodId, string personnelNo)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?PersonnelNo=" + personnelNo);
        //    IntegrationWebClient.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetEmployee(Action<EmployeeDTO, Exception> action, long periodId, string employeeNo)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(periodId) + "?employeeNo=" + employeeNo);
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void AddEmployee(Action<EmployeeDTO, Exception> action, EmployeeDTO employee)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(employee.PeriodId));
        //    IntegrationWebClient.Post(new Uri(url, PMSClientConfig.UriKind), action, employee, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void UpdateEmployee(Action<EmployeeDTO, Exception> action, EmployeeDTO employee)
        //{
        //    var url = string.Format(baseAddress + makeApiAdress(employee.PeriodId));
        //    IntegrationWebClient.Put(new Uri(url, PMSClientConfig.UriKind), action, employee, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}

        //public void GetEmployeeJobPositionsInPeriod(Action<EmployeeJobPositionsDTO, Exception> action, string employeeNo, long periodId)
        //{
        //    var url = string.Format(baseAddress + makeEmployeeJobPositionsApiAdress(periodId, employeeNo));
        //    IntegrationWebClient.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        //}




    }
}
