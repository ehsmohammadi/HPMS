using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.Employees
{
    public class EmployeePointCalculatedWithAboveMaxPointState : EmployeePointState
    {
        public EmployeePointCalculatedWithAboveMaxPointState()
            : base("3", "EmployeePointCalculatedWithAboveMaxPointState","محاسبه شده با نمره بالاتر از حد نصاب")
        {
            
        }

        public override void ConfirmAboveMaxEmployeePoint(Employee employee, Period period)
        {
            if(employee.FinalPoint>period.MaxFinalPoint)
                employee.EmployeePointState=new EmployeePointConfirmedWithAboveMaxPointState();
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
    }


}

