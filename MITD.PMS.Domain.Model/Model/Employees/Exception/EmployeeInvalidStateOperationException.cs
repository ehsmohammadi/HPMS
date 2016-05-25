using MITD.Core;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeeInvalidStateOperationException : EmployeeException, IInvalidStateOperationException
    {
        public EmployeeInvalidStateOperationException(string domainObjectName, string stateName, string operationName)
            : base("can not do  " + operationName + " in " + stateName + " state for  "+domainObjectName)
        {
            DomainObjectName = domainObjectName;
            StateName  = stateName;
            OperationName = operationName;
        }

        public EmployeeInvalidStateOperationException(string domainObjectName, string operationName)
            : base("can not do  " + operationName + " in current state for  " + domainObjectName)
        {
            DomainObjectName = domainObjectName;
            OperationName = operationName;
        }

        public string DomainObjectName { get; private set; }
        public string StateName { get; private set; }
        public string OperationName { get; private set; }
    }
}
