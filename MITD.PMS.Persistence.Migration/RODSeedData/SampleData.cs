using System;
using FluentMigrator;
using MITD.Data.NH;
using MITD.PMS.Application;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.JobIndices;
using MITD.PMS.Domain.Model.Jobs;
using MITD.PMSAdmin.Domain.Model.CustomFieldTypes;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using MITD.PMS.Domain.Service;
using MITD.PMS.Persistence.NH;
using MITD.Core.RuleEngine.NH;
using MITD.Core;
using MITD.PMS.Domain.Model.Policies;
using MITD.PMS.Domain.Model.Periods;
using MITD.PMS.Domain.Model.Employees;
using MITD.PMS.Domain.Model.Jobs;
using MITD.Core.RuleEngine.Model;
using MITD.PMS.Domain.Model.Calculations;
namespace MITD.PMS.Persistence
{
    [Profile("Demo")]
    public class SampleDate : Migration
    {

        public class JobindexDes
        {
            public PMSAdmin.Domain.Model.JobIndices.JobIndex JobIndex { get; set; }
            public string Importance { get; set; }
            public bool IsInquirable { get; set; }

        }

        public class DocSeed
        {
            public string DicName { get; set; }
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public string Value3 { get; set; }
            public string Value4 { get; set; }
            public string Value5 { get; set; }
        }

