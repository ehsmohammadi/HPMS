using MITD.PMS.Domain.Model;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndexPoints;
using MITD.PMS.Domain.Service;
using MITD.PMS.RuleContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MITD.PMSReport.Domain.Model;
using CalculationData = MITD.PMSReport.Domain.Model.CalculationData;
using Employee = MITD.PMS.Domain.Model.Employees.Employee;

namespace MITD.PMS.Application
{

    public class CalculationDataProvider : ICalculationDataProvider
    {
        private readonly IEmployeeRepository empRep;
        private readonly IPMSAdminService pmsAdminService;
        private readonly IJobIndexPointRepository jipRep;

        public CalculationDataProvider(IEmployeeRepository empRep, IPMSAdminService pmsAdminService, IJobIndexPointRepository jipRep)
        {
            this.empRep = empRep;
            this.pmsAdminService = pmsAdminService;
            this.jipRep = jipRep;
        }

        public RuleContracts.CalculationData Provide(Employee employee, out CalculationData calculationData, Calculation calculation, bool withCalculationPoint, CalculatorSession calculationSession)
        {
            empRep.Attach(employee);
            calculationData = empRep.ProvideDataForRule(employee, calculation.Id, withCalculationPoint);
            if (calculationSession.CalculationPoints != null && calculationSession.CalculationPoints.Any())
                calculationData.CalculationPoints.AddRange(calculationSession.CalculationPoints);
            var res = new MITD.PMS.RuleContracts.CalculationData
            {
                Employee = new PMS.RuleContracts.Employee
                {
                    FirstName = calculationData.employee.FirstName,
                    LastName = calculationData.employee.LastName,
                    EmployeeNo = calculationData.employee.Id.EmployeeNo
                },
                CustomFields =
                    (from k in
                         pmsAdminService.GetSharedEmployeeCustomField(
                             employee.CustomFieldValues.Select(m => m.Key).ToList())
                     join l in employee.CustomFieldValues on k.Id equals l.Key
                     select new { k, l }).ToDictionary(j => j.k.DictionaryName, j => j.l.Value),
                JobPositions = calculationData.JobPositions.Select(i => new JobPosition
                {
                    Name = i.Key.Name,
                    DictionaryName = i.Key.DictionaryName,
                   
                    Job = new Job { Name = i.Value.Job.Name, DictionaryName = i.Value.Job.DictionaryName },
                    CustomFields = (from k in pmsAdminService.GetSharedCutomFieldListForJob(i.Value.Job.SharedJob.Id,
                        i.Value.CustomFields.Select(m => m.JobCustomFieldId.SharedJobCustomFieldId).ToList())
                                    join l in i.Value.CustomFields on k.Id equals l.JobCustomFieldId.SharedJobCustomFieldId
                                    select new { k, l }).ToDictionary(j => j.k.DictionaryName, j => j.l.JobCustomFieldValue),
                    Indices = i.Value.Indices.ToDictionary(
                        j => new JobIndex
                        {
                            Name = j.Key.Name,
                            DictionaryName = j.Key.DictionaryName,
                            IsInquireable = j.Key.IsInquireable,
                            Group =
                                new JobIndexGroup { Name = j.Key.Group.Name, DictionaryName = j.Key.Group.DictionaryName },
                            CustomFields =
                                (from k in
                                     pmsAdminService.GetSharedCutomFieldListForJobIndex(j.Key.SharedJobIndexId,
                                         j.Key.CustomFieldValues.Select(m => m.Key).ToList())
                                 join l in j.Key.CustomFieldValues on k.Id equals l.Key
                                 select new { k, l }).ToDictionary(m => m.k.DictionaryName, m => m.l.Value)
                        },
                        j => (j.Value != null
                            ? j.Value.ToDictionary(
                                k => new PMS.RuleContracts.Employee
                                {
                                    FirstName = k.Key.FirstName,
                                    LastName = k.Key.LastName,
                                    EmployeeNo = k.Key.Id.EmployeeNo
                                },
                                k =>k.Value.Select(f=>
                                    new Inquiry
                                {
                                    JobPosition =
                                        new InquirerJobPosition
                                        {
                                            Name = f.JobPosition.Name,
                                            DictionaryName = f.JobPosition.DictionaryName,
                                            
                                            
                                        },
                                    Value = f.Point.JobIndexValue
                                }).ToList())
                            : null))
                }).ToList()
            };

            var ruleResult = addPreviousEmployeePointsToCalculationData(calculationData, calculationSession.CalculationPoints);
            res.Points = ruleResult;
            res.PathNo = calculationSession.PathNo;
            return res;
        }

