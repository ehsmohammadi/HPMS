
//using System;
//using System.Collections.Generic;
//using MITD.Core;
//using MITD.Core.RuleEngine;
//using MITD.PMS.RuleContracts;
//using System.Linq;
//using System.Globalization;

//namespace MITD.Core.RuleEngine
//{

//    public static class Utils
//    {
//        public static RuleResult Res = new RuleResult();


//        public static int IndexCount(JobPosition job, string indexCFName, string indexCFValue, string group)
//        {
//            return job.Indices.Count(j => j.Key.CustomFields.Any(k => k.Key == indexCFName && k.Value == indexCFValue) && j.Key.Group.DictionaryName == group);
//        }

//        public static string IndexGroup(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index)
//        {
//            return index.Key.Group.DictionaryName;
//        }

//        public static string IndexField(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string fieldName)
//        {
//            try
//            {
//                return index.Key.CustomFields[fieldName];
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, " شاخص " + fieldName);
//            }
//        }

//        public static RulePoint AddEmployeePoint(JobPosition job, KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index,
//            string name, decimal value, bool final = false)
//        {
//            var res = new RulePoint { Name = name, Value = value, Final = final };
//            if (!Utils.Res.JobResults.Any(j => j.Key == job.DictionaryName))
//                Utils.Res.JobResults.Add(job.DictionaryName, new JobPositionResult());
//            if (!Utils.Res.JobResults[job.DictionaryName].IndexResults.Any(j => j.Key == index.Key.DictionaryName))
//                Utils.Res.JobResults[job.DictionaryName].IndexResults.Add(index.Key.DictionaryName, new List<RulePoint>());
//            Utils.Res.JobResults[job.DictionaryName].IndexResults[index.Key.DictionaryName].Add(res);
//            return res;
//        }

//        public static RulePoint AddEmployeePoint(JobPosition job, string name, decimal value, bool final = false)
//        {
//            var res = new RulePoint { Name = name, Value = value, Final = final };
//            if (!Utils.Res.JobResults.Any(j => j.Key == job.DictionaryName))
//                Utils.Res.JobResults.Add(job.DictionaryName, new JobPositionResult());
//            Utils.Res.JobResults[job.DictionaryName].Results.Add(res);
//            return res;
//        }

//        public static RulePoint AddEmployeePoint(string name, decimal value, bool final = false)
//        {

//            var res = new RulePoint { Name = name, Value = value, Final = final };
//            Utils.Res.Results.Add(res);
//            return res;
//        }


//        public static RulePoint AddCalculationPoint(string name, decimal value, bool final = false)
//        {
//            try
//            {
//                var res = new RulePoint { Name = name, Value = value, Final = final };
//                Utils.Res.CalculationPoints.Add(res);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, "اضافه کردن  مقدار محاسبه با عنوان " + name);
//            }
//        }


//        public static RulePoint GetCalculationPoint(CalculationData data, string name)
//        {
//            try
//            {
//                return data.Points.CalculationPoints.SingleOrDefault(j => j.Name == name);
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, name);
//            }
//        }

//        private static Exception exceptionConvertor(Exception ex, string keyName)
//        {
//            var strkeyNotFound = string.Format("خطا در دریافت اطلاعات مقدار محاسبه. مقداری با کلید {0} یافت نشد ", keyName) + "\r\n";
//            var strkeyOutOfRange = string.Format("خطا در دریافت اطلاعات مقدار محاسبه.مقدار {0} در رنج قابل قبول نیست ", keyName) + "\r\n";
//            var strOtherExp = string.Format("خطا در دریافت اطلاعات مقدار محاسبه با عنوان " + keyName) + "\r\n";

//            if (ex is KeyNotFoundException)
//                return new KeyNotFoundException(strkeyNotFound + ex.Message);
//            if (ex is IndexOutOfRangeException)
//                return new IndexOutOfRangeException(strkeyOutOfRange + ex.Message);

//            return new Exception(strOtherExp + ex.Message);
//        }


//        public static RulePoint GetPoint(JobPosition job, string name)
//        {
//            try
//            {
//                if (!Utils.Res.JobResults.Any(j => j.Key == job.DictionaryName)) return null;
//                return Utils.Res.JobResults[job.DictionaryName].Results.SingleOrDefault(j => j.Name == name);
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, name);
//            }
//        }

//        public static RulePoint GetPoint(CalculationData data, JobPosition job, string name)
//        {
//            try
//            {
//                if (!data.Points.JobResults.Any(j => j.Key == job.DictionaryName)) return null;
//                return Utils.Res.JobResults[job.DictionaryName].Results.SingleOrDefault(j => j.Name == name);
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, name);
//            }
//        }

//        public static RulePoint GetPoint(JobPosition job, KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string name)
//        {
//            try
//            {
//                RulePoint res = null;
//                if (Utils.Res.JobResults.ContainsKey(job.DictionaryName))
//                    if (Utils.Res.JobResults[job.DictionaryName].IndexResults.ContainsKey(index.Key.DictionaryName))
//                        res =
//                            Utils.Res.JobResults[job.DictionaryName].IndexResults[index.Key.DictionaryName]
//                                .SingleOrDefault(j => j.Name == name);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, name);
//            }
//        }

//        public static RulePoint GetPoint(CalculationData data, JobPosition job,
//                                         KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string name)
//        {
//            try
//            {
//                RulePoint res = null;
//                if (data.Points.JobResults.ContainsKey(job.DictionaryName))
//                    if (data.Points.JobResults[job.DictionaryName].IndexResults.ContainsKey(index.Key.DictionaryName))
//                        res =
//                            data.Points.JobResults[job.DictionaryName].IndexResults[index.Key.DictionaryName]
//                                .SingleOrDefault(j => j.Name == name);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, name);
//            }
//        }

