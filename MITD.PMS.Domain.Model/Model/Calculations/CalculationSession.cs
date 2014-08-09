using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Model.JobIndexPoints;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculatorSession
    {

        private object lockObjCalcFailed = new object();
        public List<SummaryCalculationPoint> CalculationPoints { get; private set; }
        public int PathNo { get; set; }
        private bool hasEmployeeCalculationFailed = false;
        public bool HasEmployeeCalculationFailed
        {
            get
            {
                lock (lockObjCalcFailed)
                {
                    return hasEmployeeCalculationFailed;
                }
            }
            set
            {
                lock (lockObjCalcFailed)
                {
                    hasEmployeeCalculationFailed = value;
                }
            }
        }


        public CalculatorSession()
        {
            CalculationPoints = new List<SummaryCalculationPoint>();
        }


        public void AddCalculationPoint(SummaryCalculationPoint point)
        {
            if (!CalculationPoints.Select(cp => cp.Id).Contains(point.Id))
                CalculationPoints.Add(point);
            else
                CalculationPoints.Single(cp => cp.Id == point.Id).SetValue(point.Value);
        }

        public void AddCalculationPoints(IEnumerable<CalculationPoint> points)
        {
            foreach (var point in points)
            {
                AddCalculationPoint(point as SummaryCalculationPoint);
            }
        }

    }
}
