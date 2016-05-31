using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeePointConfirmedWithAboveMaxPointState : EmployeePointState
    {
        public EmployeePointConfirmedWithAboveMaxPointState()
            : base("4", "EmployeePointConfirmedWithAboveMaxPointState","تایید شده با نمره بالاتر از حد نصاب")
        {
            
        }

        public override void Rollback(Employee employee, Period period)
        {
            employee.EmployeePointState=new EmployeePointCalculatedWithAboveMaxPointState();
        }

        public override void ConfirmFinalPoint(Employee employee, Period period)
        {
            employee.EmployeePointState = new EmployeePointConfirmedWithAboveMaxPointState();
        }
    }


}