//        public static RulePoint GetPoint(JobPosition job, string indexName, string name)
//        {
//            try
//            {
//                RulePoint res = null;
//                if (Utils.Res.JobResults.ContainsKey(job.DictionaryName))
//                    if (Utils.Res.JobResults[job.DictionaryName].IndexResults.ContainsKey(indexName))
//                        res = Utils.Res.JobResults[job.DictionaryName].IndexResults[indexName].SingleOrDefault(j => j.Name == name);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, name);
//            }
//        }

//        public static RulePoint GetPoint(CalculationData data, JobPosition job, string indexName, string name)
//        {
//            try
//            {
//                RulePoint res = null;
//                if (data.Points.JobResults.ContainsKey(job.DictionaryName))
//                    if (data.Points.JobResults[job.DictionaryName].IndexResults.ContainsKey(indexName))
//                        res = data.Points.JobResults[job.DictionaryName].IndexResults[indexName].SingleOrDefault(j => j.Name == name);
//                return res;
//            }
//            catch (Exception ex)
//            {
//                throw exceptionConvertor(ex, name);
//            }
//        }

//        public static decimal GetInquiryByJobPositionName(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> jobIndex, string inquirerJobPositionName)
//        {
//            return Convert.ToDecimal(jobIndex.Value.SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.DictionaryName == inquirerJobPositionName).Value);
//        }



//        public static decimal GetInquiryByEmployeeNoAndJobPositionName(KeyValuePair<JobIndex, Dictionary<Employee, List<Inquiry>>> index, string inquirerEmployeeNo, string JobPositionName)
//        {
//            return Convert.ToDecimal(index.Value.Where(e=>e.Key.EmployeeNo==inquirerEmployeeNo ).SelectMany(j => j.Value).SingleOrDefault(x => x.JobPosition.DictionaryName == JobPositionName).Value);
//        }
//    }

//    public class RuleResultHelper : IRuleResult<RuleResult>
//    {
//        public RuleResult GetResult()
//        {
//            return Utils.Res;
//        }
//        public void Clear()
//        {
//            Utils.Res = new RuleResult();
//        }
//    }


//    public class Rule10 : IRule<CalculationData>
//    {
//        public void Execute(CalculationData data)
//        {

//            decimal z = .4m;
//            Utils.AddCalculationPoint("z", z);
//            foreach (var job in data.JobPositions)
//            {
//                if (Utils.GetPoint(job, "a1") != null) break;
//                decimal notPresentIndexesCount = 1;
//                if (job.CustomFields["PeriodicMaintenance"] == "0")
//                    notPresentIndexesCount = 2;

//                decimal a1 = Utils.IndexCount(job, "Importance", "1", "General");
//                decimal b1 = Utils.IndexCount(job, "Importance", "3", "General");
//                decimal c1 = Utils.IndexCount(job, "Importance", "5", "General");
//                decimal d1 = Utils.IndexCount(job, "Importance", "7", "General");
//                decimal e1 = Utils.IndexCount(job, "Importance", "9", "General");

//                //محاسبه عدد وزنی شاخص های عمومی
//                decimal y1 = 0;
//                decimal n = (a1 + 3 * b1 + 5 * c1 + 7 * d1 + 9 * e1);
//                if (n != 0)
//                    y1 = 20 / n;

//                Utils.AddEmployeePoint(job, "a1", y1);
//                Utils.AddEmployeePoint(job, "b1", 3 * y1);
//                Utils.AddEmployeePoint(job, "c1", 5 * y1);
//                Utils.AddEmployeePoint(job, "d1", 7 * y1);
//                Utils.AddEmployeePoint(job, "e1", 9 * y1);

//                decimal a2 = Utils.IndexCount(job, "Importance", "1", "Technical");
//                decimal b2 = Utils.IndexCount(job, "Importance", "3", "Technical");
//                decimal c2 = Utils.IndexCount(job, "Importance", "5", "Technical");
//                decimal d2 = Utils.IndexCount(job, "Importance", "7", "Technical") - notPresentIndexesCount;
//                decimal e2 = Utils.IndexCount(job, "Importance", "9", "Technical") - notPresentIndexesCount;

//                //محاسبه عدد وزنی شاخص های تخصصی
//                decimal y2 = 0;
//                decimal m = (a2 + 3 * b2 + 5 * c2 + 7 * d2 + 9 * e2);
//                if (m != 0)
//                    y2 = 80 / m;

//                Utils.AddEmployeePoint(job, "a2", y2);
//                Utils.AddEmployeePoint(job, "b2", 3 * y2);
//                Utils.AddEmployeePoint(job, "c2", 5 * y2);
//                Utils.AddEmployeePoint(job, "d2", 7 * y2);
//                Utils.AddEmployeePoint(job, "e2", 9 * y2);

//                decimal a3 = Utils.IndexCount(job, "Importance", "1", "Equalizer");
//                decimal b3 = Utils.IndexCount(job, "Importance", "3", "Equalizer");
//                decimal c3 = Utils.IndexCount(job, "Importance", "5", "Equalizer");
//                decimal d3 = Utils.IndexCount(job, "Importance", "7", "Equalizer");
//                decimal e3 = Utils.IndexCount(job, "Importance", "9", "Equalizer");

//                //محاسبه عدد وزنی شاخص های یکسان ساز
//                decimal y3 = 0;
//                decimal o = (a3 + 3 * b3 + 5 * c3 + 7 * d3 + 9 * e3);
//                if (o != 0)
//                    y3 = z / o;

//                Utils.AddEmployeePoint(job, "a3", y3);
//                Utils.AddEmployeePoint(job, "b3", 3 * y3);
//                Utils.AddEmployeePoint(job, "c3", 5 * y3);
//                Utils.AddEmployeePoint(job, "d3", 7 * y3);
//                Utils.AddEmployeePoint(job, "e3", 9 * y3);

