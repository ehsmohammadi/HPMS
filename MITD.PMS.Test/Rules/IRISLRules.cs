
using System;
using System.Collections.Generic;
using MITD.Core;
using MITD.Core.RuleEngine;
using MITD.PMS.RuleContracts;
using System.Linq;
using System.Globalization;

namespace MITD.Core.RuleEngine
{

    public static class Utils
    {
        public static RuleResult Res = new RuleResult();

        #region Function
        public static int IndexCount(JobPosition job, string indexCFName, string indexCFValue, string group)
        {
            return job.Indices.Count(j => j.Key.CustomFields.Any(k => k.Key == indexCFName && k.Value == indexCFValue) && j.Key.Group.DictionaryName == group);
        }

        public static string IndexGroup(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index)
        {
            return index.Key.Group.DictionaryName;
        }

        public static string IndexField(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string fieldName)
        {
            try
            {
                return index.Key.CustomFields[fieldName];
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, " شاخص " + fieldName);
            }
        }

        public static RulePoint AddEmployeePoint(JobPosition job, KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index,
            string name, decimal value, bool final = false)
        {
            var res = new RulePoint { Name = name, Value = value, Final = final };
            if (!Utils.Res.JobResults.Any(j => j.Key == job.DictionaryName))
                Utils.Res.JobResults.Add(job.DictionaryName, new JobPositionResult());
            if (!Utils.Res.JobResults[job.DictionaryName].IndexResults.Any(j => j.Key == index.Key.DictionaryName))
                Utils.Res.JobResults[job.DictionaryName].IndexResults.Add(index.Key.DictionaryName, new List<RulePoint>());
            Utils.Res.JobResults[job.DictionaryName].IndexResults[index.Key.DictionaryName].Add(res);
            return res;
        }

        public static RulePoint AddEmployeePoint(JobPosition job, string name, decimal value, bool final = false)
        {
            var res = new RulePoint { Name = name, Value = value, Final = final };
            if (!Utils.Res.JobResults.Any(j => j.Key == job.DictionaryName))
                Utils.Res.JobResults.Add(job.DictionaryName, new JobPositionResult());
            Utils.Res.JobResults[job.DictionaryName].Results.Add(res);
            return res;
        }

        public static RulePoint AddEmployeePoint(string name, decimal value, bool final = false)
        {

            var res = new RulePoint { Name = name, Value = value, Final = final };
            Utils.Res.Results.Add(res);
            return res;
        }


