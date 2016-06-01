using System;
using System.Collections.Generic;
using System.Linq;
using FluentMigrator;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.Core.RuleEngine.NH;
using MITD.Data.NH;
using MITD.PMS.Domain.Model.Calculations;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.InquiryUnitIndexPoints;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.JobPositions;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Persistence.NH;
using MITD.PMS.Domain.Service;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;

namespace MITD.PMS.Persistence
{
    [Profile("Convert")]
    public class ConvertSeedData : Migration
    {
        public override void Up()
        {

            #region rule Engine
            var uows = new MITD.Domain.Repository.UnitOfWorkScope(
                new Data.NH.NHUnitOfWorkFactory(() =>
                {
                    RuleEngineSession.sessionName = "PMSDBConnection";
                    return RuleEngineSession.GetSession();
                }));

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var recRep = new REConfigeRepository(uow);

                #region RuleEnginConfigurationItem

                AdminMigrationUtility.CreateRuleEnginConfigurationItem(recRep, "RuleTextTemplate", @"
                        public class <#classname#> : IRule<CalculationData>
                        {
                            public void Execute(CalculationData data)
                            {
                                <#ruletext#>
                            }
                        }");

                AdminMigrationUtility.CreateRuleEnginConfigurationItem(recRep, "ReferencedAssemblies",
                    @"System.Core.dll;MITD.Core.RuleEngine.dll;MITD.PMS.RuleContracts.dll");

                AdminMigrationUtility.CreateRuleEnginConfigurationItem(recRep, "LibraryTextTemplate", @"
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
                                public static RuleResult Res =  new RuleResult();
                                <#functions#>
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

                            <#rules#>
                        }");

                #endregion

                #region Functions

                var rfRep = new Core.RuleEngine.NH.RuleFunctionRepository(uow);
                AdminMigrationUtility.CreateRuleFunction(rfRep, "توابع مورد نیاز", @"
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

         public static List<decimal> GetSubordinatesInquiryPointBy(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> jobIndex)
        {
            try
            {
                var valueList =jobIndex.Value.SelectMany(j => j.Value)
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
                var point=jobIndex.Value.SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.JobPositionLevel == 1);
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
                        if (Math.Abs(subordinatePoint - parentPoint) < 40)
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
                subordinatesPoint = !subordinates.Any() ? 0 : subordinates.Sum() / subordinates.Count;
            }

            var point = (parentPoint * 4 + subordinatesPoint * 1 + selfPoint * 1) /
                        ((parentPoint != 0 ? 1 : 0) * 4 + (subordinatesPoint != 0 ? 1 : 0) * 1 + (selfPoint != 0 ? 1 : 0) * 1);
            return point;
        }


        public static RulePoint AddEmployeePoint(string name, decimal value, bool final = false)
        {

            var res = new RulePoint { Name = name, Value = value, Final = final };
            Utils.Res.Results.Add(res);
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
                throw exceptionConvertor(ex, ""اضافه کردن  مقدار محاسبه با عنوان "" + name);
            }
        }
        public static Exception exceptionConvertor(Exception ex, string keyName)
        {
            var strkeyNotFound = string.Format(""خطا در دریافت اطلاعات مقدار محاسبه. مقداری با کلید {0} یافت نشد "", keyName) + ""\r\n"";
            var strkeyOutOfRange = string.Format(""خطا در دریافت اطلاعات مقدار محاسبه.مقدار {0} در رنج قابل قبول نیست "", keyName) + ""\r\n"";
            var strOtherExp = string.Format(""خطا در دریافت اطلاعات مقدار محاسبه با عنوان "" + keyName) + ""\r\n"";

            if (ex is KeyNotFoundException)
                return new KeyNotFoundException(strkeyNotFound + ex.Message);
            if (ex is IndexOutOfRangeException)
                return new IndexOutOfRangeException(strkeyOutOfRange + ex.Message);

            return new Exception(strOtherExp + ex.Message);
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
        ");

                #endregion

                #region Rules

                var ruleRep = new RuleRepository(uow);
                AdminMigrationUtility.CreateRule(ruleRep, "محاسبه شاخص های کارمندان در دور اول", RuleType.PerCalculation, 1, @"
            if (data.PathNo != 1) return;
            decimal total = 0;
            decimal performancePoint = 0;
            decimal sumPerformanceGroupImportance = 0;

            foreach (var position in data.JobPositions)
            {

                var x = 0m;
                var y = 0m;

                if (Utils.GetCalculationPoint(data, position.Unit.ParentId + "";"" + position.Unit.Id + ""/UnitPoint"") == null)
                {
                    foreach (var index in position.Unit.Indices)
                    {
                        x += Convert.ToDecimal(index.Value.Item2) * Convert.ToDecimal(index.Key.CustomFields[""UnitIndexImportance""]);
                        y += Convert.ToDecimal(index.Key.CustomFields[""UnitIndexImportance""]);

                    }
                    var res = x / y;
                    Utils.AddCalculationPoint(position.Unit.ParentId + "";"" + position.Unit.Id + ""/UnitPoint"", res);
                }


                foreach (var index in position.Indices)
                {
                    var point = Utils.GetPoint(index);
                    if (index.Key.Group.DictionaryName == ""PerformanceGroup"")
                    {
                        var jobindexImportance = Convert.ToDecimal(index.Key.CustomFields[""JobIndexImportance""]);
                        sumPerformanceGroupImportance += jobindexImportance;
                        performancePoint = performancePoint + point * jobindexImportance;
                    }
                    total = total + point;
                    Utils.AddEmployeePoint(position, index,
                        index.Key.Group.DictionaryName == ""PerformanceGroup"" ? ""Performance-gross"" : ""Behavioural-gross"",
                        point);
                }
                var finalPerformancePoint = 0m;
                if (sumPerformanceGroupImportance != 0)
                {
                    finalPerformancePoint = performancePoint / sumPerformanceGroupImportance;
                }
                Utils.AddEmployeePoint(position, ""PerformanceIndices"", finalPerformancePoint);

                Utils.AddCalculationPoint(data.Employee.EmployeeNo + ""/"" + position.Unit.Id + ""/PerformanceIndex"", finalPerformancePoint);


            }");

                AdminMigrationUtility.CreateRule(ruleRep, "محاسبه واحد ها  در دور دوم", RuleType.PerCalculation, 2, @"
            if (data.PathNo != 2)
                return;
            var unitCalculationFlag = Utils.Res.CalculationPoints.SingleOrDefault(c => c.Name == ""UnitCalculationFlag"");
            if (unitCalculationFlag != null)
                return;

            var allstringUnitPoints = data.Points.CalculationPoints.Where(c => c.Name.Contains(""UnitPoint"")).ToList();
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
                Utils.AddCalculationPoint(c.Item1 + "";"" + c.Item2 + ""/TotalPointUnit"", c.Item3);
                //data.Points.CalculationPoints.Where(f => f.Name.Contains(""TotalPointUnit"")).Single(d => d.Name.Contains(string.Concat(c.Item1, ';', c.Item2))).Value = c.Item3;
                //data.Points.CalculationPoints.Where(f => f.Name.Contains(""TotalPointUnit"")).Single(d => d.Name.Contains(string.Concat(c.Item1, ';', c.Item2))).Value = 8;
            });

            Utils.AddCalculationPoint(""UnitCalculationFlag"", 1);
            ");

                AdminMigrationUtility.CreateRule(ruleRep, "محاسبه شاخص های کارمندان در دور دوم", RuleType.PerCalculation, 3, @"
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
                    data.Points.CalculationPoints.Where(c => c.Name.Contains(""/"" + position.Unit.Id + ""/"")).ToList();
                //todo:clean 
                if (!unitPerformancePoints.Any())
                    throw new Exception(""unit performance points count is 0"");

                var countForAvarage=unitPerformancePoints.Count(u => u.Value!=0);
                if (countForAvarage != 0)
                    unitPerformanceAveragePoint = unitPerformancePoints.Sum(up => up.Value)/countForAvarage;
                                                  
                decimal unitPoint;
                try
                {
                    unitPoint = Utils.Res.CalculationPoints.Single(
                   c => c.Name == position.Unit.ParentId + "";"" + position.Unit.Id + ""/TotalPointUnit"").Value;
                }
                catch (Exception ex)
                {

                    throw new Exception(""Total Unit Point is not calculated "" + position.Unit.ParentId + ""--"" + position.Unit.Id);
                }
                Utils.AddEmployeePoint(position, ""finalunitPoint"", unitPoint);

                decimal totalPerformancePoint = 0;
                try
                {
                    if (unitPerformanceAveragePoint != 0)
                        totalPerformancePoint =
                                   unitPerformancePoints.Single(up => up.Name.Contains(data.Employee.EmployeeNo)).Value * (unitPoint / unitPerformanceAveragePoint);
                }
                catch (Exception ex)
                {
                    throw new Exception(""Total performance point is not calculated"");
                }

                Utils.AddEmployeePoint(position, ""finalPerformancePoint"", totalPerformancePoint);

                foreach (var index in position.Indices)
                {

                    var jobindexImportance = Convert.ToDecimal(index.Key.CustomFields[""JobIndexImportance""]);
                    sumIndexImportance += jobindexImportance;
                    if (index.Key.Group.DictionaryName == ""BehaviouralGroup"")
                    {
                      var point = Utils.GetPoint(index);                  
                      sumBehaviralPoint = sumBehaviralPoint + point * jobindexImportance;
                    }
                    if (index.Key.Group.DictionaryName == ""PerformanceGroup"")
                    {
                        sumPerformanceGroupImportance = sumPerformanceGroupImportance + jobindexImportance;
                    }

                }
                if (sumIndexImportance == 0)
                    throw new Exception(""sumIndexImportance is 0"");
                total = total + ((sumBehaviralPoint + totalPerformancePoint * sumPerformanceGroupImportance) / sumIndexImportance);
                Utils.AddEmployeePoint(position, ""finalJob"", (sumBehaviralPoint + totalPerformancePoint * sumPerformanceGroupImportance) / sumIndexImportance);
            }

            Utils.AddEmployeePoint(""final"", total / data.JobPositions.Count, true);
            ");


                #endregion

                uow.Commit();
            }
            #endregion

            #region  PMS Admin

            uows = new MITD.Domain.Repository.UnitOfWorkScope(
               new NHUnitOfWorkFactory(() =>
               {
                   PMSAdmin.Persistence.NH.PMSAdminSession.sessionName = "PMSDBConnection";
                   return PMSAdmin.Persistence.NH.PMSAdminSession.GetSession();
               }));

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var cftRep = new MITD.PMSAdmin.Persistence.NH.CustomFieldRepository(uow);

                #region UnitIndex CustomFields Definition

                AdminMigrationUtility.DefineCustomFieldType(cftRep, "اهمیت", "UnitIndexImportance", 0, 10,
                    MITD.PMSAdmin.Domain.Model.CustomFieldTypes.EntityTypeEnum.UnitIndex);

                #endregion

                #region JobIndex CustomFields Definition

                AdminMigrationUtility.DefineCustomFieldType(cftRep, "اهمیت", "JobIndexImportance", 0, 10,
                    PMSAdmin.Domain.Model.CustomFieldTypes.EntityTypeEnum.JobIndex);

                #endregion

                #region Job CustomFields Definition

                //var cft1 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                //    "بودجه سالانه مصوب (U.S $)", "DeclaredAnnualBudget", 0, 1000000, EntityTypeEnum.Job, "string");
                //cftRep.Add(cft1);
                //jobCftList.Add(cft1);


                //for (int i = 1; i < 7; i++)
                //{
                //    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                //        "سن كشتی" + i, "ShipAge" + i, 0, 100, EntityTypeEnum.Job, "string");
                //    cftRep.Add(cft);
                //    jobCftList.Add(cft);
                //}



                #endregion

                #region Employee CustomFields Definition

                //for (int i = 0; i < 10; i++)
                //{
                //    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                //        "فبلد دلخواه کارمند" + i, "EmployeeCft" + i, 0, 100, EntityTypeEnum.Employee, "string");
                //    cftRep.Add(cft);
                //    employeeCftList.Add(cft);
                //}

                #endregion

                var unitIndexRep = new PMSAdmin.Persistence.NH.UnitIndexRepository(uow);

                #region UnitIndexCategory Creation

                AdminMigrationUtility.CreateUnitIndexCategory(unitIndexRep);

                #endregion

                var jobIndexRep = new PMSAdmin.Persistence.NH.JobIndexRepository(uow);

                #region JobIndexCategory Creation

                AdminMigrationUtility.CreateJobIndexCategory(jobIndexRep);

                #endregion

                var policyRep = new PMSAdmin.Persistence.NH.PolicyRepository(uow);

                #region Policy Creation

                AdminMigrationUtility.CreatePolicy(policyRep, "روش دفتر برنامه ها و روش ها", "MethodsAndPlanOfficePolicy");

                #endregion

                uow.Commit();
            }

