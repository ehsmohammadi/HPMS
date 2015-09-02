
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

        public static decimal GetInquiryByJobPositionlevel(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> jobIndex, int level)
        {
            try
            {
                return Convert.ToDecimal(jobIndex.Value.SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.JobPositionLevel == level).Value);
            }
            catch (Exception)
            {

                return 0;
            }
        }



        public static decimal GetInquiryByEmployeeNoAndJobPositionName(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string inquirerEmployeeNo, string JobPositionName)
        {

            return Convert.ToDecimal(index.Value.Where(e => e.Key.EmployeeNo == inquirerEmployeeNo).SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.DictionaryName == JobPositionName).Value);


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
            decimal total = 0;
            decimal performancePoint = 0;
            decimal sumPerformanceGroupImportance = 0;
            foreach (var position in data.JobPositions)
            {
                foreach (var index in position.Indices)
                {
                    var parentPoint = Utils.GetInquiryByJobPositionlevel(index, 1);
                    var childPoint = Utils.GetInquiryByJobPositionlevel(index, 3);
                    var selfPoint = Utils.GetInquiryByJobPositionlevel(index, 4);
                    var point = (parentPoint * 4 + childPoint * 1 + selfPoint * 1) / ((parentPoint != 0 ? 1 : 0) * 4 + (childPoint != 0 ? 1 : 0) * 1 + (selfPoint != 0 ? 1 : 0) * 1);
                    if (index.Key.Group.DictionaryName == "PerformanceGroup")
                    {
                        var jobindexImportance = Convert.ToDecimal(index.Key.CustomFields["JobIndexImportance"]);
                        sumPerformanceGroupImportance += jobindexImportance;
                        performancePoint = performancePoint + point * jobindexImportance;
                    }
                    total = total + point;
                    Utils.AddEmployeePoint(position, index, "gross", point);

                }
                var finalPerformancePoint = performancePoint/sumPerformanceGroupImportance;
                Utils.AddEmployeePoint(position, "PerformanceIndices", finalPerformancePoint);
                Utils.AddCalculationPoint(data.Employee.EmployeeNo+"/"+position.Unit.Id + "/PerformanceIndex", finalPerformancePoint);
            }

            

            ////decimal z = .4m;
            ////Utils.AddCalculationPoint("z", z);
            //foreach (var job in data.JobPositions)
            //{
            //    if (Utils.GetPoint(job, "a1") != null) break;
            //    decimal notPresentIndexesCount = 1;
            //    if (job.CustomFields["PeriodicMaintenance"] == "0")
            //        notPresentIndexesCount = 2;

            //    decimal a1 = Utils.IndexCount(job, "Importance", "1", "General");


            //    //محاسبه عدد وزنی شاخص های عمومی

            //    Utils.AddEmployeePoint(job, "a1", a1);

            //}

        }
    }
    public class Rule11 : IRule<CalculationData>
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
                //////////////////////////////////////////////////////////////
                var unitPerformancePoints =
                    data.Points.CalculationPoints.Where(c => c.Name.Contains("/" + position.Unit.Id + "/")).ToList();
                //todo:clean 
                if (!unitPerformancePoints.Any())
                    throw new Exception("unit performance points count is 0");


                var unitPerformanceAveragePoint = unitPerformancePoints.Sum(up => up.Value)/
                                                  unitPerformancePoints.Count();

                if (unitPerformanceAveragePoint==0)
                    throw new Exception("unitPerformanceAveragePoint is 0");
                var totalPerformancePoint =
                    unitPerformancePoints.Single(up => up.Name.Contains(data.Employee.EmployeeNo)).Value * (9 / unitPerformanceAveragePoint);

                foreach (var index in position.Indices)
                {
                   
                    var jobindexImportance = Convert.ToDecimal(index.Key.CustomFields["JobIndexImportance"]);
                    sumIndexImportance += jobindexImportance;
                    if (index.Key.Group.DictionaryName == "BehaviouralGroup")
                    {
                        var parentPoint = Utils.GetInquiryByJobPositionlevel(index, 1);
                        var childPoint = Utils.GetInquiryByJobPositionlevel(index, 3);
                        var selfPoint = Utils.GetInquiryByJobPositionlevel(index, 4);
                        var point = (parentPoint * 4 + childPoint * 1 + selfPoint * 1) / ((parentPoint != 0 ? 1 : 0) * 4 + (childPoint != 0 ? 1 : 0) * 1 + (selfPoint != 0 ? 1 : 0) * 1);
                        sumBehaviralPoint = sumBehaviralPoint + point * jobindexImportance;
                    }
                    if (index.Key.Group.DictionaryName == "PerformanceGroup")
                    {
                        sumPerformanceGroupImportance = sumPerformanceGroupImportance + jobindexImportance;
                    }

                }
                if (sumIndexImportance == 0)
                    throw new Exception("sumIndexImportance is 0");
                total = total + ((sumBehaviralPoint + totalPerformancePoint * sumPerformanceGroupImportance )/ sumIndexImportance);
                Utils.AddEmployeePoint(position, "finalJob", sumBehaviralPoint + totalPerformancePoint / sumIndexImportance);


            }

            Utils.AddEmployeePoint("final", total/data.JobPositions.Count*10, true);

            //var jobs = data.JobPositions.Where(j => j.Job.DictionaryName == "TechnicalInspector");
            //if (jobs == null || jobs.Count() == 0) return;

            //decimal calcPointMinCostControl;
            //var rulePointCalcPointMinCostControl = Utils.GetCalculationPoint(data, "MinPereiodicMaintenancePlaningTime");
            //calcPointMinCostControl = rulePointCalcPointMinCostControl != null ? rulePointCalcPointMinCostControl.Value : decimal.MaxValue;


            //foreach (var job in jobs)
            //{


            //    //محاسبه تعداد شاخص ها با اهمیت های مختلف
            //    foreach (var index in job.Indices)
            //    {
            //        decimal x = 0;
            //        //فرهنگ و تعهد سازمانی
            //        if (index.Key.DictionaryName == "OrganizationalCultureAndCommitment")
            //        {
            //            var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
            //            var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
            //            var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
            //            var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
            //            var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
            //            var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;

            //            if (inquiryPoint > 70) inquiryPoint = Math.Min(inquiryPoint + inquiryPoint * Convert.ToDecimal("0.05"), 100);
            //            else if (inquiryPoint < 60) inquiryPoint = Math.Max(inquiryPoint - inquiryPoint * Convert.ToDecimal("0.05"), 0);

            //            x = inquiryPoint / 9;
            //        }


            //        Utils.AddEmployeePoint(job, index, "gross", x);
            //    }

            //}
            //Utils.AddCalculationPoint("MinPereiodicMaintenancePlaningTime", calcPointMinCostControl);


        }
    }

}