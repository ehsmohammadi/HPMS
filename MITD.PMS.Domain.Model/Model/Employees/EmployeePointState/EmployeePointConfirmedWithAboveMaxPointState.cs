namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeePointConfirmedWithAboveMaxPointState : EmployeePointState
    {
        public EmployeePointConfirmedWithAboveMaxPointState()
            : base("4", "EmployeePointConfirmedWithAboveMaxPointState","تایید شده با نمره بالاتر از حد نصاب")
        {
            
        }

        public override void Rollback(Employee employee)
        {
            employee.EmployeePointState=new EmployeePointCalculatedWithAboveMaxPointState();
        }
    }


}