        private List<DocSeed> docSeedValues = new List<DocSeed>
        {
            new DocSeed{DicName="OrganizationalCultureAndCommitment",Value1="9",Value2="8",Value3="9",Value4="9",Value5="9"},
            new DocSeed{DicName="OrganizationalGrowthAndExcelency",Value1="8",Value2="8",Value3="9",Value4="9",Value5="8"},
            new DocSeed{DicName="Discipline",Value1="9",Value2="9",Value3="9",Value4="9",Value5="9"},
            new DocSeed{DicName="AdherenceToOrganizarionValues",Value1="9",Value2="9",Value3="9",Value4="9",Value5="9"},
            new DocSeed{DicName="HardWorking",Value1="9",Value2="8",Value3="9",Value4="9",Value5="8"},
            new DocSeed{DicName="EnglishLanquageSkills",Value1="8",Value2="8",Value3="8",Value4="8",Value5="7"},
            new DocSeed{DicName="ICDLFamiliarity",Value1="8",Value2="8",Value3="8",Value4="8",Value5="8"},
            new DocSeed{DicName="MentalAndPhisicalAbility",Value1="9",Value2="9",Value3="9",Value4="9",Value5="9"},
            new DocSeed{DicName="DeclaredAnnualBudget",Value1="4561300",Value2="5174600",Value3="3485160",Value4="5004400",Value5="1171820"},
            new DocSeed{DicName="TotalAnnualCost",Value1="490675",Value2="984681",Value3="536472",Value4="877233",Value5="372589"},
            new DocSeed{DicName="BudgetQuality",Value1="9",Value2="8",Value3="9",Value4="9",Value5="7"},
            new DocSeed{DicName="EstimatedPereiodicMaintenanceTime",Value1="0",Value2="23",Value3="0",Value4="16",Value5="0"},
            new DocSeed{DicName="ActualPereiodicMaintenanceTime",Value1="0",Value2="20",Value3="0",Value4="21",Value5="0"},
            new DocSeed{DicName="PereiodicMaintenancePlaningQuality",Value1="0",Value2="7",Value3="0",Value4="8",Value5="0"},
            new DocSeed{DicName="VoyageMaintenancePlannigAbility",Value1="9",Value2="8",Value3="9",Value4="9",Value5="8"},
            new DocSeed{DicName="ContractorSelectionProcess",Value1="9",Value2="8",Value3="9",Value4="9",Value5="7"},
            new DocSeed{DicName="TechnicalOffHireTime",Value1="0",Value2="0.6",Value3="3.47",Value4="0.97",Value5="3.66"},
            new DocSeed{DicName="VesselFaultSumNo",Value1="26",Value2="0",Value3="4",Value4="14",Value5="0"},
            new DocSeed{DicName="PSCFSCvesselInspectionNO",Value1="2",Value2="0",Value3="2",Value4="3",Value5="0"},
            new DocSeed{DicName="ProblemSolvingAbility",Value1="8",Value2="8",Value3="8",Value4="8",Value5="7"},
            new DocSeed{DicName="CertificateUpdates",Value1="9",Value2="8",Value3="9",Value4="9",Value5="8"},
            new DocSeed{DicName="JobSoftwareFamiliarity",Value1="9",Value2="8",Value3="9",Value4="9",Value5="7"},
            new DocSeed{DicName="TechnicalCourseAttending",Value1="0",Value2="100",Value3="100",Value4="100",Value5="100"},
            new DocSeed{DicName="ShipAge1",Value1="10",Value2="5",Value3="5",Value4="5",Value5="13"},
            new DocSeed{DicName="ShipAge2",Value1="10",Value2="5",Value3="4",Value4="5",Value5="13"},
            new DocSeed{DicName="ShipAge3",Value1="9",Value2="9",Value3="4",Value4="3",Value5="13"},
            new DocSeed{DicName="ShipAge4",Value1="2",Value2="9",Value3="9",Value4="0",Value5="13"},
            new DocSeed{DicName="ShipAge5",Value1="1",Value2="9",Value3="0",Value4="0",Value5="0"},
            new DocSeed{DicName="ShipAge6",Value1="0",Value2="0",Value3="0",Value4="0",Value5="0"},

            new DocSeed{DicName="ShipType1",Value1="2",Value2="2",Value3="2",Value4="2",Value5="2"},
            new DocSeed{DicName="ShipType2",Value1="2",Value2="2",Value3="2",Value4="2",Value5="2"},
            new DocSeed{DicName="ShipType3",Value1="2",Value2="2",Value3="2",Value4="2",Value5="2"},
            new DocSeed{DicName="ShipType4",Value1="2",Value2="2",Value3="2",Value4="2",Value5="2"},
            new DocSeed{DicName="ShipType5",Value1="2",Value2="2",Value3="2",Value4="2",Value5="2"},
            new DocSeed{DicName="ShipType6",Value1="2",Value2="2",Value3="2",Value4="2",Value5="2"},

            new DocSeed{DicName="ShipPath1",Value1="3",Value2="3",Value3="3",Value4="3",Value5="3"},
            new DocSeed{DicName="ShipPath2",Value1="3",Value2="3",Value3="3",Value4="3",Value5="3"},
            new DocSeed{DicName="ShipPath3",Value1="3",Value2="9",Value3="3",Value4="3",Value5="9"},
            new DocSeed{DicName="ShipPath4",Value1="9",Value2="9",Value3="5",Value4="9",Value5="9"},
            new DocSeed{DicName="ShipPath5",Value1="9",Value2="9",Value3="9",Value4="9",Value5="9"},
            new DocSeed{DicName="ShipPath6",Value1="9",Value2="9",Value3="9",Value4="9",Value5="9"},

            new DocSeed{DicName="ShipNo",Value1="5",Value2="5",Value3="4",Value4="4",Value5="4"},
            new DocSeed{DicName="MaintenaceLocation",Value1="9",Value2="8",Value3="9",Value4="9",Value5="7"},
            new DocSeed{DicName="ShipOperationalEfficiency",Value1="201",Value2="186",Value3="299",Value4="279",Value5="29"},
            new DocSeed{DicName="PeriodicMaintenance",Value1="0",Value2="1",Value3="0",Value4="1",Value5="0"},

            new DocSeed{DicName="CommandersInquiryScore",Value1="78",Value2="86",Value3="74",Value4="76",Value5="77"},
            new DocSeed{DicName="FirstOfficersInquiryScore",Value1="78",Value2="83",Value3="78",Value4="79",Value5="78"},
            new DocSeed{DicName="ChiefEngineersInquiryScore",Value1="78",Value2="86",Value3="73",Value4="85",Value5="77"},
            new DocSeed{DicName="SecondOfficersInquiryScore",Value1="73",Value2="80",Value3="81",Value4="78",Value5="77"},
            new DocSeed{DicName="ElectronicEngineersInquiryScore",Value1="75",Value2="83",Value3="78",Value4="78",Value5="77"},

        };
        public override void Up()
        {
            //List<PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType> employeeCftList = new List<CustomFieldType>();
            List<PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType> jobIndexCftList = new List<CustomFieldType>();
            List<PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType> jobCftList = new List<CustomFieldType>();
            List<PMSAdmin.Domain.Model.Jobs.Job> jobList = new List<PMSAdmin.Domain.Model.Jobs.Job>();
            List<JobindexDes> GenralJobIndexList = new List<JobindexDes>();
            List<JobindexDes> TechnicalJobIndexList = new List<JobindexDes>();
            List<JobindexDes> EqulizerjobIndexList = new List<JobindexDes>();
            List<PMSAdmin.Domain.Model.JobPositions.JobPosition> jobPositionList = new List<PMSAdmin.Domain.Model.JobPositions.JobPosition>();
            List<PMSAdmin.Domain.Model.Units.Unit> unitList = new List<PMSAdmin.Domain.Model.Units.Unit>();
            PMSAdmin.Domain.Model.Policies.RuleEngineBasedPolicy policy;
            List<Core.RuleEngine.Model.Rule> rules = new List<Rule>();
            Core.RuleEngine.Model.RuleFunction rf;
            Period period;
            PMSAdmin.Domain.Model.JobPositions.JobPosition technicalManagerJobPosition;


            List<PMS.Domain.Model.Jobs.Job> jobInPeriodList = new List<PMS.Domain.Model.Jobs.Job>();
            List<PMS.Domain.Model.JobIndices.JobIndex> jobIndexInPeriodList = new List<PMS.Domain.Model.JobIndices.JobIndex>();
            List<PMS.Domain.Model.JobPositions.JobPosition> jobPositionInPeriodList = new List<PMS.Domain.Model.JobPositions.JobPosition>();
            List<PMS.Domain.Model.Units.Unit> unitInPeriodList = new List<PMS.Domain.Model.Units.Unit>();
            List<PMS.Domain.Model.Employees.Employee> empList = new List<PMS.Domain.Model.Employees.Employee>();

            #region rule Engine
            var uows = new MITD.Domain.Repository.UnitOfWorkScope(
                new Data.NH.NHUnitOfWorkFactory(() =>
                {
                    RuleEngineSession.sessionName = "PMSDBConnection";
                    return Core.RuleEngine.NH.RuleEngineSession.GetSession();
                }));

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var recRep = new Core.RuleEngine.NH.REConfigeRepository(uow);
                var rec = new Core.RuleEngine.Model.RuleEngineConfigurationItem(
                    new Core.RuleEngine.Model.RuleEngineConfigurationItemId("RuleTextTemplate"),
@"
public class <#classname#> : IRule<CalculationData>
{
    public void Execute(CalculationData data)
    {
        <#ruletext#>
    }
}");
                recRep.Add(rec);
                rec = new Core.RuleEngine.Model.RuleEngineConfigurationItem(
                    new Core.RuleEngine.Model.RuleEngineConfigurationItemId("ReferencedAssemblies"),
                                        @"System.Core.dll;MITD.Core.RuleEngine.dll;MITD.PMS.RuleContracts.dll");
                recRep.Add(rec);
                rec = new Core.RuleEngine.Model.RuleEngineConfigurationItem(
                    new Core.RuleEngine.Model.RuleEngineConfigurationItemId("LibraryTextTemplate"),
@"
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
                recRep.Add(rec);

                var rfRep = new Core.RuleEngine.NH.RuleFunctionRepository(uow);
                rf = new RuleFunction(rfRep.GetNextId(), "توابع خطکش پورتر",
@"
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
                throw exceptionConvertor(ex, "" شاخص "" + fieldName);
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
                var res = new RulePoint {Name = name, Value = value, Final = final};
                Utils.Res.CalculationPoints.Add(res);
                return res;
            }
            catch (Exception ex)
            {
                throw exceptionConvertor(ex, ""اضافه کردن  مقدار محاسبه با عنوان ""+name);
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
                throw exceptionConvertor(ex,name);
            }
        }

        private static Exception exceptionConvertor(Exception ex, string keyName)
        {
            var strkeyNotFound = string.Format(""خطا در دریافت اطلاعات مقدار محاسبه. مقداری با کلید {0} یافت نشد "", keyName) + ""\r\n"";
            var strkeyOutOfRange = string.Format(""خطا در دریافت اطلاعات مقدار محاسبه.مقدار {0} در رنج قابل قبول نیست "", keyName) + ""\r\n"";
            var strOtherExp = string.Format(""خطا در دریافت اطلاعات مقدار محاسبه با عنوان "" + keyName) + ""\r\n"";
                
                if (ex is KeyNotFoundException)
                    return new KeyNotFoundException(strkeyNotFound+ex.Message);
                if (ex is IndexOutOfRangeException)
                    return new IndexOutOfRangeException(strkeyOutOfRange + ex.Message);
                 
                return new Exception(strOtherExp+ex.Message);
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

        public static RulePoint GetPoint(JobPosition job,KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string name)
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
            return Convert.ToDecimal(jobIndex.Value.SelectMany(j=>j.Value).SingleOrDefault(x => x.JobPosition.DictionaryName == inquirerJobPositionName).Value);
        }
"
                );
                rfRep.Add(rf);
                var ruleRep = new RuleRepository(uow);
                var rule1 = new Rule(new RuleId(ruleRep.GetNextId()), "محاسبه وزن ها",
@"
            decimal z = .4m;
            Utils.AddCalculationPoint(""z"", z);
            foreach (var job in data.JobPositions)
            {
                if (Utils.GetPoint(job, ""a1"") != null) break;
                decimal notPresentIndexesCount = 1;
                if (job.CustomFields[""PeriodicMaintenance""] == ""0"")
                    notPresentIndexesCount = 2;

                decimal a1 = Utils.IndexCount(job, ""Importance"", ""1"", ""General"");
                decimal b1 = Utils.IndexCount(job, ""Importance"", ""3"", ""General"");
                decimal c1 = Utils.IndexCount(job, ""Importance"", ""5"", ""General"");
                decimal d1 = Utils.IndexCount(job, ""Importance"", ""7"", ""General"");
                decimal e1 = Utils.IndexCount(job, ""Importance"", ""9"", ""General"");

                //محاسبه عدد وزنی شاخص های عمومی
                decimal y1 = 0;
                decimal n = (a1 + 3 * b1 + 5 * c1 + 7 * d1 + 9 * e1);
                if (n != 0)
                    y1 = 20 / n;

                Utils.AddEmployeePoint(job, ""a1"", y1);
                Utils.AddEmployeePoint(job, ""b1"", 3 * y1);
                Utils.AddEmployeePoint(job, ""c1"", 5 * y1);
                Utils.AddEmployeePoint(job, ""d1"", 7 * y1);
                Utils.AddEmployeePoint(job, ""e1"", 9 * y1);

                decimal a2 = Utils.IndexCount(job, ""Importance"", ""1"", ""Technical"");
                decimal b2 = Utils.IndexCount(job, ""Importance"", ""3"", ""Technical"");
                decimal c2 = Utils.IndexCount(job, ""Importance"", ""5"", ""Technical"");
                decimal d2 = Utils.IndexCount(job, ""Importance"", ""7"", ""Technical"") - notPresentIndexesCount;
                decimal e2 = Utils.IndexCount(job, ""Importance"", ""9"", ""Technical"") - notPresentIndexesCount;

                //محاسبه عدد وزنی شاخص های تخصصی
                decimal y2 = 0;
                decimal m = (a2 + 3 * b2 + 5 * c2 + 7 * d2 + 9 * e2);
                if (m != 0)
                    y2 = 80 / m;

                Utils.AddEmployeePoint(job, ""a2"", y2);
                Utils.AddEmployeePoint(job, ""b2"", 3 * y2);
                Utils.AddEmployeePoint(job, ""c2"", 5 * y2);
                Utils.AddEmployeePoint(job, ""d2"", 7 * y2);
                Utils.AddEmployeePoint(job, ""e2"", 9 * y2);

                decimal a3 = Utils.IndexCount(job, ""Importance"", ""1"", ""Equalizer"");
                decimal b3 = Utils.IndexCount(job, ""Importance"", ""3"", ""Equalizer"");
                decimal c3 = Utils.IndexCount(job, ""Importance"", ""5"", ""Equalizer"");
                decimal d3 = Utils.IndexCount(job, ""Importance"", ""7"", ""Equalizer"");
                decimal e3 = Utils.IndexCount(job, ""Importance"", ""9"", ""Equalizer"");

                //محاسبه عدد وزنی شاخص های یکسان ساز
                decimal y3 = 0;
                decimal o = (a3 + 3 * b3 + 5 * c3 + 7 * d3 + 9 * e3);
                if (o != 0)
                    y3 = z / o;

                Utils.AddEmployeePoint(job, ""a3"", y3);
                Utils.AddEmployeePoint(job, ""b3"", 3 * y3);
                Utils.AddEmployeePoint(job, ""c3"", 5 * y3);
                Utils.AddEmployeePoint(job, ""d3"", 7 * y3);
                Utils.AddEmployeePoint(job, ""e3"", 9 * y3);

          }
", RuleType.PerCalculation, 1);
                ruleRep.Add(rule1);
                rules.Add(rule1);


                var rule2 = new Rule(new RuleId(ruleRep.GetNextId()), "محاسبه شاخص های مرحله اول",
@"
           if (data.PathNo != 1) return;
            var jobs = data.JobPositions.Where(j => j.Job.DictionaryName == ""TechnicalInspector"");
            if (jobs == null || jobs.Count() == 0) return;

            decimal calcPointMinCostControl;
            var rulePointCalcPointMinCostControl = Utils.GetCalculationPoint(data, ""MinPereiodicMaintenancePlaningTime"");
            calcPointMinCostControl = rulePointCalcPointMinCostControl != null ? rulePointCalcPointMinCostControl.Value : decimal.MaxValue;

            decimal calcPointMinTechnicalOffHireTime;
            var rulePointCalcPointMinTechnicalOffHireTime = Utils.GetCalculationPoint(data, ""MinTechnicalOffHireTime"");
            calcPointMinTechnicalOffHireTime = rulePointCalcPointMinTechnicalOffHireTime != null ? rulePointCalcPointMinTechnicalOffHireTime.Value : decimal.MaxValue;


            foreach (var job in jobs)
            {


                //محاسبه تعداد شاخص ها با اهمیت های مختلف
                foreach (var index in job.Indices)
                {
                    decimal x = 0;
                    //فرهنگ و تعهد سازمانی
                    if (index.Key.DictionaryName == ""OrganizationalCultureAndCommitment"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""OrganizationalGrowthAndExcelency"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""Discipline"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""AdherenceToOrganizarionValues"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""HardWorking"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""EnglishLanquageSkills"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""ICDLFamiliarity"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""MentalAndPhisicalAbility"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }


                    // Technical ------------------------------------------------------------------------
                    //قابلیت برنامه ریزی و کنترل تعمیرات سفری
                    else if (index.Key.DictionaryName == ""VoyageMaintenancePlannigAbility"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    //برنامه ریزی و کنترل تعمیرات ادواری (کیفیت
                    else if (index.Key.DictionaryName == ""PereiodicMaintenancePlaningQuality"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    //پروسه انتخاب پیمانکاران
                    else if (index.Key.DictionaryName == ""ContractorSelectionProcess"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    //آنالير مشكلات فني ، پیگیری نواقص و ارائه راهکار
                    else if (index.Key.DictionaryName == ""ProblemSolvingAbility"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    //به روزنگه داشتن گواهینامه ها و رعایت الزامات قانونی
                    else if (index.Key.DictionaryName == ""CertificateUpdates"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    //آشنایی با نرم افزارهای مربوط به شغل
                    else if (index.Key.DictionaryName == ""JobSoftwareFamiliarity"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    //کنترل هزینه
                    else if (index.Key.DictionaryName == ""CostControl"")
                    {
                        //----------------------------------------------------------------------------------------------------
                        continue;
                        //x = Math.Abs(Convert.ToDecimal(job.CustomFields[""DeclaredAnnualBudget""]) - Convert.ToDecimal(job.CustomFields[""TotalAnnualCost""]) + 1) / Convert.ToDecimal(job.CustomFields[""DeclaredAnnualBudget""]);
                        //var calcPointMinCostControl = Utils.GetCalculationPoint(data, ""MinCostControl"");
                        //if (calcPointMinCostControl == null)
                        //    Utils.AddCalculationPoint(""MinCostControl"", x);
                        //else if (x < calcPointMinCostControl.Value)
                        //    Utils.AddCalculationPoint(""MinCostControl"", x);
                    }
                    else if (index.Key.DictionaryName == ""BudgetQuality"")
                    {
                        continue;
                        //var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        //x = inquiryPoint / 9;
                    }
                    //برنامه ریزی و کنترل تعمیرات ادواری (زمان
                    else if (index.Key.DictionaryName == ""PereiodicMaintenancePlaningTime"")
                    {
                        if (job.CustomFields[""PeriodicMaintenance""] == ""0"")
                            continue;
                        else
                        {
                            x = Math.Abs(Convert.ToDecimal(job.CustomFields[""EstimatedPereiodicMaintenanceTime""]) - Convert.ToDecimal(job.CustomFields[""ActualPereiodicMaintenanceTime""]) + 1) / Convert.ToDecimal(job.CustomFields[""EstimatedPereiodicMaintenanceTime""]);
                            if (x < calcPointMinCostControl)
                               calcPointMinCostControl= x;
                            //if (calcPointMinCostControl.HasValue)
                            //    Utils.AddCalculationPoint(""MinPereiodicMaintenancePlaningTime"", x);
                            //else if (x < calcPointMinCostControl.Value)
                            //    
                        }
                    }
                    //مدت زمان Off HIRE فني (ساعت)
                    else if (index.Key.DictionaryName == ""TechnicalOffHireTime"")
                    {
                        x = (Convert.ToDecimal(job.CustomFields[""TechnicalOffHireTime""]) / Convert.ToDecimal(job.CustomFields[""ShipOperationalEfficiency""])) + 1;
                       
                        //if (calcPointMinTechnicalOffHireTime == null)
                        //    Utils.AddCalculationPoint(""MinTechnicalOffHireTime"" , x);
                        //else 
                        if (x < calcPointMinTechnicalOffHireTime)
                            calcPointMinTechnicalOffHireTime = x;
                           // Utils.AddCalculationPoint(""MinTechnicalOffHireTime"" , x);
                    }
                    //مجموع تعداد نواقص اعلام شده توسط FSC و PSC برای شناورها
                    else if (index.Key.DictionaryName == ""VesselFaultSumNo"")
                    {
                        decimal y = Convert.ToDecimal(job.CustomFields[""PSCFSCvesselInspectionNO""]);
                        x = 2 / ((Convert.ToDecimal(job.CustomFields[""VesselFaultSumNo""]) / (y > 0 ? y : 1)) + 2);
                    }
                    //بهره وري دوره های تخصصی ( دوره مهندس ناظری)
                    else if (index.Key.DictionaryName == ""TechnicalCourseAttending"")
                    {
                        x = Convert.ToDecimal(job.CustomFields[""TechnicalCourseAttending""]) / 100;
                    }
                    else if (index.Key.DictionaryName == ""PereiodicMaintenancePlaningQuality"")
                    {
                        if (job.CustomFields[""PeriodicMaintenance""] == ""0"")
                            continue;
                        else
                        {
                            var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                            x = inquiryPoint / 9;
                        }
                    }
                    else if (index.Key.DictionaryName == ""VoyageMaintenancePlannigAbility"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""ContractorSelectionProcess"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""ProblemSolvingAbility"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""CertificateUpdates"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }
                    else if (index.Key.DictionaryName == ""JobSoftwareFamiliarity"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = inquiryPoint / 9;
                    }



                    // Equalizer -------------------------------------------------------------------------------------
                    //مکان و محل تعمیرات
                    else if (index.Key.DictionaryName == ""MaintenaceLocation"")
                    {
                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, ""TechnicalInspectorJobPosition"");
                        x = (10 - inquiryPoint) / 9;
                    }
                    //سن كشتي
                    else if (index.Key.DictionaryName == ""ShipAge"")
                    {
                        decimal no = Convert.ToDecimal(job.CustomFields[""ShipNo""]);
                        for (int i = 1; i < no + 1; i++)
                        {
                            decimal age = Convert.ToDecimal(job.CustomFields[""ShipAge"" + i]);
                            if (age >= 0 && age < 5) age = 1;
                            else if (age >= 5 && age < 10) age = 3;
                            else if (age >= 10 && age < 15) age = 5;
                            else if (age >= 15 && age < 20) age = 7;
                            else if (age >= 20) age = 9;
                            else continue;
                            x += age;
                        }
                        x = x / no / 9;
                    }
                    //نوع كشتي
                    else if (index.Key.DictionaryName == ""ShipType"")
                    {
                        decimal no = Convert.ToDecimal(job.CustomFields[""ShipNo""]);
                        for (int i = 1; i < no + 1; i++)
                        {
                            decimal type = Convert.ToDecimal(job.CustomFields[""ShipType"" + i]);
                            if (type == 0) type = 8;
                            else if (type == 1) type = 7;
                            else type = 9;
                            x += type;
                        }
                        x = x / no / 9;
                    }
                    //تعداد كشتي
                    else if (index.Key.DictionaryName == ""ShipNo"")
                    {
                        decimal no = Convert.ToDecimal(job.CustomFields[""ShipNo""]);
                        x = no / 6;
                    }
                    //کیفیت پرسنل كشتي
                    else if (index.Key.DictionaryName == ""ShipStaffQuality"")
                    {
                        x += (110 - Convert.ToDecimal(job.CustomFields[""CommandersInquiryScore""])) / 100 * 0.328m;
                        x += (110 - Convert.ToDecimal(job.CustomFields[""FirstOfficersInquiryScore""])) / 100 * 0.138m;
                        x += (110 - Convert.ToDecimal(job.CustomFields[""ChiefEngineersInquiryScore""])) / 100 * 0.277m;
                        x += (110 - Convert.ToDecimal(job.CustomFields[""SecondOfficersInquiryScore""])) / 100 * 0.128m;
                        x += (110 - Convert.ToDecimal(job.CustomFields[""ElectronicEngineersInquiryScore""])) / 100 * 0.129m;
                    }
                    //کارکرد عملیاتی کشتی ( روز )
                    else if (index.Key.DictionaryName == ""ShipOperationalEfficiency"")
                    {
                        x = Convert.ToDecimal(job.CustomFields[""ShipOperationalEfficiency""]) / (Convert.ToInt32(job.CustomFields[""ShipNo""]) * 30 * 3);
                    }
                    //مسیر حرکت کشتی
                    else if (index.Key.DictionaryName == ""ShipPath"")
                    {
                        decimal no = Convert.ToDecimal(job.CustomFields[""ShipNo""]);
                        for (int i = 1; i < no + 1; i++)
                        {
                            decimal type = Convert.ToDecimal(job.CustomFields[""ShipPath"" + i]);
                            if (type == 1) type = 9;
                            else if (type == 2) type = 9;
                            else if (type == 3) type = 7;
                            else if (type == 4) type = 5;
                            else if (type == 5) type = 5;
                            else if (type == 6) type = 9;
                            else if (type == 7) type = 5;
                            else if (type == 8) type = 8;
                            else if (type == 9) type = 9;
                            x += type;
                        }
                        x = x / no / 9;
                    }
                    else continue;

                    Utils.AddEmployeePoint(job, index, ""gross"", x);
                }

            }
            Utils.AddCalculationPoint(""MinPereiodicMaintenancePlaningTime"", calcPointMinCostControl);
            Utils.AddCalculationPoint(""MinTechnicalOffHireTime"", calcPointMinTechnicalOffHireTime);
", RuleType.PerCalculation, 2);
                ruleRep.Add(rule2);
                rules.Add(rule2);


                var rule3 = new Rule(new RuleId(ruleRep.GetNextId()), "شاخص های مرحله دوم",
@"
           if (data.PathNo != 2) return;
            var jobs = data.JobPositions.Where(j => j.Job.DictionaryName == ""TechnicalInspector"");
            if (jobs == null || jobs.Count() == 0) return;
            foreach (var job in jobs)
            {

                //محاسبه تعداد شاخص ها با اهمیت های مختلف
                decimal offHire = 0;
                foreach (var index in job.Indices)
                {
                    decimal x = 0;

                    // Technical ------------------------------------------------------------------------
                    x = 0;
                    //کنترل هزینه
                    if (index.Key.DictionaryName == ""CostControl"")
                    {
                        continue;
                        //x = Utils.GetPoint(data, job, index, ""gross"").Value;
                        //var calcPointMinCostControl = Utils.GetCalculationPoint(data, ""MinCostControl"");
                        //x = Math.Min(0.05m, calcPointMinCostControl.Value) / x;
                    }
                    //برنامه ریزی و کنترل تعمیرات ادواری (زمان
                    else if (index.Key.DictionaryName == ""PereiodicMaintenancePlaningTime"")
                    {
                        if (job.CustomFields[""PeriodicMaintenance""] == ""0"")
                            continue;
                        else
                        {
                            x = Utils.GetPoint(data, job, index, ""gross"").Value;
                            var MinPereiodicMaintenancePlaningTime = Utils.GetCalculationPoint(data,
                                ""MinPereiodicMaintenancePlaningTime"" );
                            x = Math.Min(0.1m, MinPereiodicMaintenancePlaningTime.Value) / x;
                        }
                    }
                    //مدت زمان Off HIRE فني (ساعت)
                    else if (index.Key.DictionaryName == ""TechnicalOffHireTime"")
                    {
                        x = Utils.GetPoint(data, job, index, ""gross"").Value;
                        var MinTechnicalOffHireTime = Utils.GetCalculationPoint(data, ""MinTechnicalOffHireTime"");
                        x = Math.Min(1, MinTechnicalOffHireTime.Value) / x;
                        offHire = x;
                    }

                    else if (index.Key.DictionaryName == ""ContractorEfficiencyControl"")
                    {
                        var VoyageMaintenancePlannigAbility =
                            Utils.GetPoint(data, job, ""VoyageMaintenancePlannigAbility"", ""gross"").Value;
                        var TechnicalOffHireTime = Utils.GetPoint(job, ""TechnicalOffHireTime"", ""gross"").Value;
                        x = (VoyageMaintenancePlannigAbility + TechnicalOffHireTime) / 2;
                    }
                    else if (index.Key.DictionaryName == ""ShipTechnicalEfficiencyControl"")
                    {
                        var VesselFaultSumNo = Utils.GetPoint(data, job, ""VesselFaultSumNo"", ""gross"").Value;
                        var TechnicalOffHireTime = Utils.GetPoint(job, ""TechnicalOffHireTime"", ""gross"").Value;
                        x = (VesselFaultSumNo + TechnicalOffHireTime) / 2;
                    }
                    else continue;

                    Utils.AddEmployeePoint(job, index, ""gross"", x);

                }

            }
", RuleType.PerCalculation, 3);
                ruleRep.Add(rule3);
                rules.Add(rule3);

                var rule4 = new Rule(new RuleId(ruleRep.GetNextId()), "محاسبه نهایی",
@"
            if (data.PathNo != 2) return;
            decimal z = Utils.GetCalculationPoint(data, ""z"").Value;
            //محاسبه تعداد شاخص ها با اهمیت های مختلف
            decimal total = 0;
            int importanceWeight = 0;

            foreach (var job in data.JobPositions)
            {
                decimal a1 = Utils.GetPoint(data, job, ""a1"").Value;
                decimal b1 = Utils.GetPoint(data, job, ""b1"").Value;
                decimal c1 = Utils.GetPoint(data, job, ""c1"").Value;
                decimal d1 = Utils.GetPoint(data, job, ""d1"").Value;
                decimal e1 = Utils.GetPoint(data, job, ""e1"").Value;

                decimal a2 = Utils.GetPoint(data, job, ""a2"").Value;
                decimal b2 = Utils.GetPoint(data, job, ""b2"").Value;
                decimal c2 = Utils.GetPoint(data, job, ""c2"").Value;
                decimal d2 = Utils.GetPoint(data, job, ""d2"").Value;
                decimal e2 = Utils.GetPoint(data, job, ""e2"").Value;

                decimal a3 = Utils.GetPoint(data, job, ""a3"").Value;
                decimal b3 = Utils.GetPoint(data, job, ""b3"").Value;
                decimal c3 = Utils.GetPoint(data, job, ""c3"").Value;
                decimal d3 = Utils.GetPoint(data, job, ""d3"").Value;
                decimal e3 = Utils.GetPoint(data, job, ""e3"").Value;

                decimal sumGeneral = 0;
                decimal sumTechnical = 0;
                decimal sumEqualizer = 0;
                Random rnd = new Random();
                foreach (var index in job.Indices)
                {
                    RulePoint p = null;
                    decimal x = 0;
                    if ((new string[5] { ""CostControl"", ""PereiodicMaintenancePlaningTime"", ""TechnicalOffHireTime"", 
                        ""ShipTechnicalEfficiencyControl"", ""ContractorEfficiencyControl"" }).Contains(index.Key.DictionaryName))
                        p = Utils.GetPoint(job, index, ""gross"");
                    else
                        p = Utils.GetPoint(data, job, index, ""gross"");
                    if (p == null)
                        continue;
                    else
                        x = p.Value;

                    if (Utils.IndexGroup(index) == ""General"")
                    {
                        if (Utils.IndexField(index, ""Importance"") == ""1"")
                        {
                            sumGeneral += Utils.AddEmployeePoint(job, index, ""net"", a1 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""3"")
                        {
                            sumGeneral += Utils.AddEmployeePoint(job, index, ""net"", b1 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""5"")
                        {
                            sumGeneral += Utils.AddEmployeePoint(job, index, ""net"", c1 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""7"")
                        {
                            sumGeneral += Utils.AddEmployeePoint(job, index, ""net"", d1 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""9"")
                        {
                            sumGeneral += Utils.AddEmployeePoint(job, index, ""net"", e1 * x).Value;
                        }
                    }
                    else if (Utils.IndexGroup(index) == ""Technical"")
                    {
                        if (Utils.IndexField(index, ""Importance"") == ""1"")
                        {
                            sumTechnical += Utils.AddEmployeePoint(job, index, ""net"", a2 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""3"")
                        {
                            sumTechnical += Utils.AddEmployeePoint(job, index, ""net"", b2 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""5"")
                        {
                            sumTechnical += Utils.AddEmployeePoint(job, index, ""net"", c2 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""7"")
                        {
                            sumTechnical += Utils.AddEmployeePoint(job, index, ""net"", d2 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""9"")
                        {
                            sumTechnical += Utils.AddEmployeePoint(job, index, ""net"", e2 * x).Value;
                        }
                    }
                    else if (Utils.IndexGroup(index) == ""Equalizer"")
                    {
                        if (Utils.IndexField(index, ""Importance"") == ""1"")
                        {
                            sumEqualizer += Utils.AddEmployeePoint(job, index, ""net"", a3 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""3"")
                        {
                            sumEqualizer += Utils.AddEmployeePoint(job, index, ""net"", b3 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""5"")
                        {
                            sumEqualizer += Utils.AddEmployeePoint(job, index, ""net"", c3 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""7"")
                        {
                            sumEqualizer += Utils.AddEmployeePoint(job, index, ""net"", d3 * x).Value;
                        }
                        else if (Utils.IndexField(index, ""Importance"") == ""9"")
                        {
                            sumEqualizer += Utils.AddEmployeePoint(job, index, ""net"", e3 * x).Value;
                        }
                    }
                }
                Utils.AddEmployeePoint(job, ""final-general"", sumGeneral);
                Utils.AddEmployeePoint(job, ""initial-technical"", sumTechnical);
                Utils.AddEmployeePoint(job, ""final-equalizer"", sumEqualizer);
                sumTechnical = sumTechnical * (1 - z / 2 + sumEqualizer);
                Utils.AddEmployeePoint(job, ""final-technical"", sumTechnical);
                sumGeneral = Math.Min(sumGeneral + sumTechnical, 100);
                Utils.AddEmployeePoint(job, ""final-job"", sumGeneral);
                total += sumGeneral * job.WorkTimePercent * job.Weight / 100;
                importanceWeight+=job.WorkTimePercent*job.Weight/100;
            }
            if (importanceWeight > 0)
                Utils.AddEmployeePoint(""final"", total / importanceWeight, true);

          
", RuleType.PerCalculation, 4);
                ruleRep.Add(rule4);
                rules.Add(rule4);

                uow.Commit();
            }
            #endregion

            #region  PMS Admin

            uows = new MITD.Domain.Repository.UnitOfWorkScope(
               new Data.NH.NHUnitOfWorkFactory(() =>
               {
                   return PMSAdmin.Persistence.NH.PMSAdminSession.GetSession();
               }));

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var cftRep = new PMSAdmin.Persistence.NH.CustomFieldRepository(uow);

                #region Employee CustomFields

                //for (int i = 0; i < 10; i++)
                //{
                //    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                //        "فبلد دلخواه کارمند" + i, "EmployeeCft" + i, 0, 100, EntityTypeEnum.Employee, "string");
                //    cftRep.Add(cft);
                //    employeeCftList.Add(cft);
                //}

                #endregion

                #region JobIndex CustomFields Creation

                //for (int i = 0; i < 9; i++)
                //{
                //    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                //        "فبلد دلخواه شاخص شغل" + i, "JobIndexCft" + i, 0, 100, EntityTypeEnum.JobIndex, "string");
                //    cftRep.Add(cft);
                //    jobIndexCftList.Add(cft);
                //}
                var imp = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    "اهمیت", "Importance", 0, 10, EntityTypeEnum.JobIndex, "string");
                cftRep.Add(imp);
                jobIndexCftList.Add(imp);

                #endregion

                #region Job CustomFields Creation

                var cft1 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    "بودجه سالانه مصوب (U.S $)", "DeclaredAnnualBudget", 0, 1000000, EntityTypeEnum.Job, "string");
                cftRep.Add(cft1);
                jobCftList.Add(cft1);

                var cft2 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    "کل هزینه انجام شده سالانه (U.S $)", "TotalAnnualCost", 0, 1000000, EntityTypeEnum.Job, "string");
                cftRep.Add(cft2);
                jobCftList.Add(cft2);

                var cft3 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    "مدت زمان برنامه ریزی شده تعمیرات ادواری در دوره ارزیابی سه ماهه", "EstimatedPereiodicMaintenanceTime", 0, 1000, EntityTypeEnum.Job, "string");
                cftRep.Add(cft3);
                jobCftList.Add(cft3);

                var cft4 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    "مدت زمان واقعی تعمیرات ادواری در دوره ارزیابی سه ماهه", "ActualPereiodicMaintenanceTime", 0, 1000, EntityTypeEnum.Job, "string");
                cftRep.Add(cft4);
                jobCftList.Add(cft4);

                var cft5 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " تعداد بازدیدهای انجام شده توسط FSC و PSC برای شناورها", "PSCFSCvesselInspectionNO", 0, 50, EntityTypeEnum.Job, "string");
                cftRep.Add(cft5);
                jobCftList.Add(cft5);

                var cft6 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " تعمیرات ادواری", "PeriodicMaintenance", 0, 50, EntityTypeEnum.Job, "string");
                cftRep.Add(cft6);
                jobCftList.Add(cft6);

                var cft7 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " تعداد کشتی", "ShipNo", 0, 50, EntityTypeEnum.Job, "string");
                cftRep.Add(cft7);
                jobCftList.Add(cft7);

                var cft8 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " مکان و محل تعمیرات", "MaintenaceLocation", 0, 50, EntityTypeEnum.Job, "string");
                cftRep.Add(cft8);
                jobCftList.Add(cft8);

                var cft9 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " میانگین نمره ارزشیابی فرماندهان ", "CommandersInquiryScore", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft9);
                jobCftList.Add(cft9);

                var cft10 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " میانگین نمره ارزشیابی افسران اول", "FirstOfficersInquiryScore", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft10);
                jobCftList.Add(cft10);

                var cft11 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " میانگین نمره ارزشیابی سرمهندسان", "ChiefEngineersInquiryScore", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft11);
                jobCftList.Add(cft11);

                var cft12 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " میانگین نمره ارزشیابی مهندسان دوم ", "SecondOfficersInquiryScore", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft12);
                jobCftList.Add(cft12);

                var cft13 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " میانگین نمره ارزشیابی مهندسان الکترونیک ", "ElectronicEngineersInquiryScore", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft13);
                jobCftList.Add(cft13);

                var cft14 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " کارکرد عملیاتی کشتی ", "ShipOperationalEfficiency", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft14);
                jobCftList.Add(cft14);

                var cft15 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    " بهره وری دوره های آموزشی ", "TechnicalCourseAttending", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft15);
                jobCftList.Add(cft15);

                var cft16 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    "Off Hire فنی مدت زمان  ", "TechnicalOffHireTime", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft16);
                jobCftList.Add(cft16);

                var cft17 = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                   "مجموع تعداد نواقص اعلام شده توسط FSC و PSC برای شناورها  ", "VesselFaultSumNo", 0, 100, EntityTypeEnum.Job, "string");
                cftRep.Add(cft17);
                jobCftList.Add(cft17);




                for (int i = 1; i < 7; i++)
                {
                    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                        "سن كشتی" + i, "ShipAge" + i, 0, 100, EntityTypeEnum.Job, "string");
                    cftRep.Add(cft);
                    jobCftList.Add(cft);
                }

                for (int i = 1; i < 7; i++)
                {
                    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                        "نوع كشتی" + i, "ShipType" + i, 0, 10, EntityTypeEnum.Job, "string");
                    cftRep.Add(cft);
                    jobCftList.Add(cft);
                }