        public static RulePoint AddCalculationPoint(string name, decimal value, bool final = false)
        {
            try
            {
                var res = new RulePoint { Name = name, Value = value, Final = final };
                Utils.Res.CalculationPoints.Add(res);
                return res;
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, "اضافه کردن  مقدار محاسبه با عنوان " + name);
            }
        }


        public static RulePoint GetCalculationPoint(CalculationData data, string name)
        {
            try
            {
                return data.Points.CalculationPoints.SingleOrDefault(j => j.Name == name);
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, name);
            }
        }

        public static Exception exceptionConvertor(Exception ex, string keyName)
        {
            var strkeyNotFound = string.Format("خطا در دریافت اطلاعات مقدار محاسبه. مقداری با کلید {0} یافت نشد ", keyName) + "\r\n";
            var strkeyOutOfRange = string.Format("خطا در دریافت اطلاعات مقدار محاسبه.مقدار {0} در رنج قابل قبول نیست ", keyName) + "\r\n";
            var strOtherExp = string.Format("خطا در دریافت اطلاعات مقدار محاسبه با عنوان " + keyName) + "\r\n";

            if (ex is KeyNotFoundException)
                return new KeyNotFoundException(strkeyNotFound + ex.Message);
            if (ex is IndexOutOfRangeException)
                return new IndexOutOfRangeException(strkeyOutOfRange + ex.Message);

            return new Exception(strOtherExp + ex.Message);
        }


        public static RulePoint GetPoint(JobPosition job, string name)
        {
            try
            {
                if (!Utils.Res.JobResults.Any(j => j.Key == job.DictionaryName)) return null;
                return Utils.Res.JobResults[job.DictionaryName].Results.SingleOrDefault(j => j.Name == name);
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, name);
            }
        }

        public static RulePoint GetPoint(CalculationData data, JobPosition job, string name)
        {
            try
            {
                if (!data.Points.JobResults.Any(j => j.Key == job.DictionaryName)) return null;
                return Utils.Res.JobResults[job.DictionaryName].Results.SingleOrDefault(j => j.Name == name);
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, name);
            }
        }

        public static RulePoint GetPoint(JobPosition job, KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string name)
        {
            try
            {
                RulePoint res = null;
                if (Utils.Res.JobResults.ContainsKey(job.DictionaryName))
                    if (Utils.Res.JobResults[job.DictionaryName].IndexResults.ContainsKey(index.Key.DictionaryName))
                        res =
                            Utils.Res.JobResults[job.DictionaryName].IndexResults[index.Key.DictionaryName]
                                .SingleOrDefault(j => j.Name == name);
                return res;
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, name);
            }
        }

        public static RulePoint GetPoint(CalculationData data, JobPosition job,
                                         KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string name)
        {
            try
            {
                RulePoint res = null;
                if (data.Points.JobResults.ContainsKey(job.DictionaryName))
                    if (data.Points.JobResults[job.DictionaryName].IndexResults.ContainsKey(index.Key.DictionaryName))
                        res =
                            data.Points.JobResults[job.DictionaryName].IndexResults[index.Key.DictionaryName]
                                .SingleOrDefault(j => j.Name == name);
                return res;
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, name);
            }
        }

        public static RulePoint GetPoint(JobPosition job, string indexName, string name)
        {
            try
            {
                RulePoint res = null;
                if (Utils.Res.JobResults.ContainsKey(job.DictionaryName))
                    if (Utils.Res.JobResults[job.DictionaryName].IndexResults.ContainsKey(indexName))
                        res = Utils.Res.JobResults[job.DictionaryName].IndexResults[indexName].SingleOrDefault(j => j.Name == name);
                return res;
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, name);
            }
        }

        public static RulePoint GetPoint(CalculationData data, JobPosition job, string indexName, string name)
        {
            try
            {
                RulePoint res = null;
                if (data.Points.JobResults.ContainsKey(job.DictionaryName))
                    if (data.Points.JobResults[job.DictionaryName].IndexResults.ContainsKey(indexName))
                        res = data.Points.JobResults[job.DictionaryName].IndexResults[indexName].SingleOrDefault(j => j.Name == name);
                return res;
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, name);
            }
        }

        public static decimal GetInquiryByJobPositionName(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> jobIndex, string inquirerJobPositionName)
        {
            return Convert.ToDecimal(jobIndex.Value.SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.DictionaryName == inquirerJobPositionName).Value);
        }

        public static List<decimal> GetSubordinatesInquiryPointBy(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> jobIndex)
        {
            try
            {
                var valueList = jobIndex.Value.SelectMany(j => j.Value)
                    .Where(x => x.JobPosition.JobPositionLevel == 3).ToList();
                return valueList.Select(v => Convert.ToDecimal(v.Value)).ToList();

            }
            catch (Exception)
            {

                return new List<decimal>();
            }
        }

        public static decimal GetParentInquiryPointBy(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> jobIndex)
        {
            try
            {
                var point = jobIndex.Value.SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.JobPositionLevel == 1);
                return point == null ? 0 : Convert.ToDecimal(point.Value);
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public static decimal GetSelfInquiryPointBy(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> jobIndex)
        {
            try
            {
                var point = jobIndex.Value.SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.JobPositionLevel == 4);
                return point == null ? 0 : Convert.ToDecimal(point.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static decimal GetPoint(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index)
        {
            var parentPoint = Utils.GetParentInquiryPointBy(index);
            var selfPoint = Utils.GetSelfInquiryPointBy(index);
            var subordinates = Utils.GetSubordinatesInquiryPointBy(index);
            decimal subordinatesPoint = 0;
            //point validation rule difference of 40
            if (parentPoint != 0)
            {
                selfPoint = Math.Abs(selfPoint - parentPoint) < 40 ? selfPoint : 0;
                if (subordinates.Count > 0)
                {
                    decimal sumSubordinatePoint = 0;
                    decimal subordinateCount = 0;
                    foreach (var subordinatePoint in subordinates)
                    {
                        if (subordinatePoint > 0 && Math.Abs(subordinatePoint - parentPoint) < 40)
                        {
                            sumSubordinatePoint += subordinatePoint;
                            subordinateCount++;
                        }
                    }
                    subordinatesPoint = subordinateCount == 0 ? 0 : sumSubordinatePoint / subordinateCount;
                }
            }
            else
            {
                subordinatesPoint = !subordinates.Any(s => s > 0) ? 0 : subordinates.Where(s => s > 0).Sum() / subordinates.Count(s => s > 0);
            }

            if (((parentPoint != 0 ? 1 : 0) * 4 + (subordinatesPoint != 0 ? 1 : 0) * 1 + (selfPoint != 0 ? 1 : 0) * 1) != 0)
                return (parentPoint * 4 + subordinatesPoint * 1 + selfPoint * 1) /
                         ((parentPoint != 0 ? 1 : 0) * 4 + (subordinatesPoint != 0 ? 1 : 0) * 1 + (selfPoint != 0 ? 1 : 0) * 1);
            return 0;
        }

        public static decimal GetInquiryByEmployeeNoAndJobPositionName(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string inquirerEmployeeNo, string JobPositionName)
        {

            return Convert.ToDecimal(index.Value.Where(e => e.Key.EmployeeNo == inquirerEmployeeNo).SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.DictionaryName == JobPositionName).Value);


        }

        public static void Update(this List<Tuple<long, long, decimal>> tuples,
            Tuple<long, long, decimal> tuple, decimal value)
        {
            var index = tuples.FindIndex(c => c == tuple);
            tuples.Insert(index, Tuple.Create(tuples[index].Item1, tuples[index].Item2, value));
            tuples.RemoveAt(index + 1);


        }

        public static List<Tuple<long, long, decimal>> CalculatePoint(Tuple<long, long, decimal> tuple, List<Tuple<long, long, decimal>> tuples)
        {

            var childs = tuples.Where(c => c.Item1 == tuple.Item2).ToList();

            foreach (var child in childs)
            {
                var sum = childs.Sum(c => c.Item3);
                var average = sum / childs.Count();
                var cof = tuple.Item3 / average;
                var newValue = cof * child.Item3;
                tuples.Update(child, newValue);
                var tpl = tuples.Find(c => c.Item2 == child.Item2);
                CalculatePoint(tpl, tuples);
            }
            return tuples;
        }

        #endregion
    }

    public class RuleResultHelper : IRuleResult<RuleResult>
    {
        public RuleResult GetResult()
        {
            return Utils.Res;
        }
        public void Clear()
        {
            Utils.Res = new RuleResult();
        }
    }

    public class Rule10 : IRule<CalculationData>
    {
        public void Execute(CalculationData data)
        {

            if (data.PathNo != 1) return;
            decimal performancePoint = 0;
            decimal sumPerformanceGroupImportance = 0;

            foreach (var position in data.JobPositions)
            {

                var x = 0m;
                var y = 0m;

                if (Utils.GetCalculationPoint(data, position.Unit.ParentId + ";" + position.Unit.Id + "/UnitPoint") == null)
                {
                    foreach (var index in position.Unit.Indices)
                    {
                        x += Convert.ToDecimal(index.Value.Item2) * Convert.ToDecimal(index.Key.CustomFields["UnitIndexImportance"]);
                        y += Convert.ToDecimal(index.Key.CustomFields["UnitIndexImportance"]);

                    }
                    if (x == 0 || y == 0)
                        throw new Exception("Unit with Id and Name" + position.Unit.Id + " - " + position.Unit.Name + "has no indices or point");
                    var res = x / y;
                    Utils.AddCalculationPoint(position.Unit.ParentId + ";" + position.Unit.Id + "/UnitPoint", res);

                }


                foreach (var index in position.Indices)
                {
                    var point = Utils.GetPoint(index);
                    if (index.Key.Group.DictionaryName == "PerformanceGroup")
                    {
                        var jobindexImportance = Convert.ToDecimal(index.Key.CustomFields["JobIndexImportance"]);
                        if (point > 0)
                            sumPerformanceGroupImportance += jobindexImportance;
                        performancePoint = performancePoint + point * jobindexImportance;
                    }
                    Utils.AddEmployeePoint(position, index,
                        index.Key.Group.DictionaryName == "PerformanceGroup" ? "Performance-gross" : "Behavioural-gross",
                        point);
                }
                var finalPerformancePoint = 0m;
                if (sumPerformanceGroupImportance != 0)
                {
                    finalPerformancePoint = performancePoint / sumPerformanceGroupImportance;
                }
                Utils.AddEmployeePoint(position, "PerformanceIndices", finalPerformancePoint);

                Utils.AddCalculationPoint(data.Employee.EmployeeNo + "/" + position.Unit.Id + "/PerformanceIndex", finalPerformancePoint);


            }

        }


    }

    public class Rule11 : IRule<CalculationData>
    {
        public void Execute(CalculationData data)
        {
            if (data.PathNo != 2)
                return;
            var unitCalculationFlag = Utils.Res.CalculationPoints.SingleOrDefault(c => c.Name == "UnitCalculationFlag");
            if (unitCalculationFlag != null)
                return;

            var allstringUnitPoints = data.Points.CalculationPoints.Where(c => c.Name.Contains("UnitPoint")).ToList();
            var unitPoints = new List<Tuple<long, long, decimal>>();
            allstringUnitPoints.ForEach(c =>
            {
                var ids = c.Name.Split('/')[0].Split(';');
                unitPoints.Add(new Tuple<long, long, decimal>(Convert.ToInt64(ids[0]), Convert.ToInt64(ids[1]), c.Value));
            });

            var roots = unitPoints.Where(c => c.Item1 == 0).ToList();

            roots.ForEach(r =>
            {
                Utils.CalculatePoint(r, unitPoints);
            });

            unitPoints.ForEach(c =>
            {
                Utils.AddCalculationPoint(c.Item1 + ";" + c.Item2 + "/TotalPointUnit", c.Item3);
            });

            Utils.AddCalculationPoint("UnitCalculationFlag", 1);

        }

    }

    public class Rule12 : IRule<CalculationData>
    {
        public void Execute(CalculationData data)
        {

            if (data.PathNo != 2) return;
            decimal total = 0;


            foreach (var position in data.JobPositions)
            {
                decimal sumBehaviralPoint = 0;
                decimal sumIndexImportance = 0;
                decimal sumPerformanceGroupImportance = 0;
                decimal unitPerformanceAveragePoint = 0;
                //////////////////////////////////////////////////////////////
                var unitPerformancePoints =
                    data.Points.CalculationPoints.Where(c => c.Name.Contains("/" + position.Unit.Id + "/")).ToList();
                //todo:clean 
                if (!unitPerformancePoints.Any())
                    throw new Exception("unit performance points count is 0");

                var countForAvarage = unitPerformancePoints.Count(u => u.Value != 0);
                if (countForAvarage != 0)
                    unitPerformanceAveragePoint = unitPerformancePoints.Sum(up => up.Value) / countForAvarage;

                decimal unitPoint;
                try
                {
                    unitPoint = Utils.Res.CalculationPoints.Single(
                   c => c.Name == position.Unit.ParentId + ";" + position.Unit.Id + "/TotalPointUnit").Value;
                }
                catch (Exception ex)
                {

                    throw new Exception("Total Unit Point is not calculated " + position.Unit.ParentId + "--" + position.Unit.Id);
                }
                Utils.AddEmployeePoint(position, "finalunitPoint", unitPoint);

                decimal totalPerformancePoint = 0;

                var employeePerformancePoint =
                    unitPerformancePoints.Single(up => up.Name.Contains(data.Employee.EmployeeNo)).Value;

                try
                {
                    if (unitPerformanceAveragePoint != 0)
                        totalPerformancePoint =
                                   employeePerformancePoint * (unitPoint / unitPerformanceAveragePoint);

                }
                catch (Exception ex)
                {
                    throw new Exception("Total performance point is not calculated");
                }

                Utils.AddEmployeePoint(position, "finalPerformancePoint", totalPerformancePoint);

                foreach (var index in position.Indices)
                {
                    var point = Utils.GetPoint(index);
                    var jobindexImportance = Convert.ToDecimal(index.Key.CustomFields["JobIndexImportance"]);
                    if (point > 0)
                        sumIndexImportance += jobindexImportance;
                    if (index.Key.Group.DictionaryName == "BehaviouralGroup")
                    {

                        sumBehaviralPoint = sumBehaviralPoint + point * jobindexImportance;
                    }
                    if (index.Key.Group.DictionaryName == "PerformanceGroup")
                    {
                        if (point > 0)
                            sumPerformanceGroupImportance = sumPerformanceGroupImportance + jobindexImportance;
                    }

                }
                if (sumIndexImportance != 0)
                {
                    total = total +((sumBehaviralPoint + totalPerformancePoint*sumPerformanceGroupImportance)/sumIndexImportance);
                    Utils.AddEmployeePoint(position, "finalJob",(sumBehaviralPoint + totalPerformancePoint*sumPerformanceGroupImportance)/sumIndexImportance);

                    Utils.AddEmployeePoint(position, "rawFinalJob", (sumBehaviralPoint + employeePerformancePoint * sumPerformanceGroupImportance) / sumIndexImportance);
                }
                else
                    Utils.AddEmployeePoint(position, "finalJob", 0);
            }

            Utils.AddEmployeePoint("final", total / data.JobPositions.Count, true);

        }
    }

}