//            }

//        }
//    }
//    public class Rule11 : IRule<CalculationData>
//    {
//        public void Execute(CalculationData data)
//        {

//            if (data.PathNo != 1) return;
//            var jobs = data.JobPositions.Where(j => j.Job.DictionaryName == "TechnicalInspector");
//            if (jobs == null || jobs.Count() == 0) return;

//            decimal calcPointMinCostControl;
//            var rulePointCalcPointMinCostControl = Utils.GetCalculationPoint(data, "MinPereiodicMaintenancePlaningTime");
//            calcPointMinCostControl = rulePointCalcPointMinCostControl != null ? rulePointCalcPointMinCostControl.Value : decimal.MaxValue;

//            decimal calcPointMinTechnicalOffHireTime;
//            var rulePointCalcPointMinTechnicalOffHireTime = Utils.GetCalculationPoint(data, "MinTechnicalOffHireTime");
//            calcPointMinTechnicalOffHireTime = rulePointCalcPointMinTechnicalOffHireTime != null ? rulePointCalcPointMinTechnicalOffHireTime.Value : decimal.MaxValue;


//            foreach (var job in jobs)
//            {


//                //محاسبه تعداد شاخص ها با اهمیت های مختلف
//                foreach (var index in job.Indices)
//                {
//                    decimal x = 0;
//                    //فرهنگ و تعهد سازمانی
//                    if (index.Key.DictionaryName == "OrganizationalCultureAndCommitment")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//                        var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//                        var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
                        
//                        if (inquiryPoint > 70) inquiryPoint = Math.Min(inquiryPoint + inquiryPoint * Convert.ToDecimal("0.05"), 100);
//                        else if (inquiryPoint < 60) inquiryPoint = Math.Max(inquiryPoint - inquiryPoint * Convert.ToDecimal("0.05"), 0);
                        
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "OrganizationalGrowthAndExcelency")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//                        var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//                        var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "Discipline")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//                        var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//                        var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "AdherenceToOrganizarionValues")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//                        var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//                        var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "HardWorking")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//                        var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//                        var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "EnglishLanquageSkills")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//                        var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//                        var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "ICDLFamiliarity")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//                        var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//                        var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "MentalAndPhisicalAbility")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//                        var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//                        var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
//                        x = inquiryPoint / 9;
//                    }


//                    // Technical ------------------------------------------------------------------------
//                    //قابلیت برنامه ریزی و کنترل تعمیرات سفری
//                    else if (index.Key.DictionaryName == "VoyageMaintenancePlannigAbility")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo,data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 ) / 6;
//                        x = inquiryPoint / 9;
//                    }
//                    //برنامه ریزی و کنترل تعمیرات ادواری (کیفیت
//                    else if (index.Key.DictionaryName == "PereiodicMaintenancePlaningQuality")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2) / 6;
//                        x = inquiryPoint / 9;
//                    }
//                    //پروسه انتخاب پیمانکاران
//                    else if (index.Key.DictionaryName == "ContractorSelectionProcess")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2) / 6;
//                        x = inquiryPoint / 9;
//                    }
//                    //آنالير مشكلات فني ، پیگیری نواقص و ارائه راهکار
//                    else if (index.Key.DictionaryName == "ProblemSolvingAbility")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2) / 6;
//                        x = inquiryPoint / 9;
//                    }
//                    //به روزنگه داشتن گواهینامه ها و رعایت الزامات قانونی
//                    else if (index.Key.DictionaryName == "CertificateUpdates")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2) / 6;
//                        x = inquiryPoint / 9;
//                    }
//                    //آشنایی با نرم افزارهای مربوط به شغل
//                    else if (index.Key.DictionaryName == "JobSoftwareFamiliarity")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2) / 6;
//                        x = inquiryPoint / 9;
//                    }
//                    //کنترل هزینه
//                    else if (index.Key.DictionaryName == "CostControl")
//                    {
//                        //----------------------------------------------------------------------------------------------------
//                        continue;
//                        //x = Math.Abs(Convert.ToDecimal(job.CustomFields["DeclaredAnnualBudget"]) - Convert.ToDecimal(job.CustomFields["TotalAnnualCost"]) + 1) / Convert.ToDecimal(job.CustomFields["DeclaredAnnualBudget"]);
//                        //var calcPointMinCostControl = Utils.GetCalculationPoint(data, "MinCostControl");
//                        //if (calcPointMinCostControl == null)
//                        //    Utils.AddCalculationPoint("MinCostControl", x);
//                        //else if (x < calcPointMinCostControl.Value)
//                        //    Utils.AddCalculationPoint("MinCostControl", x);
//                    }
//                    else if (index.Key.DictionaryName == "BudgetQuality")
//                    {
//                        continue;
//                        //var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        //x = inquiryPoint / 9;
//                    }
//                    //برنامه ریزی و کنترل تعمیرات ادواری (زمان
//                    else if (index.Key.DictionaryName == "PereiodicMaintenancePlaningTime")
//                    {
//                        if (job.CustomFields["PeriodicMaintenance"] == "0")
//                            continue;
//                        else
//                        {
//                            x = Math.Abs(Convert.ToDecimal(job.CustomFields["EstimatedPereiodicMaintenanceTime"]) - Convert.ToDecimal(job.CustomFields["ActualPereiodicMaintenanceTime"]) + 1) / Convert.ToDecimal(job.CustomFields["EstimatedPereiodicMaintenanceTime"]);
//                            if (x < calcPointMinCostControl)
//                               calcPointMinCostControl= x;
//                            //if (calcPointMinCostControl.HasValue)
//                            //    Utils.AddCalculationPoint("MinPereiodicMaintenancePlaningTime", x);
//                            //else if (x < calcPointMinCostControl.Value)
//                            //    
//                        }
//                    }
//                    //مدت زمان Off HIRE فني (ساعت)
//                    else if (index.Key.DictionaryName == "TechnicalOffHireTime")
//                    {
//                        x = (Convert.ToDecimal(job.CustomFields["TechnicalOffHireTime"]) / Convert.ToDecimal(job.CustomFields["ShipOperationalEfficiency"])) + 1;
                       
