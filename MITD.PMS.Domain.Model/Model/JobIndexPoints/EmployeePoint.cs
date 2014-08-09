using MITD.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Periods;
using System;
namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    public abstract class EmployeePoint : CalculationPoint
    {
        protected readonly EmployeeId employeeId;

        protected EmployeePoint():base()
        {
        }
        protected EmployeePoint(CalculationPointId id,Period period,Employee employee, Calculation calculation, 
            string name, decimal value, bool isFinal=false):base(id,period,calculation,name,value,isFinal)
        {
           
            if (employee == null || employee.Id==null)
                throw new ArgumentNullException("employee");
            employeeId = employee.Id;
        }
        public virtual EmployeeId EmployeeId { get { return employeeId; } }

    }
}
