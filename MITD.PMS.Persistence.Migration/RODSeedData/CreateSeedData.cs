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
    [Profile("Debug")]
    public class CreateSeedData : Migration
    {
        public override void Up()
        {
            List<PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType> employeeCftList = new List<CustomFieldType>();
            List<PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType> jobIndexCftList = new List<CustomFieldType>();
            List<PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType> jobCftList = new List<CustomFieldType>();
            List<PMSAdmin.Domain.Model.Jobs.Job> jobList = new List<PMSAdmin.Domain.Model.Jobs.Job>();
            List<PMSAdmin.Domain.Model.JobIndices.JobIndex> jobIndexList = new List<PMSAdmin.Domain.Model.JobIndices.JobIndex>();
            List<PMSAdmin.Domain.Model.JobPositions.JobPosition> jobPositionList = new List<PMSAdmin.Domain.Model.JobPositions.JobPosition>();
            List<PMSAdmin.Domain.Model.Units.Unit> unitList = new List<PMSAdmin.Domain.Model.Units.Unit>();
            PMSAdmin.Domain.Model.Policies.RuleEngineBasedPolicy policy;
            Core.RuleEngine.Model.Rule rule;
            Core.RuleEngine.Model.RuleFunction rf;
            Period period;


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
public static string IndexGroup(KeyValuePair<JobIndex, Dictionary<Employee, Inquiry>> index)
{
    return index.Key.Group.DictionaryName;
}

public static string IndexField(KeyValuePair<JobIndex, Dictionary<Employee, Inquiry>> index, string fieldName)
{
    return index.Key.CustomFields[fieldName];
}

public static RulePoint AddPoint(JobPosition job, KeyValuePair<JobIndex, Dictionary<Employee, Inquiry>> index,
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

public static RulePoint AddPoint(JobPosition job, string name, decimal value, bool final = false)
{
    var res = new RulePoint { Name = name, Value = value, Final = final };
    if (!Utils.Res.JobResults.Any(j => j.Key == job.DictionaryName))
        Utils.Res.JobResults.Add(job.DictionaryName, new JobPositionResult());
    Utils.Res.JobResults[job.DictionaryName].Results.Add(res);
    return res;
}

public static RulePoint AddPoint(string name, decimal value, bool final = false)
{
    var res = new RulePoint { Name = name, Value = value, Final = final };
    Utils.Res.Results.Add(res);
    return res;
}

public static RulePoint GetPoint(JobPosition job, KeyValuePair<JobIndex, Dictionary<Employee, Inquiry>> index, string name)
{
    return Utils.Res.JobResults[job.DictionaryName].IndexResults[index.Key.DictionaryName].Single(j=>j.Name==name);
}

public static decimal GetInquiryByJobPositionName( KeyValuePair<JobIndex, Dictionary<Employee, Inquiry>> jobIndex,string inquirerJobPositionName )
{
    return Convert.ToDecimal(jobIndex.Value.SingleOrDefault                    (x=>x.Value.JobPosition.DictionaryName==inquirerJobPositionName).Value.Value);
}
"
                );
                rfRep.Add(rf);
                var ruleRep = new RuleRepository(uow);
                rule = new Rule(new RuleId(ruleRep.GetNextId()), "محاسبه امتیاز شاخص ها",
@"

//محاسبه تعداد شاخص ها با اهمیت های مختلف
decimal total = 0;
int it = 0;
foreach(var job in data.JobPositions)
{
	
    decimal a1 = Utils.IndexCount(job, ""Importance"", ""1"", ""General"");
    decimal b1 = Utils.IndexCount(job, ""Importance"", ""3"", ""General"");
    decimal c1 = Utils.IndexCount(job, ""Importance"", ""5"", ""General"");
    decimal d1 = Utils.IndexCount(job, ""Importance"", ""7"", ""General"");
    decimal e1 = Utils.IndexCount(job, ""Importance"", ""9"", ""General"");

    //محاسبه عدد وزنی شاخص های عمومی
    decimal y1 = 0;
    decimal n = (9 * a1 + 7 * b1 + 5 * c1 + 3 * d1 + e1);
    if (n != 0)
        y1 = 20 / n;

    a1 = 9 * y1;
    b1 = 7 * y1;
    c1 = 5 * y1;
    d1 = 3 * y1;
    e1 = y1;

    decimal a2 = Utils.IndexCount(job, ""Importance"", ""1"", ""Technical"");
    decimal b2 = Utils.IndexCount(job, ""Importance"", ""3"", ""Technical"");
    decimal c2 = Utils.IndexCount(job, ""Importance"", ""5"", ""Technical"");
    decimal d2 = Utils.IndexCount(job, ""Importance"", ""7"", ""Technical"");
    decimal e2 = Utils.IndexCount(job, ""Importance"", ""9"", ""Technical"");
                
    //محاسبه عدد وزنی شاخص های تخصصی
    decimal y2 = 0;
    decimal m = (9 * a2 + 7 * b2 + 5 * c2 + 3 * d2 + e2);
    if (m != 0)
        y2 = 80 / m;

    a2 = 9 * y2;
    b2 = 7 * y2;
    c2 = 5 * y2;
    d2 = 3 * y2;
    e2 = y2;

    decimal a3 = Utils.IndexCount(job, ""Importance"", ""1"", ""Equalizer"");
    decimal b3 = Utils.IndexCount(job, ""Importance"", ""3"", ""Equalizer"");
    decimal c3 = Utils.IndexCount(job, ""Importance"", ""5"", ""Equalizer"");
    decimal d3 = Utils.IndexCount(job, ""Importance"", ""7"", ""Equalizer"");
    decimal e3 = Utils.IndexCount(job, ""Importance"", ""9"", ""Equalizer"");

    //محاسبه عدد وزنی شاخص های یکسان ساز
    decimal z = .1m;
    decimal y3 = 0;
    decimal o = (9 * a3 + 7 * b3 + 5 * c3 + 3 * d3 + e3);
    if (o != 0)
        y3 = z / o;

    a3 = 9 * y3;
    b3 = 7 * y3;
    c3 = 5 * y3;
    d3 = 3 * y3;
    e3 = y3;

    decimal sum = 0;
    decimal sum2 = 0;
    decimal sum3 = 0;
    Random rnd = new Random();
    foreach (var index in job.Indices)
    {
        if (Utils.IndexGroup(index) == ""General"")
        {
            if(job.Job.DictionaryName==""TechnicalInspector"")
            {
               decimal x = 0;
//فرهنگ و تعهد سازمانی
               if(index.Key.DictionaryName==""OrganizationalCultureAndCommitment"")
               {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                
               } 
               else if(index.Key.DictionaryName==""OrganizationalGrowthAndExcelency"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""Discipline"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""AdherenceToOrganizarionValues"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""HardWorking"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""EnglishLanquageSkills"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""ICDLFamiliarity"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""MentalAndPhisicalAbility"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }




               Utils.AddPoint(job, index, ""gross"", x);

            }


            if (Utils.IndexField(index, ""Importance"") == ""9"")
            {
                sum += Utils.AddPoint(job, index, ""net"", a1 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""7"")
            {
                sum += Utils.AddPoint(job, index, ""net"", b1 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""5"")
            {
                sum += Utils.AddPoint(job, index, ""net"", c1 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""3"")
            {
                sum += Utils.AddPoint(job, index, ""net"", d1 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""1"")
            {
                sum += Utils.AddPoint(job, index, ""net"", e1 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
        }
        else if (Utils.IndexGroup(index) == ""Technical"")
        {
            if(job.Job.DictionaryName==""TechnicalInspector"")
            {
               decimal x = 0;
//کنترل هزینه
               if(index.Key.DictionaryName==""CostControl"")
               {
                   x = Math.Abs(Convert.ToDecimal(job.CustomFields[""DeclaredAnnualBudget""])-Convert.ToDecimal(job.CustomFields[""TotalAnnualCost""])+1)/Convert.ToDecimal(job.CustomFields[""DeclaredAnnualBudget""]);
               }
//برنامه ریزی و کنترل تعمیرات ادواری (زمان
               else if(index.Key.DictionaryName==""PereiodicMaintenancePlaningTime"")
               {
                   x = Math.Abs(Convert.ToDecimal(job.CustomFields[""EstimatedPereiodicMaintenanceTime""])-Convert.ToDecimal(job.CustomFields[""ActualPereiodicMaintenanceTime""])+1)/Convert.ToDecimal(job.CustomFields[""EstimatedPereiodicMaintenanceTime""]);
               }
//مدت زمان Off HIRE فني (ساعت)
             else if(index.Key.DictionaryName==""TechnicalOffHireTime"")
               {
                  x = (Convert.ToDecimal(job.CustomFields[""TechnicalOffHireTime""])/Convert.ToDecimal(job.CustomFields[""ShipOperationalEfficiency""]))+1;
               }
//مجموع تعداد نواقص اعلام شده توسط FSC و PSC برای شناورها
             else if(index.Key.DictionaryName==""VesselFaultSumNo"")
               {
                  decimal y = Convert.ToDecimal(job.CustomFields[""PSCFSCvesselInspectionNO""]);
                   x = 2/((Convert.ToDecimal(job.CustomFields[""VesselFaultSumNo""])/(y>0?y:1))+2);
               }
//بهره وري دوره های تخصصی ( دوره مهندس ناظری)
             else if(index.Key.DictionaryName==""TechnicalCourseAttending"")
               {
                   x = Convert.ToDecimal(job.CustomFields[""TechnicalCourseAttending""])/100;
               }

               else if(index.Key.DictionaryName==""BudgetQuality"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""PereiodicMaintenancePlaningQuality"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""VoyageMaintenancePlannigAbility"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""ContractorSelectionProcess"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""ProblemSolvingAbility"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""CertificateUpdates"")                
               {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }
               else if(index.Key.DictionaryName==""JobSoftwareFamiliarity"")
                {
                    var inquiryPoint=Utils.GetInquiryByJobPositionName(index,""TechnicalInspectorJobPosition"");
                     x=inquiryPoint/9;
                }

            
               Utils.AddPoint(job, index, ""gross"", x);

          }


            if (Utils.IndexField(index, ""Importance"") == ""9"")
            {
                sum2 += Utils.AddPoint(job, index, ""net"", a2 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""7"")
            {
                sum2 += Utils.AddPoint(job, index, ""net"", b2 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""5"")
            {
                sum2 += Utils.AddPoint(job, index, ""net"", c2 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""3"")
            {
                sum2 += Utils.AddPoint(job, index, ""net"", d2 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""1"")
            {
                sum2 += Utils.AddPoint(job, index, ""net"", e2 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
        }
        else if (Utils.IndexGroup(index) == ""Equalizer"")
        {
            if(job.Job.DictionaryName==""TechnicalInspector"")
            {
               decimal x = 0;
//سن كشتي
               if(index.Key.DictionaryName==""ShipAge"")
               {
                   int no = Convert.ToInt32(job.CustomFields[""ShipNo""]);
                   for (int i = 1; i < no+1; i++)
                  {
                     decimal age = Convert.ToDecimal(job.CustomFields[""ShipAge""+i]);
                     if(age>=0 && age<5) age=1;
                     else if(age>=5 && age<10) age=3;
                     else if(age>=10 && age<15) age=5;
                     else if(age>=15 && age<20) age=7;
                     else if(age>=20) age=9;
                     else continue;
                     x += age;
                  }
                  x=x/no/9;
               }
//نوع كشتي
               else if(index.Key.DictionaryName==""ShipType"")
               {
                   int no = Convert.ToInt32(job.CustomFields[""ShipNo""]);
                   for (int i = 1; i < no+1; i++)
                  {
                     decimal type = Convert.ToDecimal(job.CustomFields[""ShipType""+i]);
                     if(type==0) type=8;
                     else if(type==1) type=7;
                     else type = 9;
                     x += type;
                  }
                  x=x/no/9;
               }
//تعداد كشتي
               else if(index.Key.DictionaryName==""ShipNo"")
               {
                   int no = Convert.ToInt32(job.CustomFields[""ShipNo""]);
                  x=no/6;
               }
//کیفیت پرسنل كشتي
               else if(index.Key.DictionaryName==""ShipStaffQuality"")
               {
                  x += (110-Convert.ToDecimal(job.CustomFields[""CommandersInquiryScore""]))/100*0.328m;
                  x += (110-Convert.ToDecimal(job.CustomFields[""FirstOfficersInquiryScore""]))/100*0.138m;
                  x += (110-Convert.ToDecimal(job.CustomFields[""ChiefEngineersInquiryScore""]))/100*0.277m;
                  x += (110-Convert.ToDecimal(job.CustomFields[""SecondOfficersInquiryScore""]))/100*0.128m;
                  x += (110-Convert.ToDecimal(job.CustomFields[""ElectronicEngineersInquiryScore""]))/100*0.129m;
               }
//کارکرد عملیاتی کشتی ( روز )
               else if(index.Key.DictionaryName==""ShipOperationEfficiency"")
               {
                  x = Convert.ToDecimal(job.CustomFields[""ShipOperationalEfficiency""])/(Convert.ToInt32(job.CustomFields[""ShipNo""])*30*3);
               }
//مسیر حرکت کشتی
               else if(index.Key.DictionaryName==""ShipPath"")
               {
                   int no = Convert.ToInt32(job.CustomFields[""ShipNo""]);
                   for (int i = 1; i < no+1; i++)
                  {
                     decimal type = Convert.ToDecimal(job.CustomFields[""ShipPath""+i]);
                     if(type==1) type=9;
                     else if(type==2) type=9;
                     else if(type==3) type=7;
                     else if(type==4) type=5;
                     else if(type==5) type=5;
                     else if(type==6) type=9;
                     else if(type==7) type=5;
                     else if(type==8) type=8;
                     else if(type==9) type=9;
                     x += type;
                  }
                  x=x/no/9;
               }
               Utils.AddPoint(job, index, ""gross"", x);
           }

            if (Utils.IndexField(index, ""Importance"") == ""9"")
            {
                sum3 += Utils.AddPoint(job, index, ""net"", a3 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""7"")
            {
                sum3 += Utils.AddPoint(job, index, ""net"", b3 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""5"")
            {
                sum3 += Utils.AddPoint(job, index, ""net"", c3 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""3"")
            {
                sum3 += Utils.AddPoint(job, index, ""net"", d3 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
            if (Utils.IndexField(index, ""Importance"") == ""1"")
            {
                sum3 += Utils.AddPoint(job, index, ""net"", e3 * Utils.GetPoint(job, index, ""gross"").Value).Value;
            }
        }
    }
    Utils.AddPoint(job, ""final-general"", sum);
    Utils.AddPoint(job, ""initial-technical"", sum2);
    Utils.AddPoint(job, ""final-equalizer"", sum3);
    sum2 = sum2 * (1 - z / 2 + sum3);
    Utils.AddPoint(job, ""final-technical"", sum2);
        sum = Math.Min(sum + sum2 + sum3, 100);
        Utils.AddPoint(job, ""final-job"", sum);
        total += sum;
        it++;
}
if (it > 0)
    Utils.AddPoint(""final"", total / it, true);

", RuleType.PerCalculation,1);
                ruleRep.Add(rule);

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

                for (int i = 0; i < 10; i++)
                {
                    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                        "فبلد دلخواه کارمند" + i, "EmployeeCft" + i, 0, 100, EntityTypeEnum.Employee, "string");
                    cftRep.Add(cft);
                    employeeCftList.Add(cft);
                }

                #endregion

                #region JobIndex CustomFields Creation

                for (int i = 0; i < 9; i++)
                {
                    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                        "فبلد دلخواه شاخص شغل" + i, "JobIndexCft" + i, 0, 100, EntityTypeEnum.JobIndex, "string");
                    cftRep.Add(cft);
                    jobIndexCftList.Add(cft);
                }
                var imp = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                    "اهمیت", "Importance", 0, 100, EntityTypeEnum.JobIndex, "string");
                cftRep.Add(imp);
                jobIndexCftList.Add(imp);

                #endregion

                #region Job CustomFields Creation

                for (int i = 0; i < 10; i++)
                {
                    var cft = new PMSAdmin.Domain.Model.CustomFieldTypes.CustomFieldType(cftRep.GetNextId(),
                        "فبلد دلخواه شغل" + i, "JobCft" + i, 0, 100, EntityTypeEnum.Job, "string");
                    cftRep.Add(cft);
                    jobCftList.Add(cft);
                }

                #endregion

                var jobRep = new PMSAdmin.Persistence.NH.JobRepository(uow);

                #region Jobs Creation

                for (int i = 0; i < 5; i++)
                {
                    var job = new PMSAdmin.Domain.Model.Jobs.Job(jobRep.GetNextId(),
                        " شغل" + i, "Job" + i);
                    job.AssignCustomFields(jobCftList.Skip(i * 2).Take(2).ToList());
                    jobRep.AddJob(job);
                    jobList.Add(job);
                }

                #endregion

                var jobPositionRep = new PMSAdmin.Persistence.NH.JobPositionRepository(uow);

                #region JobPositions Creation

                for (int i = 0; i < 5; i++)
                {
                    var jobPosition = new PMSAdmin.Domain.Model.JobPositions.JobPosition(jobPositionRep.GetNextId(),
                        " پست" + i, "JobPosition" + i);
                    jobPositionRep.Add(jobPosition);
                    jobPositionList.Add(jobPosition);
                }

                #endregion

                var unitRep = new PMSAdmin.Persistence.NH.UnitRepository(uow);

                #region Unit Creation

                for (int i = 0; i < 5; i++)
                {
                    var unit = new PMSAdmin.Domain.Model.Units.Unit(unitRep.GetNextId(),
                        " واحد" + i, "Unit" + i);
                    unitRep.Add(unit);
                    unitList.Add(unit);
                }

                #endregion


                var jobIndexRep = new PMSAdmin.Persistence.NH.JobIndexRepository(uow);

                #region JobIndexes Creation

                var jobIndexCategory = new PMSAdmin.Domain.Model.JobIndices.JobIndexCategory(jobIndexRep.GetNextId(), null, "دسته شاخص", "JobIndexCategory");
                jobIndexRep.Add(jobIndexCategory);

                for (int i = 0; i < 5; i++)
                {
                    var jobIndex = new PMSAdmin.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), jobIndexCategory,
                        " شاخص شغل" + i, "JobIndex" + i);
                    var jobIndexCustomFields = jobIndexCftList.Skip(i * 2).Take(2).ToList();
                    jobIndexCustomFields.Add(jobIndexCftList.Single(j => j.DictionaryName == "Importance"));
                    jobIndex.AssignCustomFields(jobIndexCustomFields);
                    jobIndexRep.Add(jobIndex);
                    jobIndexList.Add(jobIndex);
                }

                #endregion

                var policyRep = new PMSAdmin.Persistence.NH.PolicyRepository(uow);
                
                #region Policy Creation
                policy = new PMSAdmin.Domain.Model.Policies.RuleEngineBasedPolicy(policyRep.GetNextId(),
                        " خظ کش پرتر", "PorterRulerPolicy");
                policyRep.Add(policy);

                policy.AssignRule(rule);
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
                var periodManagerService = new PeriodManagerService(periodRep,null,null,null,null,null,null,null,null,null);
                #region Period creation

                period = new Period(new PeriodId(periodRep.GetNextId()), Guid.NewGuid().ToString(), DateTime.Now, DateTime.Now);
                period.ChangeActiveStatus(periodManagerService,true);
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

                var countjil = jobIndexList.Count();
                var index = 0;
                foreach (var itm in jobIndexList.Take(countjil / 2))
                {

                    var sharedJobIndex =
                        new PMS.Domain.Model.JobIndices.SharedJobIndex(
                            new PMS.Domain.Model.JobIndices.SharedJobIndexId(itm.Id.Id), itm.Name,
                            itm.DictionaryName);
                    var jobIndex = new PMS.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), period,
                        sharedJobIndex, jobIndexGroupGenaral, index % 2 == 0);

                    var dicSharedCutomField = jobIndexCftList
                        .Where(j => itm.CustomFieldTypeIdList.Select(i => i.Id).Contains(j.Id.Id)).Select(p =>
                            new PMS.Domain.Model.JobIndices.SharedJobIndexCustomField(
                                new PMS.Domain.Model.JobIndices.SharedJobIndexCustomFieldId(p.Id.Id), p.Name,
                                p.DictionaryName,
                                p.MinValue, p.MaxValue)).ToDictionary(s => s, s => s.DictionaryName == "Importance" ? (((index + 1) * 2)-1).ToString() : string.Empty);

                    jobIndex.UpdateCustomFields(dicSharedCutomField);
                    jobIndexInPeriodList.Add(jobIndex);
                    jobIndexRep.Add(jobIndex);
                    index++;

                }
                index = 0;
                foreach (var itm in jobIndexList.Skip(countjil / 2))
                {
                    var sharedJobIndex =
                        new PMS.Domain.Model.JobIndices.SharedJobIndex(
                            new PMS.Domain.Model.JobIndices.SharedJobIndexId(itm.Id.Id), itm.Name,
                            itm.DictionaryName);
                    var jobIndex = new PMS.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), period,
                        sharedJobIndex, jobIndexGroupTechnical, index % 2 == 0);
                    var dicSharedCutomField = jobIndexCftList
                        .Where(j => itm.CustomFieldTypeIdList.Select(i => i.Id).Contains(j.Id.Id)).Select(p =>
                            new PMS.Domain.Model.JobIndices.SharedJobIndexCustomField(
                                new PMS.Domain.Model.JobIndices.SharedJobIndexCustomFieldId(p.Id.Id), p.Name,
                                p.DictionaryName,
                                p.MinValue, p.MaxValue)).ToDictionary(s => s, s => s.DictionaryName == "Importance" ? (((index + 1) * 2)-1).ToString() : string.Empty);

                    jobIndex.UpdateCustomFields(dicSharedCutomField);

                    jobIndexInPeriodList.Add(jobIndex);
                    jobIndexRep.Add(jobIndex);
                    index++;

                }
                #endregion

                var jobRep = new PMS.Persistence.NH.JobRepository(uow);

                #region Job creation

                foreach (var pmsAdminJob in jobList)
                {
                    var jobJobIndices = jobIndexInPeriodList.Select(jobIndex => new JobJobIndex(jobIndex.Id, true, true, true)).ToList();
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
                PMS.Domain.Model.JobPositions.JobPosition jobPositionParent = null;
                foreach (var pmsAdminJobPosition in jobPositionList)
                {
                    var jobPosition = new PMS.Domain.Model.JobPositions.JobPosition(period,
                        new Domain.Model.JobPositions.SharedJobPosition(new Domain.Model.JobPositions.SharedJobPositionId(pmsAdminJobPosition.Id.Id), pmsAdminJobPosition.Name, pmsAdminJobPosition.DictionaryName)
                        , jobPositionParent,
                        jobInPeriodList[jpIndex],
                        unitInPeriodList[jpIndex]
                        );

                    if (jpIndex != 1 && jpIndex != 2)
                        jobPositionParent = jobPosition;

                    jobPositionInPeriodList.Add(jobPosition);
                    jobPositionRep.Add(jobPosition);
                    jpIndex++;
                }
                #endregion

                var employeeRep = new PMS.Persistence.NH.EmployeeRepository(uow);

                #region Employee Creation

                for (var i = 0; i < 10; i++)
                {
                    var employeeCustomFields =
                       employeeCftList.ToList()
                           .ToDictionary(
                               e =>
                                   new Domain.Model.Employees.SharedEmployeeCustomField(
                                       new Domain.Model.Employees.SharedEmployeeCustomFieldId(e.Id.Id), e.Name,
                                       e.DictionaryName, e.MinValue, e.MaxValue), e => e.Id.Id.ToString());
                    var employee =
                        new PMS.Domain.Model.Employees.Employee(
                            ((i + 1) * 2000).ToString(), period, "کارمند" + i,
                            "کارمندیان" + i, employeeCustomFields);
                   



                    var jobPositionInPeriod = jobPositionInPeriodList.Skip(i / 2).Take(1).Single();

                    var jobcustomFields = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod.JobId)).CustomFields;
                    if (jobcustomFields != null && jobcustomFields.Count != 0)
                    {
                        var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee, jobPositionInPeriod, period.StartDate, period.EndDate,100,1,
                        jobcustomFields.Select(j => new EmployeeJobCustomFieldValue(j.Id, "10")).ToList()
                        );
                        employee.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                    }

                    empList.Add(employee);
                    employeeRep.Add(employee);

                }
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
                            var id = inquiryRep.GetNextId();
                            var inquiryIndexPoint = new Domain.Model.InquiryJobIndexPoints.InquiryJobIndexPoint(
                                new Domain.Model.InquiryJobIndexPoints.InquiryJobIndexPointId(id),
                                itm, jobIndex as Domain.Model.JobIndices.JobIndex, "5");
                            inquiryRep.Add(inquiryIndexPoint);
                            }

                        }
                    }
                }
                uow.Commit();
            }

            using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            {
                EventPublisher publisher = new EventPublisher();
                var rebps = new RuleBasedPolicyEngineService(new LocatorProvider("PMSDb"), publisher);
                var policyRep = new MITD.PMS.Persistence.NH.PolicyRepository(uow,
                    new PolicyConfigurator(rebps));
                var pmsPolicy = policyRep.GetById(new PolicyId(policy.Id.Id));
        
                var calcRep = new CalculationRepository(uow);
                var calc = new Calculation(calcRep.GetNextId(), period, pmsPolicy,
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