//                        //if (calcPointMinTechnicalOffHireTime == null)
//                        //    Utils.AddCalculationPoint("MinTechnicalOffHireTime" , x);
//                        //else 
//                        if (x < calcPointMinTechnicalOffHireTime)
//                            calcPointMinTechnicalOffHireTime = x;
//                           // Utils.AddCalculationPoint("MinTechnicalOffHireTime" , x);

//                        if (x < 50) x = Math.Max(x - x * Convert.ToDecimal("0.05"), 0);
                    
//                    }
//                    //مجموع تعداد نواقص اعلام شده توسط FSC و PSC برای شناورها
//                    else if (index.Key.DictionaryName == "VesselFaultSumNo")
//                    {
//                        decimal y = Convert.ToDecimal(job.CustomFields["PSCFSCvesselInspectionNO"]);
//                        x = 2 / ((Convert.ToDecimal(job.CustomFields["VesselFaultSumNo"]) / (y > 0 ? y : 1)) + 2);
//                    }
//                    //بهره وري دوره های تخصصی ( دوره مهندس ناظری)
//                    else if (index.Key.DictionaryName == "TechnicalCourseAttending")
//                    {
//                        x = Convert.ToDecimal(job.CustomFields["TechnicalCourseAttending"]) / 100;
//                    }
//                    else if (index.Key.DictionaryName == "PereiodicMaintenancePlaningQuality")
//                    {
//                        if (job.CustomFields["PeriodicMaintenance"] == "0")
//                            continue;
//                        else
//                        {
//                            var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                            x = inquiryPoint / 9;
//                        }
//                    }
//                    else if (index.Key.DictionaryName == "VoyageMaintenancePlannigAbility")
//                    {
//                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "ContractorSelectionProcess")
//                    {
//                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "ProblemSolvingAbility")
//                    {
//                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "CertificateUpdates")
//                    {
//                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        x = inquiryPoint / 9;
//                    }
//                    else if (index.Key.DictionaryName == "JobSoftwareFamiliarity")
//                    {
//                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        x = inquiryPoint / 9;
//                    }



//                    // Equalizer -------------------------------------------------------------------------------------
//                    //مکان و محل تعمیرات
//                    else if (index.Key.DictionaryName == "MaintenaceLocation")
//                    {
//                        var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//                        var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo, data.JobPositions[0].Name);
//                        var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2) / 6;
//                        x = (10 - inquiryPoint) / 9;
//                    }
//                    //سن كشتي
//                    else if (index.Key.DictionaryName == "ShipAge")
//                    {
//                        decimal no = Convert.ToDecimal(job.CustomFields["ShipNo"]);
//                        for (int i = 1; i < no + 1; i++)
//                        {
//                            decimal age = Convert.ToDecimal(job.CustomFields["ShipAge" + i]);
//                            if (age >= 0 && age < 5) age = 1;
//                            else if (age >= 5 && age < 10) age = 3;
//                            else if (age >= 10 && age < 15) age = 5;
//                            else if (age >= 15 && age < 20) age = 7;
//                            else if (age >= 20) age = 9;
//                            else continue;
//                            x += age;
//                        }
//                        x = x / no / 9;
//                    }
//                    //نوع كشتي
//                    else if (index.Key.DictionaryName == "ShipType")
//                    {
//                        decimal no = Convert.ToDecimal(job.CustomFields["ShipNo"]);
//                        for (int i = 1; i < no + 1; i++)
//                        {
//                            decimal type = Convert.ToDecimal(job.CustomFields["ShipType" + i]);
//                            if (type == 0) type = 8;
//                            else if (type == 1) type = 7;
//                            else type = 9;
//                            x += type;
//                        }
//                        x = x / no / 9;
//                    }
//                    //تعداد كشتي
//                    else if (index.Key.DictionaryName == "ShipNo")
//                    {
//                        decimal no = Convert.ToDecimal(job.CustomFields["ShipNo"]);
//                        x = no / 6;
//                    }
//                    //کیفیت پرسنل كشتي
//                    else if (index.Key.DictionaryName == "ShipStaffQuality")
//                    {
//                        x += (110 - Convert.ToDecimal(job.CustomFields["CommandersInquiryScore"])) / 100 * 0.328m;
//                        x += (110 - Convert.ToDecimal(job.CustomFields["FirstOfficersInquiryScore"])) / 100 * 0.138m;
//                        x += (110 - Convert.ToDecimal(job.CustomFields["ChiefEngineersInquiryScore"])) / 100 * 0.277m;
//                        x += (110 - Convert.ToDecimal(job.CustomFields["SecondOfficersInquiryScore"])) / 100 * 0.128m;
//                        x += (110 - Convert.ToDecimal(job.CustomFields["ElectronicEngineersInquiryScore"])) / 100 * 0.129m;
//                    }
//                    //کارکرد عملیاتی کشتی ( روز )
//                    else if (index.Key.DictionaryName == "ShipOperationalEfficiency")
//                    {
//                        x = Convert.ToDecimal(job.CustomFields["ShipOperationalEfficiency"]) / (Convert.ToInt32(job.CustomFields["ShipNo"]) * 30 * 3);
//                    }
//                    //مسیر حرکت کشتی
//                    else if (index.Key.DictionaryName == "ShipPath")
//                    {
//                        decimal no = Convert.ToDecimal(job.CustomFields["ShipNo"]);
//                        for (int i = 1; i < no + 1; i++)
//                        {
//                            decimal type = Convert.ToDecimal(job.CustomFields["ShipPath" + i]);
//                            if (type == 1) type = 9;
//                            else if (type == 2) type = 9;
//                            else if (type == 3) type = 7;
//                            else if (type == 4) type = 5;
//                            else if (type == 5) type = 5;
//                            else if (type == 6) type = 9;
//                            else if (type == 7) type = 5;
//                            else if (type == 8) type = 8;
//                            else if (type == 9) type = 9;
//                            x += type;
//                        }
//                        x = x / no / 9;
//                    }
//                    else continue;

