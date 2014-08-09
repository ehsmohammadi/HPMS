using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class EmployeeCalculationResultDTO
    {
        private long calculationId;
        public long CalculationId
        {
            get { return calculationId; }
            set { this.SetField(p => p.CalculationId, ref calculationId, value); }
        }

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

        private PageResultDTO<JobIndexPointSummaryDTOWithAction> employeeCalcTotalScoreList;
        public PageResultDTO<JobIndexPointSummaryDTOWithAction> EmployeeCalcTotalScoreList
        {
            get { return employeeCalcTotalScoreList; }
            set { this.SetField(p => p.EmployeeCalcTotalScoreList, ref employeeCalcTotalScoreList, value); }
        }
    }
}
