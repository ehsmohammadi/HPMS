using System;
using System.Collections.Generic;
using System.Linq;
using FluentMigrator;
using MITD.Core;
using MITD.Core.RuleEngine.Model;
using MITD.Core.RuleEngine.NH;
using MITD.Data.NH;
using MITD.PMS.Domain.Model.Calculations;
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
        }"); 

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
                var finalPerformancePoint = performancePoint/sumPerformanceGroupImportance;
                Utils.AddEmployeePoint(position, ""PerformanceIndices"", finalPerformancePoint);
                Utils.AddCalculationPoint(data.Employee.EmployeeNo+""/""+position.Unit.Id + ""/PerformanceIndex"", finalPerformancePoint);
            }");

                AdminMigrationUtility.CreateRule(ruleRep, "محاسبه شاخص های کارمندان در دور دوم", RuleType.PerCalculation, 2, @"
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


                var unitPerformanceAveragePoint = unitPerformancePoints.Sum(up => up.Value)/
                                                  unitPerformancePoints.Count();

                if (unitPerformanceAveragePoint==0)
                    throw new Exception(""unitPerformanceAveragePoint is 0"");
                var totalPerformancePoint =
                    unitPerformancePoints.Single(up => up.Name.Contains(data.Employee.EmployeeNo)).Value * (9 / unitPerformanceAveragePoint);

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
                total = total + ((sumBehaviralPoint + totalPerformancePoint * sumPerformanceGroupImportance )/ sumIndexImportance);
                Utils.AddEmployeePoint(position, ""finalJob"", sumBehaviralPoint + totalPerformancePoint / sumIndexImportance);


            }

            Utils.AddEmployeePoint(""final"", total/data.JobPositions.Count*10, true);
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

                AdminMigrationUtility.CreateUnit(unitRep,"حوزه مدیرعامل","ChairManDepartment");
                AdminMigrationUtility.CreateUnit(unitRep, "شرکت حمل کانتینری", "ContinerTransportationCompany");
                AdminMigrationUtility.CreateUnit(unitRep, "شرکت حمل فله", "BulkTransportationCompany");
                AdminMigrationUtility.CreateUnit(unitRep, "معاونت مالی", "FinancialDepartment");
                AdminMigrationUtility.CreateUnit(unitRep, "معاونت اداری", "AdministrativeDepartment");

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

                AdminMigrationUtility.CreatePolicy(policyRep,"روش دفتر برنامه ها و روش ها","MethodsAndPlanOfficePolicy");

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

                PMSMigrationUtility.CreatePeriod(periodRep,"دوره آذر",DateTime.Now, DateTime.Now);

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
                        PMSMigrationUtility.CreateJobIndex(jobIndexRep, jobIndex.Key, behaviouralGroup, true, cftDic,1);
                    }
                    if (jobIndex.Value == performanceGroupStr)
                    {
                        var cftDic =
                            AdminMigrationUtility.DefinedCustomFields.Where(d => jobIndex.Key.CustomFieldTypeIdList.Contains(d.Id))
                                .ToDictionary(c => c, c => value.ToString());
                        PMSMigrationUtility.CreateJobIndex(jobIndexRep, jobIndex.Key, performanceGroup, true, cftDic,2);
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
                    var cftDic =
                        AdminMigrationUtility.DefinedCustomFields.Where(d => unitIndex.CustomFieldTypeIdList.Contains(d.Id))
                            .ToDictionary(c => c, c => "10");
                    PMSMigrationUtility.CreateUnitIndex(unitIndexRep, unitIndex, unitGroup, true, cftDic);

                }

                #endregion

                var unitRep = new UnitRepository(uow);

                #region Unit Creation

                var adminUnitChairManDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "ChairManDepartment");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitChairManDepartment, null);

                var adminUnitContinerTransportationCompany = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "ContinerTransportationCompany");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitContinerTransportationCompany, null);

                var adminUnitBulkTransportationCompany = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "BulkTransportationCompany");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitBulkTransportationCompany, null);

                var adminUnitFinancialDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "FinancialDepartment");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitFinancialDepartment, null);

                var adminUnitAdministrativeDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "AdministrativeDepartment");
                var parent = PMSMigrationUtility.CreateUnit(unitRep, adminUnitAdministrativeDepartment, null);

                var adminUnitOfficeOrganization = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "OfficeOrganization");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitOfficeOrganization, parent);

                var adminUnitPersonnelDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "PersonnelDepartment");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitPersonnelDepartment, parent);

                var adminUnitSupportDepartment = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitSupportDepartment, parent);

                var adminUnitPhysicalEducationOffice = AdminMigrationUtility.Units.Single(u => u.DictionaryName == "PhysicalEducationOffice");
                PMSMigrationUtility.CreateUnit(unitRep, adminUnitPhysicalEducationOffice, parent);


                #endregion

                var jobPositionRep = new JobPositionRepository(uow);

                #region JobPosition Creation
                //Employee D opertional Point : 10 
                var supportDepartmentManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "SupportDepartmentManager"), null,
                    PMSMigrationUtility.Jobs.First(),
                    PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"),
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
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"),
                    new Dictionary<string, List<string>>
                    {
                        {"HardWorking", new List<string> {"10", "9", "10"}},

                        {"clarity", new List<string> {"9", "10", "8"}},

                        {"PartnershipOfStrategicPlans", new List<string> {"8", "7", "8"}},

                        {"PartnershipOfOperationalPlans", new List<string> {"7", "9", "10"}},

                        {"utilizationFromAutomation", new List<string> {"10", "9", "9"}}
                    });
                //Employee B opertional Point : 8.9 
                var deleiveryAndClearanceEmployee=PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "DeleiveryAndClearanceEmployee"), deleiveryAndClearanceManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"),
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
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"),
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

                PMSMigrationUtility.CreateEmployee(employeeRep, "100000", "مدیر", "مدیریان", supportDepartmentManager);

                PMSMigrationUtility.CreateEmployee(employeeRep, "652547", "مجتبی", "یاوری - فرد A", deleiveryAndClearanceManager);
                PMSMigrationUtility.CreateEmployee(employeeRep, "100001", "کارمند ترخیص", "کارمندیان", deleiveryAndClearanceEmployee);

                PMSMigrationUtility.CreateEmployee(employeeRep, "671061", "رمضان", "محمدی فیروزه", recordedAndClassifiedDocumentsManager);
                //PMSMigrationUtility.CreateEmployee(employeeRep, "100002", "کارمند ثبت", "کارمندیان", recordedAndClassifiedDocumentsEmployee);

                //PMSMigrationUtility.CreateEmployee(employeeRep, "701392", "عباس", "داوودی", supportAndServicesManager);
                //PMSMigrationUtility.CreateEmployee(employeeRep, "100003", "کارمند خدمات", "کارمندیان", supportAndServicesEmployee);

                //PMSMigrationUtility.CreateEmployee(employeeRep, "670151", "محمد علی", "بخشی", inventoryManager);
                //PMSMigrationUtility.CreateEmployee(employeeRep, "100004", "کارمند انبار", "کارمندیان", inventoryEmployee);

                #endregion

                uow.Commit();
            }

            
            #endregion


            // ready for inquiry //////////////////////////////////////////////////////////////////////////////

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var jobPositionRep = new PMS.Persistence.NH.JobPositionRepository(uow);
                var inquiryConfiguratorService = new JobPositionInquiryConfiguratorService(jobPositionRep);

                foreach (var jobPosition in PMSMigrationUtility.JobPositions)
                {
                    var jobp = jobPositionRep.GetBy(jobPosition.Key.Id);
                    jobp.ConfigeInquirer(inquiryConfiguratorService, false);
                }
                uow.Commit();
            }

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var jobPositionRep = new JobPositionRepository(uow);
                var jobRep = new JobRepository(uow);
                var jobIndexRep = new JobIndexRepository(uow);
                var inquiryRep = new InquiryJobIndexPointRepository(uow);
                var periodRep = new PeriodRepository(uow);
                foreach (var jobPosition in PMSMigrationUtility.JobPositions)
                {
                    PMSMigrationUtility.CreateJobIndexPointWithValuesFromMatrix(jobPosition, jobRep, jobIndexRep, inquiryRep, jobPositionRep);
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
                var calc = new Calculation(calcRep.GetNextId(), p, pmsPolicy,
                    "محاسبه آزمایشی", DateTime.Now, "100000" + ";" + "652547" + ";" + "100001" + ";" + "671061");
                calcRep.Add(calc);
                uow.Commit();
            }


        }

        

        public override void Down()
        {
        }
    }

}
