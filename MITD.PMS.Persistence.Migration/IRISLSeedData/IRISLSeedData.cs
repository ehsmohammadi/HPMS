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
    [Profile("IRISL")]
    public class IRISLSeedData : Migration
    {
        private string behaviouralGroupStr = "Behavioural";
        private string performanceGroupStr = "Performance";
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

                if (Utils.GetCalculationPoint(data, position.Unit.Id + ""/TotalPointUnitIndex"") == null)
                {
                    foreach (var index in position.Unit.Indices)
                    {
                        x += Convert.ToDecimal(index.Value.Item2) * Convert.ToDecimal(index.Key.CustomFields[""UnitIndexImportance""]);
                        y += Convert.ToDecimal(index.Key.CustomFields[""UnitIndexImportance""]);

                    }
                    var res = x / y;
                    Utils.AddCalculationPoint(position.Unit.ParentId + "";"" + position.Unit.Id + ""/UnitPoint"", res);
                    Utils.AddEmployeePoint(position, ""UnitPoint"", res);
                }


                foreach (var index in position.Indices)
                {
                    var parentPoint = Utils.GetInquiryByJobPositionlevel(index, 1);
                    var childPoint = Utils.GetInquiryByJobPositionlevel(index, 3);
                    var selfPoint = Utils.GetInquiryByJobPositionlevel(index, 4);
                    var point = (parentPoint * 4 + childPoint * 1 + selfPoint * 1) / ((parentPoint != 0 ? 1 : 0) * 4 + (childPoint != 0 ? 1 : 0) * 1 + (selfPoint != 0 ? 1 : 0) * 1);
                    if (index.Key.Group.DictionaryName == ""PerformanceGroup"")
                    {
                        var jobindexImportance = Convert.ToDecimal(index.Key.CustomFields[""JobIndexImportance""]);
                        sumPerformanceGroupImportance += jobindexImportance;
                        performancePoint = performancePoint + point * jobindexImportance;
                    }
                    total = total + point;
                    Utils.AddEmployeePoint(position, index, ""gross"", point);

                }
                var finalPerformancePoint = performancePoint / sumPerformanceGroupImportance;
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
                //////////////////////////////////////////////////////////////
                var unitPerformancePoints =
                    data.Points.CalculationPoints.Where(c => c.Name.Contains(""/"" + position.Unit.Id + ""/"")).ToList();
                //todo:clean 
                if (!unitPerformancePoints.Any())
                    throw new Exception(""unit performance points count is 0"");


                var unitPerformanceAveragePoint = unitPerformancePoints.Sum(up => up.Value) /
                                                  unitPerformancePoints.Count();

                if (unitPerformanceAveragePoint == 0)
                    throw new Exception(""unitPerformanceAveragePoint is 0"");

                var unitPoint =Utils.Res.CalculationPoints.Single(
                        c => c.Name == position.Unit.ParentId + "";"" + position.Unit.Id + ""/TotalPointUnit"").Value;
                Utils.AddEmployeePoint(position, ""finalunitPoint"", unitPoint);

                var totalPerformancePoint =
                    unitPerformancePoints.Single(up => up.Name.Contains(data.Employee.EmployeeNo)).Value * (unitPoint / unitPerformanceAveragePoint);

                foreach (var index in position.Indices)
                {

                    var jobindexImportance = Convert.ToDecimal(index.Key.CustomFields[""JobIndexImportance""]);
                    sumIndexImportance += jobindexImportance;
                    if (index.Key.Group.DictionaryName == ""BehaviouralGroup"")
                    {
                        var parentPoint = Utils.GetInquiryByJobPositionlevel(index, 1);
                        var childPoint = Utils.GetInquiryByJobPositionlevel(index, 3);
                        var selfPoint = Utils.GetInquiryByJobPositionlevel(index, 4);
                        var point = (parentPoint * 4 + childPoint * 1 + selfPoint * 1) / ((parentPoint != 0 ? 1 : 0) * 4 + (childPoint != 0 ? 1 : 0) * 1 + (selfPoint != 0 ? 1 : 0) * 1);
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

            Utils.AddEmployeePoint(""final"", total / data.JobPositions.Count * 10, true);
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

                var unitRep = new PMSAdmin.Persistence.NH.UnitRepository(uow);

                #region Unit Creation

                AdminMigrationUtility.CreateUnit(unitRep, "حوزه مدیرعامل", "ChairManDepartment");
                AdminMigrationUtility.CreateUnit(unitRep, "شرکت حمل کانتینری", "ContinerTransportationCompany");
                AdminMigrationUtility.CreateUnit(unitRep, "شرکت حمل فله", "BulkTransportationCompany");
                AdminMigrationUtility.CreateUnit(unitRep, "معاونت مالی", "FinancialDepartment");
                AdminMigrationUtility.CreateUnit(unitRep, "معاونت اداری", "AdministrativeDepartment");
                AdminMigrationUtility.CreateUnit(unitRep, "معاونت فناوری اطلاعات", "ITDepartment");
                AdminMigrationUtility.CreateUnit(unitRep, "دفتر برنامه ریزی و روش ها", "MethodsAndPlanningOffice");

                AdminMigrationUtility.CreateUnit(unitRep, "دفتر تشکیلات", "OfficeOrganization");
                AdminMigrationUtility.CreateUnit(unitRep, "امور کارکنان", "PersonnelDepartment");
                AdminMigrationUtility.CreateUnit(unitRep, "امور پشتیبانی", "SupportDepartment");
                AdminMigrationUtility.CreateUnit(unitRep, "اداره تربیت بدنی", "PhysicalEducationOffice");


                #endregion

                var unitIndexRep = new PMSAdmin.Persistence.NH.UnitIndexRepository(uow);

                #region UnitIndices Creation

                AdminMigrationUtility.CreateUnitIndex(unitIndexRep, "تحقق برنامه های راهبردی", "RealizationOfStrategicPlans");
                AdminMigrationUtility.CreateUnitIndex(unitIndexRep, "تحقق برنامه های عملیاتی", "RealizationOfOperationalPlans");
                AdminMigrationUtility.CreateUnitIndex(unitIndexRep, "ضریب نفوذ اتوماسیون", "PenetrationAutomation");

                #endregion

                var jobRep = new PMSAdmin.Persistence.NH.JobRepository(uow);

                #region Jobs Creation

                AdminMigrationUtility.CreateJob(jobRep, "شغل سازمان", "OrganozationJob");

                #endregion

                var jobPositionRep = new PMSAdmin.Persistence.NH.JobPositionRepository(uow);

                #region JobPositions Creation
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر عامل", "GeneralManger");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر شرکت حمل کانتینری", "ContinerTransportationCompanyManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر شرکت حمل فله", "BulkTransportationCompanyManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر معاونت مالی", "FinancialDepartmentManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر معاونت اداری", "AdministrativeDepartmentManager");

                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر دفتر تشکیلات", "OfficeOrganizationManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر امور کارکنان", "PersonnelDepartmentManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر اداره تربیت بدنی", "PhysicalEducationOfficeManager");

                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر امور پشتیبانی", "SupportDepartmentManager");

                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر واحد تحویل و ترخیص", "DeleiveryAndClearanceManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "کارمند واحد تحویل و ترخیص", "DeleiveryAndClearanceEmployee");

                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر واحد ثبت و طبقه بندی اسناد و مدارک", "RecordedAndClassifiedDocumentsManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "کارمند واحد ثبت و طبقه بندی اسناد و مدارک", "RecordedAndClassifiedDocumentsEmployee");

                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر واحد پشتیبانی و خدمات", "SupportAndServicesManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "کارمند واحد پشتیبانی و خدمات", "SupportAndServicesEmployee");

                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مدیر انبار", "InventoryManager");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "مسئول انبار", "InventoryClerk");

                #endregion


                var jobIndexRep = new PMSAdmin.Persistence.NH.JobIndexRepository(uow);

                #region JobIndexes Creation

                AdminMigrationUtility.CreateJobIndex(jobIndexRep, "سخت کوشی", "HardWorking", behaviouralGroupStr);
                AdminMigrationUtility.CreateJobIndex(jobIndexRep, "نظم و ترتیب", "clarity", behaviouralGroupStr);
                AdminMigrationUtility.CreateJobIndex(jobIndexRep, "مشارکت برنامه های راهبردی", "PartnershipOfStrategicPlans", performanceGroupStr);
                AdminMigrationUtility.CreateJobIndex(jobIndexRep, "مشارکت برنامه های عملیاتی", "PartnershipOfOperationalPlans", performanceGroupStr);
                AdminMigrationUtility.CreateJobIndex(jobIndexRep, "بهره گیری از سیستم اتوماسیون", "utilizationFromAutomation", performanceGroupStr);

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

                PMSMigrationUtility.CreatePeriod(periodRep, "دوره آذر", DateTime.Now, DateTime.Now);

                #endregion

                var jobIndexRep = new JobIndexRepository(uow);

                #region JobIndex Creation

                var behaviouralGroup = PMSMigrationUtility.CreateJobIndexGroup(jobIndexRep, "گروه شاخص های رفتاری",
                    "BehaviouralGroup");
                var performanceGroup = PMSMigrationUtility.CreateJobIndexGroup(jobIndexRep, "گروه شاخص های عملکردی",
                    "PerformanceGroup");

                var value = 3;
                foreach (var jobIndex in AdminMigrationUtility.JobIndices)
                {
                    if (jobIndex.Value == behaviouralGroupStr)
                    {
                        var cftDic =
                            AdminMigrationUtility.DefinedCustomFields.Where(d => jobIndex.Key.CustomFieldTypeIdList.Contains(d.Id))
                                .ToDictionary(c => c, c => "1");
                        PMSMigrationUtility.CreateJobIndex(jobIndexRep, jobIndex.Key, behaviouralGroup, true, cftDic, 1);
                    }
                    if (jobIndex.Value == performanceGroupStr)
                    {
                        var cftDic =
                            AdminMigrationUtility.DefinedCustomFields.Where(d => jobIndex.Key.CustomFieldTypeIdList.Contains(d.Id))
                                .ToDictionary(c => c, c => value.ToString());
                        PMSMigrationUtility.CreateJobIndex(jobIndexRep, jobIndex.Key, performanceGroup, true, cftDic, 2);
                        value--;
                    }
                }




                #endregion

                var jobRep = new JobRepository(uow);

                #region Job creation

                foreach (var job in AdminMigrationUtility.Jobs)
                {
                    PMSMigrationUtility.CreateJob(jobRep, job);
                }

                #endregion

                var unitIndexRep = new UnitIndexRepository(uow);

                #region UnitIndex Creation
                //todo : meghdar zarib ahmiyat ha eshtebah ast 
                var unitGroup = PMSMigrationUtility.CreateUnitIndexGroup(unitIndexRep, "گروه شاخص های سازمانی", "OrganizationUnitGroup");

                foreach (var unitIndex in AdminMigrationUtility.UnitIndices)
                {
                    Dictionary<CustomFieldType, string> cftDic;
                    if (unitIndex.DictionaryName == "PenetrationAutomation")
                        cftDic = AdminMigrationUtility.DefinedCustomFields.Where(
                            d => unitIndex.CustomFieldTypeIdList.Contains(d.Id))
                            .ToDictionary(c => c, c => "2");
                    else
                        cftDic = AdminMigrationUtility.DefinedCustomFields.Where(
                            d => unitIndex.CustomFieldTypeIdList.Contains(d.Id))
                            .ToDictionary(c => c, c => "10");
                    PMSMigrationUtility.CreateUnitIndex(unitIndexRep, unitIndex, unitGroup, true, cftDic);

                }

                #endregion

                var unitRep = new UnitRepository(uow);

                #region Unit Creation

                var adminUnitChairManDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "ChairManDepartment");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitChairManDepartment, null, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","9"},
                    {"RealizationOfOperationalPlans","10"},
                    {"PenetrationAutomation","10"}
                });

                var adminUnitContinerTransportationCompany = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "ContinerTransportationCompany");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitContinerTransportationCompany, null, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","9.5"},
                    {"RealizationOfOperationalPlans","8"},
                    {"PenetrationAutomation","10"}
                });

                var adminUnitBulkTransportationCompany = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "BulkTransportationCompany");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitBulkTransportationCompany, null, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","9"},
                    {"RealizationOfOperationalPlans","8.5"},
                    {"PenetrationAutomation","6"}
                });

                var adminUnitFinancialDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "FinancialDepartment");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitFinancialDepartment, null, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","10"},
                    {"RealizationOfOperationalPlans","10"},
                    {"PenetrationAutomation","5"}
                });

                //var adminUnitITDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "ITDepartment");
                //PMSMigrationUtility.CreateUnit(unitRep, adminUnitITDepartment, null);

                //var adminUnitMethodsAndPlanningOffice = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "MethodsAndPlanningOffice");
                //PMSMigrationUtility.CreateUnit(unitRep, adminUnitMethodsAndPlanningOffice, null);

                var adminUnitAdministrativeDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "AdministrativeDepartment");
                var parent = PMSMigrationUtility.CreateUnit(unitRep, adminUnitAdministrativeDepartment, null, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","9"},
                    {"RealizationOfOperationalPlans","10"},
                    {"PenetrationAutomation","10"}
                });




                var adminUnitOfficeOrganization = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "OfficeOrganization");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitOfficeOrganization, parent, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","9"},
                    {"RealizationOfOperationalPlans","10"},
                    {"PenetrationAutomation","9.5"}
                });

                var adminUnitPersonnelDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "PersonnelDepartment");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitPersonnelDepartment, parent, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","9"},
                    {"RealizationOfOperationalPlans","10"},
                    {"PenetrationAutomation","9.5"}
                });

                var adminUnitSupportDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitSupportDepartment, parent, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","9"},
                    {"RealizationOfOperationalPlans","8"},
                    {"PenetrationAutomation","8.5"}
                });

                var adminUnitPhysicalEducationOffice = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "PhysicalEducationOffice");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitPhysicalEducationOffice, parent, new Dictionary<string, string>
                {
                    {"RealizationOfStrategicPlans","9"},
                    {"RealizationOfOperationalPlans","8"},
                    {"PenetrationAutomation","8.5"}
                });


                #endregion

                var jobPositionRep = new JobPositionRepository(uow);

                #region JobPosition Creation


                var generalManger = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "GeneralManger"), null,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "ChairManDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });


                var ContinerTransportationCompanyManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "ContinerTransportationCompanyManager"), null,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "ContinerTransportationCompany"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });


                var BulkTransportationCompanyManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "BulkTransportationCompanyManager"), null,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "BulkTransportationCompany"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });

                var FinancialDepartmentManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "FinancialDepartmentManager"), null,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "FinancialDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });

                var AdministrativeDepartmentManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "AdministrativeDepartmentManager"), null,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "AdministrativeDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });

                var OfficeOrganizationManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "OfficeOrganizationManager"), AdministrativeDepartmentManager,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "OfficeOrganization"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });

                var PersonnelDepartmentManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "PersonnelDepartmentManager"), AdministrativeDepartmentManager,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "PersonnelDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });

                var PhysicalEducationOfficeManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "PhysicalEducationOfficeManager"), AdministrativeDepartmentManager,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "PhysicalEducationOffice"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });


                //Employee D opertional Point : 10 
                var supportDepartmentManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "SupportDepartmentManager"), AdministrativeDepartmentManager,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "SupportDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", ""}},

                        {"clarity", new List<string> {"9", "10", ""}},

                        {"PartnershipOfStrategicPlans", new List<string> {"10", "10", ""}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", ""}},

                        {"utilizationFromAutomation", new List<string> {"10", "10", ""}}
                    });
                //Employee A opertional Point : 8.53  Total point 8.595
                var deleiveryAndClearanceManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "DeleiveryAndClearanceManager"), supportDepartmentManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "SupportDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", "10"}},

                        {"clarity", new List<string> {"9", "10", "8"}},

                        {"PartnershipOfStrategicPlans", new List<string> {"8", "7", "8"}},

                        {"PartnershipOfOperationalPlans", new List<string> {"7", "9", "10"}},

                        {"utilizationFromAutomation", new List<string> {"10", "9", "9"}}
                    });
                //Employee B opertional Point : 8.9 
                var deleiveryAndClearanceEmployee = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "DeleiveryAndClearanceEmployee"), deleiveryAndClearanceManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "SupportDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", "10"}},

                        {"clarity", new List<string> {"9", "10", "8"}},

                        {"PartnershipOfStrategicPlans", new List<string> {"9", "9", "8"}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", "10"}},

                        {"utilizationFromAutomation", new List<string> {"9", "9", "8"}}
                    });

                // Employee c opertional Point : 9.1
                var recordedAndClassifiedDocumentsManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "RecordedAndClassifiedDocumentsManager"), supportDepartmentManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Keys.Single(u => u.DictionaryName == "SupportDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", "10"}},

                        {"clarity", new List<string> {"9", "10", "8"}},

                        {"PartnershipOfStrategicPlans", new List<string> {"9", "9", "8"}},

                        {"PartnershipOfOperationalPlans", new List<string> {"10", "10", "10"}},

                        {"utilizationFromAutomation", new List<string> {"8", "9", "10"}}
                    });

                //var recordedAndClassifiedDocumentsEmployee = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                //    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "RecordedAndClassifiedDocumentsEmployee"), recordedAndClassifiedDocumentsManager,
                //    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));


                //var supportAndServicesManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                //    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "SupportAndServicesManager"), supportDepartmentManager,
                //    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                //var supportAndServicesEmployee = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                //    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "SupportAndServicesEmployee"), supportAndServicesManager,
                //    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                //var inventoryManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                //    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "InventoryManager"), supportDepartmentManager,
                //    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                //var inventoryEmployee = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                //    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "InventoryClerk"), inventoryManager,
                //    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));


                #endregion

                var employeeRep = new EmployeeRepository(uow);

                #region Employee Creation and assign jobPosition




                PMSMigrationUtility.CreateEmployee(employeeRep, "1", "مدیر", "عامل", generalManger);
                PMSMigrationUtility.CreateEmployee(employeeRep, "2", "مدیر", "شرکت حمل کانتینری", ContinerTransportationCompanyManager);
                PMSMigrationUtility.CreateEmployee(employeeRep, "3", "مدیر", "شرکت حمل فله", BulkTransportationCompanyManager);
                PMSMigrationUtility.CreateEmployee(employeeRep, "4", "مدیر", "معاونت مالی", FinancialDepartmentManager);
                PMSMigrationUtility.CreateEmployee(employeeRep, "5", "مدیر", "معاونت اداری", AdministrativeDepartmentManager);

                PMSMigrationUtility.CreateEmployee(employeeRep, "6", "مدیر", "دفتر تشکیلات", OfficeOrganizationManager);
                PMSMigrationUtility.CreateEmployee(employeeRep, "7", "مدیر", "امور کارکنان", PersonnelDepartmentManager);
                PMSMigrationUtility.CreateEmployee(employeeRep, "8", "مدیر", "اداره تربیت بدنی", PhysicalEducationOfficeManager);
                PMSMigrationUtility.CreateEmployee(employeeRep, "100000", "مدیر", "امور پشتیبانی", supportDepartmentManager);





                PMSMigrationUtility.CreateEmployee(employeeRep, "652547", "مجتبی", "یاوری - فرد A", deleiveryAndClearanceManager);
                PMSMigrationUtility.CreateEmployee(employeeRep, "100001", "کارمند ترخیص", "کارمندیان", deleiveryAndClearanceEmployee);

                PMSMigrationUtility.CreateEmployee(employeeRep, "671061", "رمضان", "محمدی فیروزه", recordedAndClassifiedDocumentsManager);
                //PMSMigrationUtility.CreateEmployee(employeeRep, "100002", "کارمند ثبت", "کارمندیان", recordedAndClassifiedDocumentsEmployee);

                //PMSMigrationUtility.CreateEmployee(employeeRep, "701392", "عباس", "داوودی", supportAndServicesManager);
                //PMSMigrationUtility.CreateEmployee(employeeRep, "100003", "کارمند خدمات", "کارمندیان", supportAndServicesEmployee);

                //PMSMigrationUtility.CreateEmployee(employeeRep, "670151", "محمد علی", "بخشی", inventoryManager);
                //PMSMigrationUtility.CreateEmployee(employeeRep, "100004", "کارمند انبار", "کارمندیان", inventoryEmployee);

                PMSMigrationUtility.CreateEmployee(employeeRep, "30000", "مدیر", "دفتر برنامه ریزی و روش ها", null);
                PMSMigrationUtility.CreateEmployee(employeeRep, "40000", "مدیر", "دفتر فناوری اطلاعات", null);







                #endregion

                uow.Commit();
            }


            #endregion


            // ready for inquiry //////////////////////////////////////////////////////////////////////////////

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var jobPositionRep = new PMS.Persistence.NH.JobPositionRepository(uow);
                var unitRep = new PMS.Persistence.NH.UnitRepository(uow);
                var unitIndexRep = new PMS.Persistence.NH.UnitIndexRepository(uow);
                var employeeRep = new EmployeeRepository(uow);
                var inquiryConfiguratorService = new JobPositionInquiryConfiguratorService(jobPositionRep);


                foreach (var jobPosition in PMSMigrationUtility.JobPositions)
                {
                    var jobp = jobPositionRep.GetBy(jobPosition.Key.Id);
                    jobp.ConfigeInquirer(inquiryConfiguratorService, false);
                }

                foreach (var unitc in PMSMigrationUtility.Units)
                {
                    var unit = unitRep.GetBy(unitc.Key.Id);
                    string employeeNo = "";
                    if (unit.Parent != null)
                        employeeNo = "5";
                    foreach (var unitUnitIndex in unit.UnitIndexList)
                    {
                        var unitIndex = unitIndexRep.GetUnitIndexById(unitUnitIndex.UnitIndexId);
                        if (unitIndex.DictionaryName == "RealizationOfStrategicPlans")
                        {
                            if (string.IsNullOrEmpty(employeeNo))
                                employeeNo = "30000";
                            var employee =
                                employeeRep.GetBy(
                                    PMSMigrationUtility.Employees.Single(e => e.Id.EmployeeNo == employeeNo).Id);
                            unit.AddCustomInquirer(employee.Id, unitIndex.Id);
                        }
                        if (unitIndex.DictionaryName == "RealizationOfOperationalPlans")
                        {
                            if (string.IsNullOrEmpty(employeeNo))
                                employeeNo = "30000";
                            var employee =
                               employeeRep.GetBy(
                                   PMSMigrationUtility.Employees.Single(e => e.Id.EmployeeNo == employeeNo).Id);
                            unit.AddCustomInquirer(employee.Id, unitIndex.Id);
                        }
                        if (unitIndex.DictionaryName == "PenetrationAutomation")
                        {
                            if (string.IsNullOrEmpty(employeeNo) || employeeNo.Equals("30000"))
                                employeeNo = "40000";
                            var employee =
                              employeeRep.GetBy(
                                  PMSMigrationUtility.Employees.Single(e => e.Id.EmployeeNo == employeeNo).Id);
                            unit.AddCustomInquirer(employee.Id, unitIndex.Id);
                        }

                    }
                    //unit.AddCustomInquirer(new EmployeeId("2000",PMSMigrationUtility.Period.Id),unit );
                }
                uow.Commit();
            }




            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var jobPositionRep = new JobPositionRepository(uow);
                var jobRep = new JobRepository(uow);
                var jobIndexRep = new JobIndexRepository(uow);
                var inquiryRep = new InquiryJobIndexPointRepository(uow);
                //////////////////////////////////////////////
                var unitRep = new PMS.Persistence.NH.UnitRepository(uow);
                var inquiryUnitIndexRep = new InquiryUnitIndexPointRepository(uow);
                var unitIndexRep = new UnitIndexRepository(uow);
                //////////////////////////////////////////////////
                var periodRep = new PeriodRepository(uow);
                foreach (var jobPosition in PMSMigrationUtility.JobPositions)
                {
                    PMSMigrationUtility.CreateJobIndexPointWithValuesFromMatrix(jobPosition, jobRep, jobIndexRep, inquiryRep, jobPositionRep);
                }

                foreach (var unitDic in PMSMigrationUtility.Units)
                {
                    var unit = unitRep.GetBy(unitDic.Key.Id);
                    foreach (var unitInquiryConfigurationItem in unit.ConfigurationItemList)
                    {
                        var unitIndex =
                            unitIndexRep.GetUnitIndexById(unitInquiryConfigurationItem.Id.UnitIndexIdUintPeriod);
                        var unitIndexPoint = new InquiryUnitIndexPoint(new InquiryUnitIndexPointId(inquiryUnitIndexRep.GetNextId()), unitInquiryConfigurationItem, unitIndex.Id, unitDic.Value[unitIndex.DictionaryName]);
                        inquiryUnitIndexRep.Add(unitIndexPoint);
                    }
                }


                var p = periodRep.GetById(PMSMigrationUtility.Period.Id);
                p.State = new PeriodInquiryCompletedState();
                uow.Commit();
            }

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                EventPublisher publisher = new EventPublisher();
                var rebps = new RuleBasedPolicyEngineService(new LocatorProvider("PMSDb"), publisher);
                var policyRep = new MITD.PMS.Persistence.NH.PolicyRepository(uow,
                    new PolicyConfigurator(rebps));
                var periodRep = new PeriodRepository(uow);
                var pmsPolicy = policyRep.GetById(new PolicyId(AdminMigrationUtility.Policy.Id.Id));
                var p = periodRep.GetById(PMSMigrationUtility.Period.Id);
                var calcRep = new CalculationRepository(uow);
                var calc = new Calculation
                    (calcRep.GetNextId(), 
                    p, 
                    pmsPolicy,
                    "محاسبه آزمایشی", 
                    DateTime.Now, 
                    "1" + ";" + "2" + ";" + "3" + ";" + "4" + ";" + "5" + ";" + "6" + ";" + "7" + ";" + "8" + ";" 
                    +"100000" + ";" + "652547" + ";" + "100001" + ";" + "671061");
                calcRep.Add(calc);
                uow.Commit();
            }


        }



        public override void Down()
        {
        }
    }

}
