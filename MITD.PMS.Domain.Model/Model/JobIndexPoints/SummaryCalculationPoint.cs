using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    public class SummaryCalculationPoint : CalculationPoint
    {
        protected SummaryCalculationPoint()
            : base()
        {
        }
        public SummaryCalculationPoint(CalculationPointId id, Period period, 
            Calculation calculation,string name, decimal value, bool isFinal=false)
            : base(id,period,calculation, name, value, isFinal)
        {
        }
    }
}
