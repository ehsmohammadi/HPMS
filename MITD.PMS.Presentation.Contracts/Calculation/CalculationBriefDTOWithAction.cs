using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MITD.Presentation;

namespace MITD.PMS.Presentation.Contracts
{
    public partial class CalculationBriefDTOWithAction : IActionDTO
    {
       
        private long id;
        public long Id
        {
            get { return id; }
            set { this.SetField(p => p.Id, ref id, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { this.SetField(p => p.Name, ref name, value); }
        }

        private string policyName;
        public string PolicyName
        {
            get { return policyName; }
            set { this.SetField(p => p.PolicyName, ref policyName, value); }
        }

        private long employeeCount;
        public long EmployeeCount
        {
            get { return employeeCount; }
            set { this.SetField(p => p.EmployeeCount, ref employeeCount, value); }
        }

        private string stateName;
        public string StateName
        {
            get { return stateName; }
            set { this.SetField(p => p.StateName, ref stateName, value); }
        }
        
        public List<int> ActionCodes { get; set; }

        private string deterministicStatus;
        public string DeterministicStatus
        {
            get { return deterministicStatus; }
            set { this.SetField(p => p.DeterministicStatus, ref deterministicStatus, value); }
        }
    }
}
