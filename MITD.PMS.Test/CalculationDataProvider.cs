using MITD.PMS.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Service;
using MITD.PMS.RuleContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Employee = MITD.PMS.Domain.Model.Employees.Employee;

namespace MITD.PMS.Test
{
    class CalculationDataProvider : ICalculationDataProvider
    {
        private readonly IEmployeeRepository empRep;
        public CalculationDataProvider(IEmployeeRepository empRep)
        {
            this.empRep = empRep;
        }


        public CalculationData Provide(Employee employee, out PMSReport.Domain.Model.CalculationData employeeData, Calculation calculation, bool withCalculationPoint, CalculatorSession pathNo)
        {
            throw new NotImplementedException();
        }

        public CalculationPointPersistanceHolder Convert(RuleResult points, PMSReport.Domain.Model.CalculationData employeeData, 
            Domain.Model.Employees.Employee employee, Domain.Model.Periods.Period period, Domain.Model.Calculations.Calculation calculation)
        {
            throw new NotImplementedException();
        }
    }
}
