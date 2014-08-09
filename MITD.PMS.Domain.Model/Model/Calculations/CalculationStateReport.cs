using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MITD.PMS.Domain.Model.Calculations
{
    public class CalculationStateReport
    {
        private Calculation calculation;
        private CalculationProgress progress;
        private IReadOnlyList<string> messages = new List<string>();
        public Calculation Calculation { get { return calculation; } }
        public CalculationProgress Progress { get { return progress; } }
        public IReadOnlyList<string> Messages { get { return messages; } }
        public CalculationStateReport(Calculation calculation, CalculationProgress progress, IReadOnlyList<string> messages)
        {
            this.calculation = calculation;
            if (messages != null)
                this.messages = messages;
            else
            {
                if (calculation.CalculationResult != null)
                    this.messages = calculation.CalculationResult.Messages;
            }
            if (progress != null)
                this.progress = progress;
            else
            {
                this.progress = new CalculationProgress();
                if (calculation.CalculationResult != null)
                {
                    this.progress.SetProgress(calculation.CalculationResult.TotalEmployeesCount,
                        calculation.CalculationResult.EmployeesCalculatedCount);
                    if (calculation.CalculationResult.LastCalculatedPath.HasValue)
                        this.progress.SetLastCalculatedEmployee(calculation.CalculationResult.LastCalculatedEmployeeId,
                            calculation.CalculationResult.LastCalculatedPath.Value);
                }


            }
        }
    }
}