            #endregion

            #region PMS

            uows = new MITD.Domain.Repository.UnitOfWorkScope(
                new NHUnitOfWorkFactory(() => PMSSession.GetSession()));

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var periodRep = new PeriodRepository(uow);

                #region Period creation

                PMSMigrationUtility.CreatePeriod(periodRep, "دوره آزمایشی فروردین", new DateTime(2016, 3, 20), new DateTime(2016, 4, 20));

                #endregion

                var jobIndexRep = new JobIndexRepository(uow);

                #region JobIndexGroup Creation

                var behaviouralGroup = PMSMigrationUtility.CreateJobIndexGroup(jobIndexRep, "گروه شاخص های رفتاری",
                    "BehaviouralGroup");
                var performanceGroup = PMSMigrationUtility.CreateJobIndexGroup(jobIndexRep, "گروه شاخص های عملکردی",
                    "PerformanceGroup");

                #endregion

                var unitIndexRep = new UnitIndexRepository(uow);

                #region UnitIndexGroup Creation

                var unitGroup = PMSMigrationUtility.CreateUnitIndexGroup(unitIndexRep, "گروه شاخص های سازمانی", "OrganizationUnitGroup");

                #endregion

                uow.Commit();
            }


            #endregion

        }
        public override void Down()
        {
        }
    }

}