        private RuleResult addPreviousEmployeePointsToCalculationData(CalculationData calculationData, IEnumerable<SummaryCalculationPoint> calculationPoints)
        {
            var points = calculationData.CalculationPoints;
            var res = new RuleResult();
            if (points == null || !points.Any())
                return res;

            res.CalculationPoints = points.Where(p => p is SummaryCalculationPoint).Select(p => new RulePoint
            {
                Name = p.Name,
                Value = p.Value,
                Final = p.IsFinal
            }).ToList();

            res.Results = points.Where(p => p is SummaryEmployeePoint).Select(p => new RulePoint
            {
                Name = p.Name,
                Value = p.Value,
                Final = p.IsFinal
            }).ToList();
            foreach (var jobPositionData in calculationData.JobPositions)
            {
                var jobPosition = jobPositionData.Key;
                var jobPositionResult = new JobPositionResult
                {
                    Results = points.Where(
                        p =>
                            p is SummaryJobPositionPoint &&
                            (p as SummaryJobPositionPoint).JobPositionId == jobPosition.Id).Select(p => new RulePoint
                            {
                                Name = p.Name,
                                Value = p.Value,
                                Final = p.IsFinal
                            }).ToList()
                };
                foreach (var jobIndex in jobPositionData.Value.Indices)
                {
                    jobPositionResult.IndexResults.Add(jobIndex.Key.DictionaryName, points.Where(
                        p =>
                            p is JobIndexPoint &&
                            (p as JobIndexPoint).JobPositionId == jobPosition.Id && (p as JobIndexPoint).JobIndexId == jobIndex.Key.Id).Select(p => new RulePoint
                            {
                                Name = p.Name,
                                Value = p.Value,
                                Final = p.IsFinal
                            }).ToList());
                }
                res.JobResults.Add(jobPosition.DictionaryName, jobPositionResult);
            }
            return res;
        }


        public CalculationPointPersistanceHolder Convert(RuleResult points, PMSReport.Domain.Model.CalculationData employeeData,
            PMS.Domain.Model.Employees.Employee employee, PMS.Domain.Model.Periods.Period period,
            PMS.Domain.Model.Calculations.Calculation calculation)
        {
            var res = new CalculationPointPersistanceHolder(jipRep, employeeData.CalculationPoints);
            foreach (var item in points.CalculationPoints)
            {
                res.AddSummeryCalculationPoint(period, calculation, item.Name, item.Value, item.Final);
            }

            foreach (var item in points.Results)
            {
                res.AddSummeryEmployeePoint(period, calculation, employee, item.Name, item.Value, item.Final);
            }
            var jprs = from i in points.JobResults
                       join j in employeeData.JobPositions
                       on i.Key equals j.Key.DictionaryName
                       select new { j, i.Value };
            foreach (var item in jprs)
            {
                foreach (var i in item.Value.Results)
                {
                    res.AddSummeryJobPositionPoint(period, calculation, employee, item.j.Key, i.Name, i.Value, i.Final);
                }
                var jirs = from n in item.Value.IndexResults
                           join m in item.j.Value.Indices.Select(k => k.Key)
                           on n.Key equals m.DictionaryName
                           select new { m, n.Value };
                foreach (var i in jirs)
                {
                    foreach (var k in i.Value)
                    {
                        res.AddJobIndexPoint(period, calculation, employee, item.j.Key, i.m, k.Name, k.Value, k.Final);
                    }
                }
            }

            return res;
        }
    }
}