                for (int i = 1; i < 7; i++)
                {
                    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                        "مسیر حرکت کشتی" + i, "ShipPath" + i, 4, 10, EntityTypeEnum.Job, "string");
                    cftRep.Add(cft);
                    jobCftList.Add(cft);
                }

                #endregion

                var jobRep = new PMSAdmin.Persistence.NH.JobRepository(uow);

                #region Jobs Creation
                var job1 = new PMSAdmin.Domain.Model.Jobs.Job(jobRep.GetNextId(),
                        " بازرس فنی", "TechnicalInspector");
                job1.AssignCustomFields(jobCftList);
                jobRep.AddJob(job1);
                jobList.Add(job1);

                var job2 = new PMSAdmin.Domain.Model.Jobs.Job(jobRep.GetNextId(),
                        " مدیر امور بازرسی", "TechnicalManager");
                jobRep.AddJob(job2);
                jobList.Add(job2);



                #endregion

                var jobPositionRep = new PMSAdmin.Persistence.NH.JobPositionRepository(uow);

                #region JobPositions Creation

                for (int i = 1; i < 6; i++)
                {
                    var jobPosition = new PMSAdmin.Domain.Model.JobPositions.JobPosition(jobPositionRep.GetNextId(),
                        " بازرس فنی" + i, "TechnicalInspector" + i);
                    jobPositionRep.Add(jobPosition);
                    jobPositionList.Add(jobPosition);
                }

