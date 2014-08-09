using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;

namespace MITD.PMS.Domain.Model.JobIndexPoints
{
    public class CalculationPointPersistanceHolder
    {
        private readonly IJobIndexPointRepository jobIndexPointRep;
        private readonly EmployeeId employeeId;
        private readonly CalculationId calculationId;
        private readonly IList<CalculationPoint> savedCalculationPoints;
        public Dictionary<CalculationPointId, decimal> EmployeePointsForUpdate { get; private set; }
        public List<CalculationPoint> EmployeePointsForAdd { get; private set; }
        public List<CalculationPoint> CalculationPoints { get; private set; }

        

        public CalculationPointPersistanceHolder(IJobIndexPointRepository jobIndexPointRep, IEnumerable<CalculationPoint> savedCalculationPoints)
        {
            this.jobIndexPointRep = jobIndexPointRep;
            if (savedCalculationPoints == null)
                this.savedCalculationPoints = new List<CalculationPoint>();
            this.savedCalculationPoints = savedCalculationPoints.ToList();
            EmployeePointsForUpdate = new Dictionary<CalculationPointId, decimal>();
            EmployeePointsForAdd = new List<CalculationPoint>();
            CalculationPoints = new List<CalculationPoint>();
        }



        public void AddSummeryCalculationPoint(Period period, Calculation calculation, string name, decimal value,
            bool isFinal)
        {
            var oldpoint = savedCalculationPoints.OfType<SummaryCalculationPoint>().SingleOrDefault(
                cp => cp.Name == name);
            if (oldpoint != null)
                CalculationPoints.Add(new SummaryCalculationPoint(oldpoint.Id,period,calculation,name,value,isFinal));
            else
                CalculationPoints.Add(new SummaryCalculationPoint(jobIndexPointRep.GetNextId(), period, calculation, name, value, isFinal));

        }

        public void AddSummeryEmployeePoint(Period period, Calculation calculation, Employee employee, string name, decimal value,
            bool isFinal)
        {
            var oldpoint = savedCalculationPoints.OfType<SummaryEmployeePoint>().SingleOrDefault(
                cp => cp.Name == name);
            if (oldpoint != null)
                EmployeePointsForUpdate.Add(oldpoint.Id, value);
            else
                EmployeePointsForAdd.Add(new SummaryEmployeePoint(
                    jobIndexPointRep.GetNextId(), period, employee, calculation, name, value, isFinal));

        }

        public void AddSummeryJobPositionPoint(Period period, Calculation calculation, Employee employee, JobPosition jobPosition, string name, decimal value,
            bool isFinal)
        {

            var oldpoint = savedCalculationPoints.OfType<SummaryJobPositionPoint>().SingleOrDefault(
                cp => cp.JobPositionId == jobPosition.Id && cp.Name == name);
            if (oldpoint != null)
                EmployeePointsForUpdate.Add(oldpoint.Id, value);
            else
                EmployeePointsForAdd.Add(new SummaryJobPositionPoint(
                    jobIndexPointRep.GetNextId(), period, employee, calculation,jobPosition, name, value, isFinal));

        }

        public void AddJobIndexPoint(Period period, Calculation calculation, Employee employee, JobPosition jobPosition, JobIndex jobIndex, string name, decimal value,
            bool isFinal)
        {
            var oldpoint = savedCalculationPoints.OfType<JobIndexPoint>().SingleOrDefault(
                cp =>
                    cp.JobPositionId == jobPosition.Id &&
                    cp.JobIndexId == jobIndex.Id && cp.Name == name);
            if (oldpoint != null)
                EmployeePointsForUpdate.Add(oldpoint.Id, value);
            else
                EmployeePointsForAdd.Add(new JobIndexPoint(
                    jobIndexPointRep.GetNextId(), period, employee, calculation, jobPosition,jobIndex, name, value, isFinal));
        }
    }
}