//                    Utils.AddEmployeePoint(job, index, "gross", x);
//                }

//            }
//            Utils.AddCalculationPoint("MinPereiodicMaintenancePlaningTime", calcPointMinCostControl);
//            Utils.AddCalculationPoint("MinTechnicalOffHireTime", calcPointMinTechnicalOffHireTime);


//        }
//    }
//    public class Rule12 : IRule<CalculationData>
//    {
//        public void Execute(CalculationData data)
//        {

//            if (data.PathNo != 2) return;
//            var jobs = data.JobPositions.Where(j => j.Job.DictionaryName == "TechnicalInspector");
//            if (jobs == null || jobs.Count() == 0) return;
//            foreach (var job in jobs)
//            {

//                //محاسبه تعداد شاخص ها با اهمیت های مختلف
//                decimal offHire = 0;
//                foreach (var index in job.Indices)
//                {
//                    decimal x = 0;

//                    // Technical ------------------------------------------------------------------------
//                    x = 0;
//                    //کنترل هزینه
//                    if (index.Key.DictionaryName == "CostControl")
//                    {
//                        continue;
//                        //x = Utils.GetPoint(data, job, index, "gross").Value;
//                        //var calcPointMinCostControl = Utils.GetCalculationPoint(data, "MinCostControl");
//                        //x = Math.Min(0.05m, calcPointMinCostControl.Value) / x;
//                    }
//                    //برنامه ریزی و کنترل تعمیرات ادواری (زمان
//                    else if (index.Key.DictionaryName == "PereiodicMaintenancePlaningTime")
//                    {
//                        if (job.CustomFields["PeriodicMaintenance"] == "0")
//                            continue;
//                        else
//                        {
//                            x = Utils.GetPoint(data, job, index, "gross").Value;
//                            var MinPereiodicMaintenancePlaningTime = Utils.GetCalculationPoint(data,
//                                "MinPereiodicMaintenancePlaningTime" );
//                            x = Math.Min(0.1m, MinPereiodicMaintenancePlaningTime.Value) / x;
//                        }
//                    }
//                    //مدت زمان Off HIRE فني (ساعت)
//                    else if (index.Key.DictionaryName == "TechnicalOffHireTime")
//                    {
//                        x = Utils.GetPoint(data, job, index, "gross").Value;
//                        var MinTechnicalOffHireTime = Utils.GetCalculationPoint(data, "MinTechnicalOffHireTime");
//                        x = Math.Min(1, MinTechnicalOffHireTime.Value) / x;
//                        offHire = x;
//                    }

//                    else if (index.Key.DictionaryName == "ContractorEfficiencyControl")
//                    {
//                        var VoyageMaintenancePlannigAbility =
//                            Utils.GetPoint(data, job, "VoyageMaintenancePlannigAbility", "gross").Value;
//                        var TechnicalOffHireTime = Utils.GetPoint(job, "TechnicalOffHireTime", "gross").Value;
//                        x = (VoyageMaintenancePlannigAbility + TechnicalOffHireTime) / 2;
//                    }
//                    else if (index.Key.DictionaryName == "ShipTechnicalEfficiencyControl")
//                    {
//                        var VesselFaultSumNo = Utils.GetPoint(data, job, "VesselFaultSumNo", "gross").Value;
//                        var TechnicalOffHireTime = Utils.GetPoint(job, "TechnicalOffHireTime", "gross").Value;
//                        x = (VesselFaultSumNo + TechnicalOffHireTime) / 2;
//                    }
//                    else continue;

//                    Utils.AddEmployeePoint(job, index, "gross", x);

//                }

//            }


//        }
//    }
//    public class Rule13 : IRule<CalculationData>
//    {
//        public void Execute(CalculationData data)
//        {

//            if (data.PathNo != 2) return;
//            decimal z = Utils.GetCalculationPoint(data, "z").Value;
//            //محاسبه تعداد شاخص ها با اهمیت های مختلف
//            decimal total = 0;
//            int importanceWeight = 0;

//            foreach (var job in data.JobPositions)
//            {
//                decimal a1 = Utils.GetPoint(data, job, "a1").Value;
//                decimal b1 = Utils.GetPoint(data, job, "b1").Value;
//                decimal c1 = Utils.GetPoint(data, job, "c1").Value;
//                decimal d1 = Utils.GetPoint(data, job, "d1").Value;
//                decimal e1 = Utils.GetPoint(data, job, "e1").Value;

//                decimal a2 = Utils.GetPoint(data, job, "a2").Value;
//                decimal b2 = Utils.GetPoint(data, job, "b2").Value;
//                decimal c2 = Utils.GetPoint(data, job, "c2").Value;
//                decimal d2 = Utils.GetPoint(data, job, "d2").Value;
//                decimal e2 = Utils.GetPoint(data, job, "e2").Value;

//                decimal a3 = Utils.GetPoint(data, job, "a3").Value;
//                decimal b3 = Utils.GetPoint(data, job, "b3").Value;
//                decimal c3 = Utils.GetPoint(data, job, "c3").Value;
//                decimal d3 = Utils.GetPoint(data, job, "d3").Value;
//                decimal e3 = Utils.GetPoint(data, job, "e3").Value;

