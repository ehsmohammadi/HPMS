using MITD.Core;
using MITD.PMS.Domain.Model.Calculations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MITD.PMS.Domain.Model.Employees;

namespace MITD.PMS.Domain.Service
{
    public class CalculationProgress
    {
        private long totalItemPerCalculation = 0;
        private long itemsCalculated = 0;
        private EmployeeId lastCalculatedEmployeeId;
        private int? lastCalculatedPath;

        public long TotalItem
        {
            get { return totalItemPerCalculation; }
        }

        public long ItemsCalculated
        {
            get { return itemsCalculated; }
        }

        public EmployeeId LastCalculatedEmployeeId
        {
            get { return lastCalculatedEmployeeId; }
        }

        public int? LastCalculatedPath
        {
            get { return lastCalculatedPath; }
        }


        public int Percent
        {
            get { return totalItemPerCalculation == 0 ? 0 : Convert.ToInt32((itemsCalculated*100)/totalItemPerCalculation); }
        }

        public void SetProgress(long totalItem, long itemsCalculated)
        {
            this.totalItemPerCalculation = totalItem;
            this.itemsCalculated = itemsCalculated;
        }

        public void SetLastCalculatedEmployee(EmployeeId lastCalculatedEmployeeId, int lastCalculatedPath)
        {
            this.lastCalculatedEmployeeId = lastCalculatedEmployeeId;
            this.lastCalculatedPath = lastCalculatedPath;
        }

    }

    public interface IJobIndexPointCalculator : IService, IEventHandler<PathPersisteCompleted>
    {
        CalculatorSession CalculatorSession { get; }

        IReadOnlyList<string> Messages { get; }

        CalculationProgress Progress { get; }

        void Start(Calculation calculation);

        void Pause(Calculation calculation);

        void Stop(Calculation calculation);

        void Resume(Calculation calculation);
    }
}
