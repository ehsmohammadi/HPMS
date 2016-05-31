using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeePointCalculatedWithNormalPointState : EmployeePointState
    {
        public EmployeePointCalculatedWithNormalPointState()
            : base("2", "EmployeePointCalculatedWithNormalPointState","محاسبه شده با نمره عادی")
        {

        }

        public override void ChangeFinalPoint(Employee employee, Period period, decimal point)
        {
            var finalPoint = point > 100 ? 100 : point;
            employee.UpdateFinalPoint(finalPoint);
            if (finalPoint > period.MaxFinalPoint)
                employee.EmployeePointState = new EmployeePointCalculatedWithAboveMaxPointState();
            else if (0 < finalPoint && finalPoint < period.MaxFinalPoint)
                employee.EmployeePointState = new EmployeePointCalculatedWithNormalPointState();
        }

        public override void ConfirmFinalPoint(Employee employee, Period period)
        {
            employee.EmployeePointState=new EmployeePointConfirmedWithNormalPointState();
        }
    }


}

