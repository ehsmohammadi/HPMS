using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeCalculationResultDetailsDTO
    {

        private string calculationName;
        public string CalculationName
        {
            get { return calculationName; }
            set { this.SetField(p => p.CalculationName, ref calculationName, value); }
        }

        private string policyName;
        public string PolicyName
        {
            get { return policyName; }
            set { this.SetField(p => p.PolicyName, ref policyName, value); }
        }


        private string periodName;
        public string PeriodName
        {
            get { return periodName; }
            set { this.SetField(p => p.PeriodName, ref periodName, value); }
        }

        private string employeeName;
        public string EmployeeName
        {
            get { return employeeName; }
            set { this.SetField(p => p.EmployeeName, ref employeeName, value); }
        }

        private PageResultDTO<JobIndexValueDTO> employeeJobIndexValueList;
        public PageResultDTO<JobIndexValueDTO> EmployeeJobIndexValueList
        {
            get { return employeeJobIndexValueList; }
            set { this.SetField(p => p.EmployeeJobIndexValueList, ref employeeJobIndexValueList, value); }
        }
    }
}
