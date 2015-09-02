using System;
using System.CodeDom;
using System.Collections.Generic;
namespace MITD.PMS.RuleContracts
{

    [Serializable]
    public class Employee
    {
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string EmployeeNo{ get; set; }
    }

    [Serializable]
    public class Inquiry
    {
        public string Value { get; set; }
        public InquirerJobPosition JobPosition { get; set; }
        
    }

    [Serializable]
    public class InquirerJobPosition
    {
        public string Name { get; set; }
        public string DictionaryName { get; set; }
        public int JobPositionLevel { get; set; }
    }

    [Serializable]
    public class JobPosition
    {
        public string Name { get; set; }
        public string DictionaryName { get; set; }
        public Job Job { get; set; }

        public Unit Unit { get; set; }
        public Dictionary<JobIndex, Dictionary<Employee, List<Inquiry>>> Indices { get; set; }
        public Dictionary<string,string> CustomFields { get; set; }
        public int WorkTimePercent { get; set; }
        public int Weight { get; set; }

    }

    [Serializable]
    public class Job
    {
        public string Name { get; set; }
        public string DictionaryName { get; set; }
    }

    [Serializable]
    public class Unit
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DictionaryName { get; set; }
    }

    [Serializable]
    public class JobIndexGroup
    {
        public string Name { get; set; }
        public string DictionaryName { get; set; }
    }

    [Serializable]
    public class JobIndex
    {
        public string Name { get; set; }
        public string DictionaryName { get; set; }
        public bool IsInquireable { get; set; }
        public JobIndexGroup Group { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
    }

    [Serializable]
    public class CalculationData
    {
        public List<JobPosition> JobPositions { get; set; }
        public Employee Employee { get; set; }
        public Dictionary<string, string> CustomFields { get; set; }
        public RuleResult Points { get; set; }
        public int PathNo { get; set; }
    }
}
