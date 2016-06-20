using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeePointConfirmedWithMaxPointState : EmployeePointState
    {
        public EmployeePointConfirmedWithMaxPointState()
            : base("5", "EmployeePointConfirmedWithMaxPointState","تایید شده با نمره حد نصاب")
        {
            
        }

        public override void ConfirmFinalPoint(Employee employee, Period period)
        {
            employee.EmployeePointState=new EmployeePointConfirmedWithMaxPointState();
        }
    }


}

