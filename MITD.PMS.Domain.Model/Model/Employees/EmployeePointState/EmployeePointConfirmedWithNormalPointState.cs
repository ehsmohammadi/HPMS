using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeePointConfirmedWithNormalPointState : EmployeePointState
    {
        public EmployeePointConfirmedWithNormalPointState()
            : base("6", "EmployeePointConfirmedWithNormalPointState","تایید شده با نمره عادی")
        {
            
        }

        public override void ConfirmFinalPoint(Employee employee, Period period)
        {
            employee.EmployeePointState = new EmployeePointConfirmedWithNormalPointState();
        }

        public override void Rollback(Employee employee, Period period)
        {
            employee.EmployeePointState = new EmployeePointCalculatedWithNormalPointState(); 
        }
    }


}