                technicalManagerJobPosition = new PMSAdmin.Domain.Model.JobPositions.JobPosition(jobPositionRep.GetNextId(),
                        "  مدیر امور فنی", "TechnicalInspectorJobPosition");
                jobPositionRep.Add(technicalManagerJobPosition);

                #endregion

                var unitRep = new PMSAdmin.Persistence.NH.UnitRepository(uow);

                #region Unit Creation

                for (int i = 0; i < 10; i++)
                {
                    var unit = new PMSAdmin.Domain.Model.Units.Unit(unitRep.GetNextId(),
                        " واحد" + i, "Unit" + i);
                    unitRep.Add(unit);
                    unitList.Add(unit);
                }

                #endregion


                var jobIndexRep = new PMSAdmin.Persistence.NH.JobIndexRepository(uow);

                #region JobIndexes Creation

                var jobIndexCategory = new PMSAdmin.Domain.Model.JobIndices.JobIndexCategory(jobIndexRep.GetNextId(), null, "دسته شاخص فنی", "TechnicalJobIndexCategory");
                jobIndexRep.Add(jobIndexCategory);

                var jobIndex1 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "فرهنگ و تعهد سازمانی", "OrganizationalCultureAndCommitment");
                jobIndex1.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex1);
                GenralJobIndexList.Add(new JobindexDes { JobIndex = jobIndex1, Importance = "7", IsInquirable = true });

                var jobIndex2 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "رشد و تعالی سازمانی", "OrganizationalGrowthAndExcelency");
                jobIndex2.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex2);
                GenralJobIndexList.Add(new JobindexDes { JobIndex = jobIndex2, Importance = "7", IsInquirable = true });

                var jobIndex3 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "نظم و انضباط", "Discipline");
                jobIndex3.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex3);
                GenralJobIndexList.Add(new JobindexDes { JobIndex = jobIndex3, Importance = "9", IsInquirable = true });

                var jobIndex4 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "پایبندی به ارزشهای سازمانی", "AdherenceToOrganizarionValues");
                jobIndex4.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex4);
                GenralJobIndexList.Add(new JobindexDes { JobIndex = jobIndex4, Importance = "7", IsInquirable = true });

                var jobIndex5 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "تلاش و سخت کوشی", "HardWorking");
                jobIndex5.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex5);
                GenralJobIndexList.Add(new JobindexDes { JobIndex = jobIndex5, Importance = "9", IsInquirable = true });

                var jobIndex6 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "تسلط بر زبان انگلیسی", "EnglishLanquageSkills");
                jobIndex6.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex6);
                GenralJobIndexList.Add(new JobindexDes { JobIndex = jobIndex6, Importance = "7", IsInquirable = true });

                var jobIndex7 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "آشنایی با ICDL", "ICDLFamiliarity");
                jobIndex7.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex7);
                GenralJobIndexList.Add(new JobindexDes { JobIndex = jobIndex7, Importance = "5", IsInquirable = true });

                var jobIndex8 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "توان روحی و جسمی", "MentalAndPhisicalAbility");
                jobIndex8.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex8);
                GenralJobIndexList.Add(new JobindexDes { JobIndex = jobIndex8, Importance = "7", IsInquirable = true });

                var jobIndex81 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "کنترل هزینه", "CostControl");
                jobIndex81.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex81);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex81, Importance = "9", IsInquirable = false });

                var jobIndex9 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "كيفيت بودجه بندی", "BudgetQuality");
                jobIndex9.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex9);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex9, Importance = "7", IsInquirable = true });

                var jobIndex10 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "برنامه ریزی و کنترل تعمیرات ادواری (زمان)", "PereiodicMaintenancePlaningTime");
                jobIndex10.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex10);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex10, Importance = "9", IsInquirable = false });

                var jobIndex101 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                       "برنامه ریزی و کنترل تعمیرات ادواری (كيفيت)", "PereiodicMaintenancePlaningQuality");
                jobIndex101.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex101);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex101, Importance = "7", IsInquirable = true });

                var jobIndex11 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "قابلیت برنامه ریزی و کنترل تعمیرات سفری", "VoyageMaintenancePlannigAbility");
                jobIndex11.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex11);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex11, Importance = "5", IsInquirable = true });

                var jobIndex12 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "پروسه انتخاب پیمانکاران", "ContractorSelectionProcess");
                jobIndex12.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex12);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex12, Importance = "5", IsInquirable = true });

                var jobIndex13 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "مدت زمان Off HIRE فني", "TechnicalOffHireTime");
                jobIndex13.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex13);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex13, Importance = "9", IsInquirable = false });

                var jobIndex131 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "کنترل عملکرد کشتی (فنی و ایمنی)", "ShipTechnicalEfficiencyControl");
                jobIndex131.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex131);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex131, Importance = "9", IsInquirable = false });

                var jobIndex132 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                       "کنترل عملکرد پیمانکاران", "ContractorEfficiencyControl");
                jobIndex132.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex132);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex132, Importance = "7", IsInquirable = false });

                var jobIndex14 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "مجموع تعداد نواقص اعلام شده توسط FSC و PSC برای شناورها", "VesselFaultSumNo");
                jobIndex14.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex14);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex14, Importance = "7", IsInquirable = false });

                var jobIndex15 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "آنالير مشكلات فني ، پیگیری نواقص و ارائه راهکار", "ProblemSolvingAbility");
                jobIndex15.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex15);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex15, Importance = "7", IsInquirable = true });

                var jobIndex16 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "به روزنگه داشتن گواهینامه ها و رعایت الزامات قانونی", "CertificateUpdates");
                jobIndex16.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex16);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex16, Importance = "7", IsInquirable = true });

                var jobIndex17 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "آشنایی با نرم افزارهای مربوط به شغل", "JobSoftwareFamiliarity");
                jobIndex17.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex17);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex17, Importance = "5", IsInquirable = true });

                var jobIndex18 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "بهره وري دوره های تخصصی ( دوره مهندس ناظری)", "TechnicalCourseAttending");
                jobIndex18.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex18);
                TechnicalJobIndexList.Add(new JobindexDes { JobIndex = jobIndex18, Importance = "5", IsInquirable = false });

                var jobIndex19 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "تعداد کشتی", "ShipNo");
                jobIndex19.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex19);
                EqulizerjobIndexList.Add(new JobindexDes { JobIndex = jobIndex19, Importance = "7", IsInquirable = false });

                var jobIndex20 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "مکان و محل تعمیرات", "MaintenaceLocation");
                jobIndex20.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex20);
                EqulizerjobIndexList.Add(new JobindexDes { JobIndex = jobIndex20, Importance = "7", IsInquirable = true });

                var jobIndex21 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "کارکرد عملیاتی کشتی ( روز )", "ShipOperationalEfficiency");
                jobIndex21.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex21);
                EqulizerjobIndexList.Add(new JobindexDes { JobIndex = jobIndex21, Importance = "7", IsInquirable = false });

                var jobIndex22 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "سن كشتی", "ShipAge");
                jobIndex22.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex22);
                EqulizerjobIndexList.Add(new JobindexDes { JobIndex = jobIndex22, Importance = "9", IsInquirable = false });

                var jobIndex23 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "نوع كشتی", "ShipType");
                jobIndex23.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex23);
                EqulizerjobIndexList.Add(new JobindexDes { JobIndex = jobIndex23, Importance = "7", IsInquirable = false });

                var jobIndex24 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "مسیر حرکت کشتی", "ShipPath");
                jobIndex24.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex24);
                EqulizerjobIndexList.Add(new JobindexDes { JobIndex = jobIndex24, Importance = "7", IsInquirable = false });

                var jobIndex25 = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        "کیفیت پرسنل كشتي", "ShipStaffQuality");
                jobIndex25.AssignCustomFields(jobIndexCftList);
                jobIndexRep.Add(jobIndex25);
                EqulizerjobIndexList.Add(new JobindexDes { JobIndex = jobIndex25, Importance = "7", IsInquirable = false });

                #endregion

                var policyRep = new PMSAdmin.Persistence.NH.PolicyRepository(uow);

                #region Policy Creation
                policy = new PMSAdmin.Domain.Model.Policies.RuleEngineBasedPolicy(policyRep.GetNextId(),
                        " خظ کش پرتر", "PorterRulerPolicy");
                policyRep.Add(policy);
                foreach (var rule in rules)
                {
                    policy.AssignRule(rule);
                }

                policy.AssignRuleFunction(rf);

                #endregion

                uow.Commit();
            }

            #endregion

            uows = new MITD.Domain.Repository.UnitOfWorkScope(
              new Data.NH.NHUnitOfWorkFactory(() => PMS.Persistence.NH.PMSSession.GetSession()));

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var periodRep = new PeriodRepository(uow);
                var periodManagerService = new PeriodManagerService(periodRep, null, null, null, null, null, null, null, null, null, null, null);
                #region Period creation

                period = new Period(new PeriodId(periodRep.GetNextId()), Guid.NewGuid().ToString(), DateTime.Now, DateTime.Now,91);
                period.ChangeActiveStatus(periodManagerService, true);
                periodRep.Add(period);
                #endregion

                var jobIndexRep = new PMS.Persistence.NH.JobIndexRepository(uow);

                #region JobIndex Creation

                var jobIndexGroupGenaral = new PMS.Domain.Model.JobIndices.JobIndexGroup(jobIndexRep.GetNextId(), period, null,
                    "گروه شاخص های عمومی", "General");
                jobIndexRep.Add(jobIndexGroupGenaral);
                var jobIndexGroupTechnical = new PMS.Domain.Model.JobIndices.JobIndexGroup(jobIndexRep.GetNextId(), period, null,
                   "گروه شاخص های تخصصی", "Technical");
                jobIndexRep.Add(jobIndexGroupTechnical);

                var jobIndexGroupEqualizer = new PMS.Domain.Model.JobIndices.JobIndexGroup(jobIndexRep.GetNextId(), period, null,
                   "گروه شاخص های یکسان ساز", "Equalizer");
                jobIndexRep.Add(jobIndexGroupEqualizer);

                foreach (var itm in GenralJobIndexList)
                {

                    var sharedJobIndex =
                        new PMS.Domain.Model.JobIndices.SharedJobIndex(
                            new PMS.Domain.Model.JobIndices.SharedJobIndexId(itm.JobIndex.Id.Id), itm.JobIndex.Name,
                            itm.JobIndex.DictionaryName);
                    var jobIndex = new PMS.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), period,
                        sharedJobIndex, jobIndexGroupGenaral, itm.IsInquirable);

                    var dicSharedCutomField = jobIndexCftList
                        .Where(j => itm.JobIndex.CustomFieldTypeIdList.Select(i => i.Id).Contains(j.Id.Id)).Select(p =>
                            new PMS.Domain.Model.JobIndices.SharedJobIndexCustomField(
                                new PMS.Domain.Model.JobIndices.SharedJobIndexCustomFieldId(p.Id.Id), p.Name,
                                p.DictionaryName,
                                p.MinValue, p.MaxValue)).ToDictionary(s => s, s => s.DictionaryName == "Importance" ? itm.Importance : string.Empty);

                    jobIndex.UpdateCustomFields(dicSharedCutomField);
                    jobIndexInPeriodList.Add(jobIndex);
                    jobIndexRep.Add(jobIndex);

                }

                foreach (var itm in TechnicalJobIndexList)
                {

                    var sharedJobIndex =
                        new PMS.Domain.Model.JobIndices.SharedJobIndex(
                            new PMS.Domain.Model.JobIndices.SharedJobIndexId(itm.JobIndex.Id.Id), itm.JobIndex.Name,
                            itm.JobIndex.DictionaryName);
                    JobIndex jobIndex;
                    if (sharedJobIndex.DictionaryName == "CostControl")
                        jobIndex = new PMS.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), period,
                            sharedJobIndex, jobIndexGroupTechnical, itm.IsInquirable, 2);
                    else
                        jobIndex = new PMS.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), period,
                            sharedJobIndex, jobIndexGroupTechnical, itm.IsInquirable);

                    var dicSharedCutomField = jobIndexCftList
                        .Where(j => itm.JobIndex.CustomFieldTypeIdList.Select(i => i.Id).Contains(j.Id.Id)).Select(p =>
                            new PMS.Domain.Model.JobIndices.SharedJobIndexCustomField(
                                new PMS.Domain.Model.JobIndices.SharedJobIndexCustomFieldId(p.Id.Id), p.Name,
                                p.DictionaryName,
                                p.MinValue, p.MaxValue)).ToDictionary(s => s, s => s.DictionaryName == "Importance" ? itm.Importance : string.Empty);

                    jobIndex.UpdateCustomFields(dicSharedCutomField);
                    jobIndexInPeriodList.Add(jobIndex);
                    jobIndexRep.Add(jobIndex);

                }

                foreach (var itm in EqulizerjobIndexList)
                {

                    var sharedJobIndex =
                        new PMS.Domain.Model.JobIndices.SharedJobIndex(
                            new PMS.Domain.Model.JobIndices.SharedJobIndexId(itm.JobIndex.Id.Id), itm.JobIndex.Name,
                            itm.JobIndex.DictionaryName);
                    var jobIndex = new PMS.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), period,
                        sharedJobIndex, jobIndexGroupEqualizer, itm.IsInquirable);

                    var dicSharedCutomField = jobIndexCftList
                        .Where(j => itm.JobIndex.CustomFieldTypeIdList.Select(i => i.Id).Contains(j.Id.Id)).Select(p =>
                            new PMS.Domain.Model.JobIndices.SharedJobIndexCustomField(
                                new PMS.Domain.Model.JobIndices.SharedJobIndexCustomFieldId(p.Id.Id), p.Name,
                                p.DictionaryName,
                                p.MinValue, p.MaxValue)).ToDictionary(s => s, s => s.DictionaryName == "Importance" ? itm.Importance : string.Empty);

                    jobIndex.UpdateCustomFields(dicSharedCutomField);
                    jobIndexInPeriodList.Add(jobIndex);
                    jobIndexRep.Add(jobIndex);

                }
                #endregion

                var jobRep = new PMS.Persistence.NH.JobRepository(uow);

                #region Job creation

                foreach (var pmsAdminJob in jobList)
                {
                    var jobJobIndices = jobIndexInPeriodList.Select(jobIndex => new JobJobIndex(jobIndex.Id, true, true, true)).ToList();
                    if (pmsAdminJob.DictionaryName == "TechnicalManager")
                        jobJobIndices = new List<JobJobIndex>();

                    var job = new PMS.Domain.Model.Jobs.Job(period, new PMS.Domain.Model.Jobs.SharedJob(
                        new PMS.Domain.Model.Jobs.SharedJobId(pmsAdminJob.Id.Id), pmsAdminJob.Name, pmsAdminJob.DictionaryName), jobCftList
                        .Where(j => pmsAdminJob.CustomFieldTypeIdList.Select(i => i.Id)
                            .Contains(j.Id.Id)).Select(p =>
                                new PMS.Domain.Model.Jobs.JobCustomField(new PMS.Domain.Model.Jobs.JobCustomFieldId(period.Id, new SharedJobCustomFieldId(p.Id.Id), new SharedJobId(pmsAdminJob.Id.Id))
                                    , new SharedJobCustomField(new SharedJobCustomFieldId(p.Id.Id), p.Name, p.DictionaryName, p.MinValue, p.MaxValue, p.TypeId))).ToList(), jobJobIndices);
                    jobInPeriodList.Add(job);
                    jobRep.Add(job);
                }
                #endregion

                var unitRep = new PMS.Persistence.NH.UnitRepository(uow);

                #region Unit Creation

                foreach (var pmsAdminUnit in unitList)
                {
                    var unit = new PMS.Domain.Model.Units.Unit(period, new PMS.Domain.Model.Units.SharedUnit(
                        new PMS.Domain.Model.Units.SharedUnitId(pmsAdminUnit.Id.Id), pmsAdminUnit.Name, pmsAdminUnit.DictionaryName), null);
                    unitInPeriodList.Add(unit);
                    unitRep.Add(unit);
                }
                #endregion

                var jobPositionRep = new PMS.Persistence.NH.JobPositionRepository(uow);

                #region JobPosition Creation

                var jpIndex = 0;
                var jobPositionParent = new PMS.Domain.Model.JobPositions.JobPosition(period,
                        new Domain.Model.JobPositions.SharedJobPosition(new Domain.Model.JobPositions.SharedJobPositionId(technicalManagerJobPosition.Id.Id), technicalManagerJobPosition.Name, technicalManagerJobPosition.DictionaryName)
                        , null,
                        jobInPeriodList.Single(j => j.DictionaryName == "TechnicalManager"),
                        unitInPeriodList.First()
                        );
                jobPositionInPeriodList.Add(jobPositionParent);
                jobPositionRep.Add(jobPositionParent);
                foreach (var pmsAdminJobPosition in jobPositionList.Where(j => j.DictionaryName != "TechnicalInspectorJobPosition").ToList())
                {
                    var jobPosition = new PMS.Domain.Model.JobPositions.JobPosition(period,
                        new Domain.Model.JobPositions.SharedJobPosition(new Domain.Model.JobPositions.SharedJobPositionId(pmsAdminJobPosition.Id.Id), pmsAdminJobPosition.Name, pmsAdminJobPosition.DictionaryName)
                        , jobPositionParent,
                        jobInPeriodList.First(),
                        unitInPeriodList[jpIndex]
                        );
                    jobPositionInPeriodList.Add(jobPosition);
                    jobPositionRep.Add(jobPosition);
                    jpIndex++;
                }
                #endregion

                var employeeRep = new PMS.Persistence.NH.EmployeeRepository(uow);

                #region Employee Creation




                var employee1 =
                        new PMS.Domain.Model.Employees.Employee(
                            ((1 + 1) * 2000).ToString(), period, "کارمند" + 1,
                            "کارمندیان" + 1);

                var jobPositionInPeriod1 = jobPositionInPeriodList[1];

                var jobcustomFields1 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod1.JobId)).CustomFields;
                if (jobcustomFields1 != null && jobcustomFields1.Count != 0)
                {
                    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee1, jobPositionInPeriod1, period.StartDate, period.EndDate, 100, 1,
                    jobcustomFields1.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value1)).ToList()
                    );
                    employee1.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                }

                empList.Add(employee1);
                employeeRep.Add(employee1);


                var employee2 =
                            new PMS.Domain.Model.Employees.Employee(
                                ((2 + 2) * 2000).ToString(), period, "کارمند" + 2,
                                "کارمندیان" + 2);

                var jobPositionInPeriod2 = jobPositionInPeriodList[2];

                var jobcustomFields2 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod2.JobId)).CustomFields;
                if (jobcustomFields2 != null && jobcustomFields2.Count != 0)
                {
                    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee2, jobPositionInPeriod2, period.StartDate, period.EndDate, 100, 1,
                    jobcustomFields2.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value2)).ToList()
                    );
                    employee2.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                }

                empList.Add(employee2);
                employeeRep.Add(employee2);


                var employee3 =
                           new PMS.Domain.Model.Employees.Employee(
                               ((3 + 3) * 2000).ToString(), period, "کارمند" + 3,
                               "کارمندیان" + 3);

                var jobPositionInPeriod3 = jobPositionInPeriodList[3];

                var jobcustomFields3 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod3.JobId)).CustomFields;
                if (jobcustomFields3 != null && jobcustomFields3.Count != 0)
                {
                    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee3, jobPositionInPeriod3, period.StartDate, period.EndDate, 100, 1,
                    jobcustomFields3.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value3)).ToList()
                    );
                    employee3.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                }

                empList.Add(employee3);
                employeeRep.Add(employee3);


                var employee4 =
                           new PMS.Domain.Model.Employees.Employee(
                              ((4 + 4) * 2000).ToString(), period, "کارمند" + 4,
                               "کارمندیان" + 4);

                var jobPositionInPeriod4 = jobPositionInPeriodList[4];

                var jobcustomFields4 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod4.JobId)).CustomFields;
                if (jobcustomFields4 != null && jobcustomFields4.Count != 0)
                {
                    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee4, jobPositionInPeriod4, period.StartDate, period.EndDate, 100, 1,
                    jobcustomFields4.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value4)).ToList()
                    );
                    employee4.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                }

                empList.Add(employee4);
                employeeRep.Add(employee4);


                var employee5 =
                           new PMS.Domain.Model.Employees.Employee(
                              ((5 + 5) * 2000).ToString(), period, "کارمند" + 5,
                               "کارمندیان" + 5);

                var jobPositionInPeriod5 = jobPositionInPeriodList[5];

                var jobcustomFields5 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod5.JobId)).CustomFields;
                if (jobcustomFields5 != null && jobcustomFields5.Count != 0)
                {
                    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee5, jobPositionInPeriod5, period.StartDate, period.EndDate, 100, 1,
                    jobcustomFields5.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value5)).ToList()
                    );
                    employee5.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                }

                empList.Add(employee5);
                employeeRep.Add(employee5);

                var employee0 =
                       new PMS.Domain.Model.Employees.Employee(
                          ((0 + 1) * 2000).ToString(), period, "مدیر",
                           "امور");

                var jobPositionInPeriod0 = jobPositionInPeriodList[0];
                var employeeJobPosition0 = new Domain.Model.Employees.EmployeeJobPosition(employee0, jobPositionInPeriod0, period.StartDate, period.EndDate, 100, 1,
                null
                );
                employee0.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition0 }, periodManagerService);
                empList.Add(employee0);
                employeeRep.Add(employee0);


                //for (int i = 0; i < 100; i++)
                //{
                //    var employee =
                //        new PMS.Domain.Model.Employees.Employee(
                //            ((5 + i)).ToString(), period, "کارمند" + i*10,
                //            "کارمندیان" + i*10);

                //    var jobPositionInPeriod = jobPositionInPeriodList[5];

                //    var jobcustomFields =
                //        jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod.JobId)).CustomFields;
                //    if (jobcustomFields != null && jobcustomFields.Count != 0)
                //    {
                //        var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee,
                //            jobPositionInPeriod, period.StartDate, period.EndDate, 100,
                //            jobcustomFields.Select(
                //                j =>
                //                    new EmployeeJobCustomFieldValue(j.Id,
                //                        docSeedValues.Single(d => d.DicName == j.DictionaryName).Value5)).ToList()
                //            );
                //        employee.AssignJobPositions(
                //            new List<Domain.Model.Employees.EmployeeJobPosition> {employeeJobPosition},
                //            periodManagerService);

                //    }

                //    empList.Add(employee);
                //    employeeRep.Add(employee);
                //}

                #endregion

                uow.Commit();
            }



            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var jobPositionRep = new PMS.Persistence.NH.JobPositionRepository(uow);
                var jobRep = new PMS.Persistence.NH.JobRepository(uow);
                var jobIndexRep = new PMS.Persistence.NH.JobIndexRepository(uow);
                var inquiryRep = new InquiryJobIndexPointRepository(uow);
                var inquiryConfiguratorService = new JobPositionInquiryConfiguratorService(jobPositionRep);

                foreach (var jobPosition in jobPositionInPeriodList)
                {
                    var jobp = jobPositionRep.GetBy(jobPosition.Id);
                    jobp.ConfigeInquirer(inquiryConfiguratorService, false);
                }
                uow.Commit();
            }

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                var jobPositionRep = new PMS.Persistence.NH.JobPositionRepository(uow);
                var jobRep = new PMS.Persistence.NH.JobRepository(uow);
                var jobIndexRep = new PMS.Persistence.NH.JobIndexRepository(uow);
                var inquiryRep = new InquiryJobIndexPointRepository(uow);
                var inquiryConfiguratorService = new JobPositionInquiryConfiguratorService(jobPositionRep);
                var periodRep = new PeriodRepository(uow);

                foreach (var jobPosition in jobPositionInPeriodList)
                {
                    var jobp = jobPositionRep.GetBy(jobPosition.Id);
                    foreach (var itm in jobp.ConfigurationItemList)
                    {
                        var job = jobRep.GetById(itm.JobPosition.JobId);
                        foreach (var jobIndexId in job.JobIndexList)
                        {
                            var jobIndex = jobIndexRep.GetById(jobIndexId.JobIndexId);
                            if ((jobIndex as JobIndex).IsInquireable)
                            {
                                string value = string.Empty;
                                if (jobPosition.DictionaryName.Contains("1"))
                                    value =
                                        docSeedValues.Single(
                                            d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
                                            .Value1;
                                if (jobPosition.DictionaryName.Contains("2"))
                                    value =
                                        docSeedValues.Single(
                                            d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
                                            .Value2;
                                if (jobPosition.DictionaryName.Contains("3"))
                                    value =
                                        docSeedValues.Single(
                                            d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
                                            .Value3;
                                if (jobPosition.DictionaryName.Contains("4"))
                                    value =
                                        docSeedValues.Single(
                                            d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
                                            .Value4;
                                if (jobPosition.DictionaryName.Contains("5"))
                                    value =
                                        docSeedValues.Single(
                                            d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
                                            .Value5;
                                var id = inquiryRep.GetNextId();
                                var inquiryIndexPoint = new Domain.Model.InquiryJobIndexPoints.InquiryJobIndexPoint(
                                    new Domain.Model.InquiryJobIndexPoints.InquiryJobIndexPointId(id),
                                    itm, jobIndex as Domain.Model.JobIndices.JobIndex, value);
                                inquiryRep.Add(inquiryIndexPoint);
                            }

                        }
                    }
                }
                var p = periodRep.GetById(period.Id);
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
                var pmsPolicy = policyRep.GetById(new PolicyId(policy.Id.Id));
                var p = periodRep.GetById(period.Id);
                var calcRep = new CalculationRepository(uow);
                var calc = new Calculation(calcRep.GetNextId(), p, pmsPolicy,
                    "محاسبه آزمایشی", DateTime.Now, empList[0].Id.EmployeeNo + ";" + empList[1].Id.EmployeeNo);
                calcRep.Add(calc);
                uow.Commit();
            }


        }



        public override void Down()
        {
        }
    }

}
