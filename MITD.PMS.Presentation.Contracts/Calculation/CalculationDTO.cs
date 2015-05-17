using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CalculationDTO 
    {
        public string PutActionName { get; set; }
        public bool IsDeterministic { get; set; }

        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string name;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نام الزامی است")]
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private long policyId;
        [Required(AllowEmptyStrings = false, ErrorMessage = "نظام محاسبه عملکرد الزامی است")]
        public long PolicyId
        {
            get { return policyId; }
            set { this.SetField(p => p.PolicyId, ref policyId, value); }
        }

        private string policyName;
        public string PolicyName
        {
            get { return policyName; }
            set { this.SetField(p => p.PolicyName, ref policyName, value); }
        }

        private long periodId;
        [Required(AllowEmptyStrings = false, ErrorMessage = "دوره الزامی است")]
        public long PeriodId
        {
            get { return periodId; }
            set { this.SetField(p => p.PeriodId, ref periodId, value); }
        }

        private string periodName;
        public string PeriodName
        {
            get { return periodName; }
            set { this.SetField(p => p.PeriodName, ref periodName, value); }
        }

        private long employeeCount;
        public long EmployeeCount
        {
            get { return employeeCount; }
            set { this.SetField(p => p.EmployeeCount, ref employeeCount, value); }
        }

        private long employeeCalculatedCount;
        public long EmployeeCalculatedCount
        {
            get { return employeeCalculatedCount; }
            set { this.SetField(p => p.EmployeeCalculatedCount, ref employeeCalculatedCount, value); }
        }

        public string EmployeeIdList { get; set; }
    }
}