//                decimal sumGeneral = 0;
//                decimal sumTechnical = 0;
//                decimal sumEqualizer = 0;
//                Random rnd = new Random();
//                foreach (var index in job.Indices)
//                {
//                    RulePoint p = null;
//                    decimal x = 0;
//                    if ((new string[5] { "CostControl", "PereiodicMaintenancePlaningTime", "TechnicalOffHireTime", 
//                        "ShipTechnicalEfficiencyControl", "ContractorEfficiencyControl" }).Contains(index.Key.DictionaryName))
//                        p = Utils.GetPoint(job, index, "gross");
//                    else
//                        p = Utils.GetPoint(data, job, index, "gross");
//                    if (p == null)
//                        continue;
//                    else
//                        x = p.Value;

//                    if (Utils.IndexGroup(index) == "General")
//                    {
//                        if (Utils.IndexField(index, "Importance") == "1")
//                        {
//                            sumGeneral += Utils.AddEmployeePoint(job, index, "net", a1 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "3")
//                        {
//                            sumGeneral += Utils.AddEmployeePoint(job, index, "net", b1 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "5")
//                        {
//                            sumGeneral += Utils.AddEmployeePoint(job, index, "net", c1 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "7")
//                        {
//                            sumGeneral += Utils.AddEmployeePoint(job, index, "net", d1 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "9")
//                        {
//                            sumGeneral += Utils.AddEmployeePoint(job, index, "net", e1 * x).Value;
//                        }
//                    }
//                    else if (Utils.IndexGroup(index) == "Technical")
//                    {
//                        if (Utils.IndexField(index, "Importance") == "1")
//                        {
//                            sumTechnical += Utils.AddEmployeePoint(job, index, "net", a2 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "3")
//                        {
//                            sumTechnical += Utils.AddEmployeePoint(job, index, "net", b2 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "5")
//                        {
//                            sumTechnical += Utils.AddEmployeePoint(job, index, "net", c2 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "7")
//                        {
//                            sumTechnical += Utils.AddEmployeePoint(job, index, "net", d2 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "9")
//                        {
//                            sumTechnical += Utils.AddEmployeePoint(job, index, "net", e2 * x).Value;
//                        }
//                    }
//                    else if (Utils.IndexGroup(index) == "Equalizer")
//                    {
//                        if (Utils.IndexField(index, "Importance") == "1")
//                        {
//                            sumEqualizer += Utils.AddEmployeePoint(job, index, "net", a3 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "3")
//                        {
//                            sumEqualizer += Utils.AddEmployeePoint(job, index, "net", b3 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "5")
//                        {
//                            sumEqualizer += Utils.AddEmployeePoint(job, index, "net", c3 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "7")
//                        {
//                            sumEqualizer += Utils.AddEmployeePoint(job, index, "net", d3 * x).Value;
//                        }
//                        else if (Utils.IndexField(index, "Importance") == "9")
//                        {
//                            sumEqualizer += Utils.AddEmployeePoint(job, index, "net", e3 * x).Value;
//                        }
//                    }
//                }
//                Utils.AddEmployeePoint(job, "final-general", sumGeneral);
//                Utils.AddEmployeePoint(job, "initial-technical", sumTechnical);
//                Utils.AddEmployeePoint(job, "final-equalizer", sumEqualizer);
//                sumTechnical = sumTechnical * (1 - z / 2 + sumEqualizer);

//                if (sumTechnical > 50) sumTechnical = Math.Min(sumTechnical + sumTechnical * Convert.ToDecimal("0.05"), 80);

//                Utils.AddEmployeePoint(job, "final-technical", sumTechnical);
//                sumGeneral = Math.Min(sumGeneral + sumTechnical, 100);

//                if (sumGeneral > 18) sumGeneral = Math.Min(sumGeneral + sumGeneral * Convert.ToDecimal("0.05"), 100);
//                else if (sumGeneral < 18) sumGeneral = Math.Max(sumGeneral - sumGeneral * Convert.ToDecimal("0.05"), 0);
                
//                Utils.AddEmployeePoint(job, "final-job", sumGeneral);
//                total += sumGeneral;// * job.WorkTimePercent * job.Weight / 100;
//                importanceWeight+=job.WorkTimePercent*job.Weight/100;
//            }
//            Utils.AddEmployeePoint("final", total , true);
//            //if (importanceWeight > 0)
//            //    Utils.AddEmployeePoint("final", total / importanceWeight, true);
//            //else
//            //    Utils.AddEmployeePoint("final", total , true);



//        }
//    }
//    //public class Rule14 : IRule<CalculationData>
//    //{
//    //    public void Execute(CalculationData data)
//    //    {

//    //        if (data.PathNo != 1) return;
//    //        var jobs = data.JobPositions.Where(j => j.Job.DictionaryName == "TechnicalInspector");
//    //        if (jobs == null || jobs.Count() == 0) return;

//    //        decimal calcPointMinCostControl;
//    //        var rulePointCalcPointMinCostControl = Utils.GetCalculationPoint(data, "MinPereiodicMaintenancePlaningTime");
//    //        calcPointMinCostControl = rulePointCalcPointMinCostControl != null ? rulePointCalcPointMinCostControl.Value : decimal.MaxValue;

//    //        decimal calcPointMinTechnicalOffHireTime;
//    //        var rulePointCalcPointMinTechnicalOffHireTime = Utils.GetCalculationPoint(data, "MinTechnicalOffHireTime");
//    //        calcPointMinTechnicalOffHireTime = rulePointCalcPointMinTechnicalOffHireTime != null ? rulePointCalcPointMinTechnicalOffHireTime.Value : decimal.MaxValue;


//    //        foreach (var job in jobs)
//    //        {


