using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeePointUnCalculatedState : EmployeePointState
    {
        public EmployeePointUnCalculatedState()
            : base("1", "EmployeePointUnCalculatedState")
        {

        }

        public override void SetPoint(Employee employee, Period period, decimal point)
        {
            var finalPoint = point > 100 ? 100 : point;
            employee.UpdateFinalPoint(finalPoint);
            if (finalPoint > period.MaxFinalPoint)
                employee.EmployeePointState = new EmployeePointCalculatedWithAboveMaxPointState();
            else if (0 < finalPoint && finalPoint < period.MaxFinalPoint)
                employee.EmployeePointState = new EmployeePointCalculatedWithNormalPointState();
        }
    }


}

