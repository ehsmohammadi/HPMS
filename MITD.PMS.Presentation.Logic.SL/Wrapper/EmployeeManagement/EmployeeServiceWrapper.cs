using System;
using System.Collections.Generic;
using MITD.PMS.Presentation.Contracts;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Logic.Wrapper
{
    public partial class EmployeeServiceWrapper:IEmployeeServiceWrapper
    {
        private readonly IUserProvider userProvider;
        private readonly string baseAddress = PMSClientConfig.BaseApiAddress;

        public EmployeeServiceWrapper(IUserProvider userProvider)
        {
            this.userProvider = userProvider;
        }


        private string makeApiAdress(long periodId)
        {
            return "Periods/" + periodId + "/Employees";
        }
        private string makeEmployeeJobPositionsApiAdress(long periodId,string employeeNo)
        {
            return "Periods/" + periodId + "/Employees/"+employeeNo+"/JobPositions";
        }

        public void GetAllEmployees(Action<PageResultDTO<EmployeeDTOWithActions>, Exception> action, long periodId, int pageSize, int pageIndex)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId)+"?PageSize="+pageSize+"&PageIndex="+pageIndex);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllEmployees(Action<List<EmployeeDTO>, Exception> action, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId));
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllEmployees(Action<PageResultDTO<EmployeeDTOWithActions>, Exception> action, long periodId, EmployeeCriteria employeeCriteria, int pageSize, int pageIndex)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?PageSize=" + pageSize + "&PageIndex=" + pageIndex +
                              getFilterEmployee(employeeCriteria, "&"));
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetAllEmployeeNo(Action<List<string>, Exception> action, long periodId, EmployeeCriteria employeeCriteria)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) +
                             getFilterEmployee(employeeCriteria, "?"));
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void ConfirmEmployeeAboveMaxPoint(Action<Exception> action, string personnelNo, long periodId)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?employeeNo=" + personnelNo);
            WebClientHelper.Put<string, string>(new Uri(url, PMSClientConfig.UriKind), (res,exp)=>action(exp), "ConfirmEmployeeAboveMaxPoint", PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void ChangeEmployeePoint(Action<Exception> action, EmployeeDTO employee)
        {
            var url = string.Format(baseAddress + makeApiAdress(employee.PeriodId) + "?employeeNo=" + employee.PersonnelNo + "&point=" + employee.FinalPoint);
            WebClientHelper.Put<string, string>(new Uri(url, PMSClientConfig.UriKind), (res, exp) => action(exp), "ChangeEmployeePoint", PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        private string getFilterEmployee(EmployeeCriteria employeeCriteria,string x)
        {
            var qs = string.Empty;
            if (employeeCriteria == null)
                return string.Empty;
            if(!string.IsNullOrEmpty(employeeCriteria.FirstName))
            {
                qs += "FirstName:" + employeeCriteria.FirstName + ";";
            }
            if(!string.IsNullOrEmpty(employeeCriteria.LastName))
            {
                qs += "LastName:" + employeeCriteria.LastName + ";";
            }
            if(!string.IsNullOrEmpty(employeeCriteria.EmployeeNo))
            {
                qs += "EmployeeNo:" + employeeCriteria.EmployeeNo + ";";
            }
            if (employeeCriteria.UnitId != 0)
            {
                qs += "UnitId:" + employeeCriteria.UnitId + ";";
            }
            if (employeeCriteria.JobPositionId != 0)
            {
                qs += "JobPositionId:" + employeeCriteria.JobPositionId + ";";
            }
            if (string.IsNullOrEmpty(qs))
                return x + "Filter=''";
            return x+"Filter=" + qs;

        }

        public void DeleteEmployee(Action<string, Exception> action, long periodId,string personnelNo)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?PersonnelNo=" + personnelNo);
            WebClientHelper.Delete(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetEmployee(Action<EmployeeDTO, Exception> action, long periodId, string employeeNo)
        {
            var url = string.Format(baseAddress + makeApiAdress(periodId) + "?employeeNo=" + employeeNo);
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void AddEmployee(Action<EmployeeDTO, Exception> action, EmployeeDTO employee)
        {
            var url = string.Format(baseAddress + makeApiAdress(employee.PeriodId));
            WebClientHelper.Post(new Uri(url, PMSClientConfig.UriKind), action, employee, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void UpdateEmployee(Action<EmployeeDTO, Exception> action, EmployeeDTO employee)
        {
            var url = string.Format(baseAddress + makeApiAdress(employee.PeriodId));
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, employee, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetEmployeeJobPositionsInPeriod(Action<EmployeeJobPositionsDTO, Exception> action, string employeeNo, long periodId)
        {
            var url = string.Format(baseAddress + makeEmployeeJobPositionsApiAdress(periodId,employeeNo));
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        public void GetEmployeeUnitsInPeriod(Action<EmployeeUnitsDTO, Exception> action, string employeeNo, long periodId)
        {
            var url = string.Format(baseAddress + makeEmployeeUnitsApiAdress(periodId, employeeNo));
            WebClientHelper.Get(new Uri(url, PMSClientConfig.UriKind), action, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));

        }

        public void AssignJobPositionsToEmployee(Action<EmployeeJobPositionsDTO, Exception> action, long periodId, string employeeNo,
            EmployeeJobPositionsDTO employeeJobPositions)
        {
            var url = string.Format(baseAddress + makeEmployeeJobPositionsApiAdress(periodId, employeeNo));
            WebClientHelper.Put(new Uri(url, PMSClientConfig.UriKind), action, employeeJobPositions, PMSClientConfig.MsgFormat, PMSClientConfig.CreateHeaderDic(userProvider.Token));
        }

        private string makeEmployeeUnitsApiAdress(long periodId, string employeeNo)
        {
            return "Periods/" + periodId + "/Employees/" + employeeNo + "/Units";
        }
    }
}