//    //            //محاسبه تعداد شاخص ها با اهمیت های مختلف
//    //            foreach (var index in job.Indices)
//    //            {
//    //                decimal x = 0;
//    //                //فرهنگ و تعهد سازمانی
//    //                if (index.Key.DictionaryName == "OrganizationalCultureAndCommitment")
//    //                {
//    //                    var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    var inquiryPointExpertOfPurchasingAndTechnicalOrders = Utils.GetInquiryByJobPositionName(index, "ExpertOfPurchasingAndTechnicalOrdersJobPosition");
//    //                    var inquiryPointElectricalSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectricalSupervisorEngineerJobPosition");
//    //                    var inquiryPointElectronicsSupervisorEngineer = Utils.GetInquiryByJobPositionName(index, "ElectronicsSupervisorEngineerJobPosition");
//    //                    var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo);
//    //                    var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2 + inquiryPointExpertOfPurchasingAndTechnicalOrders + inquiryPointElectricalSupervisorEngineer + inquiryPointElectronicsSupervisorEngineer) / 9;
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "OrganizationalGrowthAndExcelency")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "Discipline")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "AdherenceToOrganizarionValues")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "HardWorking")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "EnglishLanquageSkills")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "ICDLFamiliarity")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "MentalAndPhisicalAbility")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }


//    //                // Technical ------------------------------------------------------------------------
//    //                //قابلیت برنامه ریزی و کنترل تعمیرات سفری
//    //                else if (index.Key.DictionaryName == "VoyageMaintenancePlannigAbility")
//    //                {
//    //                    var inquiryPointTechnicalInspectorJobPosition = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    var inquiryPointTechnicalInspector = Utils.GetInquiryByEmployeeNoAndJobPositionName(index, data.Employee.EmployeeNo);
//    //                    var inquiryPoint = (inquiryPointTechnicalInspectorJobPosition * 4 + inquiryPointTechnicalInspector * 2) / 6;
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                //برنامه ریزی و کنترل تعمیرات ادواری (کیفیت
//    //                else if (index.Key.DictionaryName == "PereiodicMaintenancePlaningQuality")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                //پروسه انتخاب پیمانکاران
//    //                else if (index.Key.DictionaryName == "ContractorSelectionProcess")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                //آنالير مشكلات فني ، پیگیری نواقص و ارائه راهکار
//    //                else if (index.Key.DictionaryName == "ProblemSolvingAbility")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                //به روزنگه داشتن گواهینامه ها و رعایت الزامات قانونی
//    //                else if (index.Key.DictionaryName == "CertificateUpdates")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                //آشنایی با نرم افزارهای مربوط به شغل
//    //                else if (index.Key.DictionaryName == "JobSoftwareFamiliarity")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                //کنترل هزینه
//    //                else if (index.Key.DictionaryName == "CostControl")
//    //                {
//    //                    //----------------------------------------------------------------------------------------------------
//    //                    continue;
//    //                    //x = Math.Abs(Convert.ToDecimal(job.CustomFields["DeclaredAnnualBudget"]) - Convert.ToDecimal(job.CustomFields["TotalAnnualCost"]) + 1) / Convert.ToDecimal(job.CustomFields["DeclaredAnnualBudget"]);
//    //                    //var calcPointMinCostControl = Utils.GetCalculationPoint(data, "MinCostControl");
//    //                    //if (calcPointMinCostControl == null)
//    //                    //    Utils.AddCalculationPoint("MinCostControl", x);
//    //                    //else if (x < calcPointMinCostControl.Value)
//    //                    //    Utils.AddCalculationPoint("MinCostControl", x);
//    //                }
//    //                else if (index.Key.DictionaryName == "BudgetQuality")
//    //                {
//    //                    continue;
//    //                    //var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    //x = inquiryPoint / 9;
//    //                }
//    //                //برنامه ریزی و کنترل تعمیرات ادواری (زمان
//    //                else if (index.Key.DictionaryName == "PereiodicMaintenancePlaningTime")
//    //                {
//    //                    if (job.CustomFields["PeriodicMaintenance"] == "0")
//    //                        continue;
//    //                    else
//    //                    {
//    //                        x = Math.Abs(Convert.ToDecimal(job.CustomFields["EstimatedPereiodicMaintenanceTime"]) - Convert.ToDecimal(job.CustomFields["ActualPereiodicMaintenanceTime"]) + 1) / Convert.ToDecimal(job.CustomFields["EstimatedPereiodicMaintenanceTime"]);
//    //                        if (x < calcPointMinCostControl)
//    //                            calcPointMinCostControl = x;
//    //                        //if (calcPointMinCostControl.HasValue)
//    //                        //    Utils.AddCalculationPoint("MinPereiodicMaintenancePlaningTime", x);
//    //                        //else if (x < calcPointMinCostControl.Value)
//    //                        //    
//    //                    }
//    //                }
//    //                //مدت زمان Off HIRE فني (ساعت)
//    //                else if (index.Key.DictionaryName == "TechnicalOffHireTime")
//    //                {
//    //                    x = (Convert.ToDecimal(job.CustomFields["TechnicalOffHireTime"]) / Convert.ToDecimal(job.CustomFields["ShipOperationalEfficiency"])) + 1;

