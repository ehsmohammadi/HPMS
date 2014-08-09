using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.RuleContracts
{
    [Serializable]
    public class RulePoint
    {
        public decimal Value { get; set; }
        public string Name { get; set; }
        public bool Final { get; set; }
    }

    [Serializable]
    public class JobPositionResult
    {
        public JobPositionResult()
        {
            IndexResults = new Dictionary<string, List<RulePoint>>();
            Results = new List<RulePoint>();
        }
        public Dictionary<string, List<RulePoint>> IndexResults { get; set; }
        public List<RulePoint> Results { get; set; }
    }

    [Serializable]
    public class RuleResult
    {
        public RuleResult()
        {
            JobResults = new Dictionary<string, JobPositionResult>();
            Results = new List<RulePoint>();
            CalculationPoints = new List<RulePoint>();
        }
        public Dictionary<string, JobPositionResult> JobResults { get; set; }
        public List<RulePoint> Results { get; set; }
        public List<RulePoint> CalculationPoints { get; set; }
    }

}