//    //                    //if (calcPointMinTechnicalOffHireTime == null)
//    //                    //    Utils.AddCalculationPoint("MinTechnicalOffHireTime" , x);
//    //                    //else 
//    //                    if (x < calcPointMinTechnicalOffHireTime)
//    //                        calcPointMinTechnicalOffHireTime = x;
//    //                    // Utils.AddCalculationPoint("MinTechnicalOffHireTime" , x);
//    //                }
//    //                //مجموع تعداد نواقص اعلام شده توسط FSC و PSC برای شناورها
//    //                else if (index.Key.DictionaryName == "VesselFaultSumNo")
//    //                {
//    //                    decimal y = Convert.ToDecimal(job.CustomFields["PSCFSCvesselInspectionNO"]);
//    //                    x = 2 / ((Convert.ToDecimal(job.CustomFields["VesselFaultSumNo"]) / (y > 0 ? y : 1)) + 2);
//    //                }
//    //                //بهره وري دوره های تخصصی ( دوره مهندس ناظری)
//    //                else if (index.Key.DictionaryName == "TechnicalCourseAttending")
//    //                {
//    //                    x = Convert.ToDecimal(job.CustomFields["TechnicalCourseAttending"]) / 100;
//    //                }
//    //                else if (index.Key.DictionaryName == "PereiodicMaintenancePlaningQuality")
//    //                {
//    //                    if (job.CustomFields["PeriodicMaintenance"] == "0")
//    //                        continue;
//    //                    else
//    //                    {
//    //                        var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                        x = inquiryPoint / 9;
//    //                    }
//    //                }
//    //                else if (index.Key.DictionaryName == "VoyageMaintenancePlannigAbility")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "ContractorSelectionProcess")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "ProblemSolvingAbility")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "CertificateUpdates")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }
//    //                else if (index.Key.DictionaryName == "JobSoftwareFamiliarity")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = inquiryPoint / 9;
//    //                }



//    //                // Equalizer -------------------------------------------------------------------------------------
//    //                //مکان و محل تعمیرات
//    //                else if (index.Key.DictionaryName == "MaintenaceLocation")
//    //                {
//    //                    var inquiryPoint = Utils.GetInquiryByJobPositionName(index, "TechnicalInspectorJobPosition");
//    //                    x = (10 - inquiryPoint) / 9;
//    //                }
//    //                //سن كشتي
//    //                else if (index.Key.DictionaryName == "ShipAge")
//    //                {
//    //                    decimal no = Convert.ToDecimal(job.CustomFields["ShipNo"]);
//    //                    for (int i = 1; i < no + 1; i++)
//    //                    {
//    //                        decimal age = Convert.ToDecimal(job.CustomFields["ShipAge" + i]);
//    //                        if (age >= 0 && age < 5) age = 1;
//    //                        else if (age >= 5 && age < 10) age = 3;
//    //                        else if (age >= 10 && age < 15) age = 5;
//    //                        else if (age >= 15 && age < 20) age = 7;
//    //                        else if (age >= 20) age = 9;
//    //                        else continue;
//    //                        x += age;
//    //                    }
//    //                    x = x / no / 9;
//    //                }
//    //                //نوع كشتي
//    //                else if (index.Key.DictionaryName == "ShipType")
//    //                {
//    //                    decimal no = Convert.ToDecimal(job.CustomFields["ShipNo"]);
//    //                    for (int i = 1; i < no + 1; i++)
//    //                    {
//    //                        decimal type = Convert.ToDecimal(job.CustomFields["ShipType" + i]);
//    //                        if (type == 0) type = 8;
//    //                        else if (type == 1) type = 7;
//    //                        else type = 9;
//    //                        x += type;
//    //                    }
//    //                    x = x / no / 9;
//    //                }
//    //                //تعداد كشتي
//    //                else if (index.Key.DictionaryName == "ShipNo")
//    //                {
//    //                    decimal no = Convert.ToDecimal(job.CustomFields["ShipNo"]);
//    //                    x = no / 6;
//    //                }
//    //                //کیفیت پرسنل كشتي
//    //                else if (index.Key.DictionaryName == "ShipStaffQuality")
//    //                {
//    //                    x += (110 - Convert.ToDecimal(job.CustomFields["CommandersInquiryScore"])) / 100 * 0.328m;
//    //                    x += (110 - Convert.ToDecimal(job.CustomFields["FirstOfficersInquiryScore"])) / 100 * 0.138m;
//    //                    x += (110 - Convert.ToDecimal(job.CustomFields["ChiefEngineersInquiryScore"])) / 100 * 0.277m;
//    //                    x += (110 - Convert.ToDecimal(job.CustomFields["SecondOfficersInquiryScore"])) / 100 * 0.128m;
//    //                    x += (110 - Convert.ToDecimal(job.CustomFields["ElectronicEngineersInquiryScore"])) / 100 * 0.129m;
//    //                }
//    //                //کارکرد عملیاتی کشتی ( روز )
//    //                else if (index.Key.DictionaryName == "ShipOperationalEfficiency")
//    //                {
//    //                    x = Convert.ToDecimal(job.CustomFields["ShipOperationalEfficiency"]) / (Convert.ToInt32(job.CustomFields["ShipNo"]) * 30 * 3);
//    //                }
//    //                //مسیر حرکت کشتی
//    //                else if (index.Key.DictionaryName == "ShipPath")
//    //                {
//    //                    decimal no = Convert.ToDecimal(job.CustomFields["ShipNo"]);
//    //                    for (int i = 1; i < no + 1; i++)
//    //                    {
//    //                        decimal type = Convert.ToDecimal(job.CustomFields["ShipPath" + i]);
//    //                        if (type == 1) type = 9;
//    //                        else if (type == 2) type = 9;
//    //                        else if (type == 3) type = 7;
//    //                        else if (type == 4) type = 5;
//    //                        else if (type == 5) type = 5;
//    //                        else if (type == 6) type = 9;
//    //                        else if (type == 7) type = 5;
//    //                        else if (type == 8) type = 8;
//    //                        else if (type == 9) type = 9;
//    //                        x += type;
//    //                    }
//    //                    x = x / no / 9;
//    //                }
//    //                else continue;

//    //                Utils.AddEmployeePoint(job, index, "gross", x);
//    //            }

//    //        }
//    //        Utils.AddCalculationPoint("MinPereiodicMaintenancePlaningTime", calcPointMinCostControl);
//    //        Utils.AddCalculationPoint("MinTechnicalOffHireTime", calcPointMinTechnicalOffHireTime);


//    //    }
//    //}
//}