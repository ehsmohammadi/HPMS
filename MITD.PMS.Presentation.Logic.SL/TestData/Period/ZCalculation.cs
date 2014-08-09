using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using MITD.PMS.Presentation.Contracts;

namespace MITD.PMS.Presentation.Logic
{ 
    public static partial class TestData
    {
        private static List<int> calculationActionCodes = new List<int>
            {
                (int)ActionType.AddPeriodCalculationExec , 
                (int)ActionType.ModifyPeriodCalculationExec ,
                (int)ActionType.DeletePeriodCalculationExec,
                (int)ActionType.ShowPeriodCalculationResult,
                (int)ActionType.FinalizePeriodCalculationExec,
                (int)ActionType.ShowPeriodCalculationState,
            };

        static TestData()
        {

            //totalScoreList = new List<EmployeeCalcTotalScorDTOWithAction>
                //{
                    //new EmployeeCalcTotalScorDTOWithAction
                    //    {
                    //        ActionCodes = totalScoreActionCodes,
                    //        EmployeeId = employeeList.Single(p => p.Id == 1).Id,
                    //        EmployeeName =
                    //            employeeList.Single(p => p.Id == 1).FirstName + " " +
                    //            employeeList.Single(p => p.Id == 1).LastName,
                    //        TotalScore = 80,
                    //        EmployeeRankInPeriod = 3,
                    //    },
                    //new EmployeeCalcTotalScorDTOWithAction
                    //    {
                    //        ActionCodes = totalScoreActionCodes,
                    //        EmployeeId = employeeList.Single(p => p.Id == 2).Id,
                    //        EmployeeName =
                    //            employeeList.Single(p => p.Id == 2).FirstName + " " +
                    //            employeeList.Single(p => p.Id == 2).LastName,
                    //        TotalScore = 79,
                    //        EmployeeRankInPeriod = 1,
                    //    },
                    //new EmployeeCalcTotalScorDTOWithAction
                    //    {
                    //        ActionCodes = totalScoreActionCodes,
                    //        EmployeeId = employeeList.Single(p => p.Id == 3).Id,
                    //        EmployeeName =
                    //            employeeList.Single(p => p.Id == 3).FirstName + " " +
                    //            employeeList.Single(p => p.Id == 3).LastName,
                    //        TotalScore = 95,
                    //        EmployeeRankInPeriod = 4,
                    //    },
                    //new EmployeeCalcTotalScorDTOWithAction
                    //    {
                    //        ActionCodes = totalScoreActionCodes,
                    //        EmployeeId = employeeList.Single(p => p.Id == 4).Id,
                    //        EmployeeName =
                    //            employeeList.Single(p => p.Id == 4).FirstName + " " +
                    //            employeeList.Single(p => p.Id == 1).LastName,
                    //        TotalScore = 69,
                    //        EmployeeRankInPeriod = 2,
                    //    },
                //};
            //ruleList = new List<RuleDTOWithAction>
            //    {
            //        new RuleDTOWithAction
            //            {
            //                Id = 1,
            //                DictionaryName = "CalculateJobIndexWeights",
            //                Name = "محاسبه وزن های شاخص ها " + " " + policyList.Single(p => p.Id == 1).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p => p.Id == 1).Id,
            //                ExcuteTime = (int)RuleExcuteTime.CalculationStarting,
            //                ExcuteOrder=1
            //            },
            //            new RuleDTOWithAction
            //            {
            //                Id = 2,
            //                DictionaryName = "CalculateEmployeeJobIndexPoint",
            //                Name = "محاسبه نمره شاخص فرد "+" "+policyList.Single(p=>p.Id==1).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p=>p.Id==1).Id,
            //                ExcuteTime = (int)RuleExcuteTime.PerEmployee,
            //                ExcuteOrder=2
            //            },
            //            new RuleDTOWithAction
            //            {
            //                Id = 3,
            //                DictionaryName = "CalculateEmployeeGeneralJobIndexPoint",
            //                Name = "محاسبه میانگین نمرات شاخص های عمومی "+" "+policyList.Single(p=>p.Id==1).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p=>p.Id==1).Id,
            //                ExcuteTime = (int)RuleExcuteTime.PerEmployee,
            //                ExcuteOrder=1
            //            },
            //            new RuleDTOWithAction
            //            {
            //                Id = 4,
            //                DictionaryName = "Rule 4",
            //                Name = "قانون 4 "+" "+policyList.Single(p=>p.Id==1).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p=>p.Id==1).Id
            //            },
            //            new RuleDTOWithAction
            //            {
            //                Id = 5,
            //                DictionaryName = "Rule 5",
            //                Name = "قانون 5 "+" "+policyList.Single(p=>p.Id==1).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p=>p.Id==1).Id
            //            },
            //            new RuleDTOWithAction
            //            {
            //                Id = 6,
            //                DictionaryName = "Rule 6",
            //                Name = "قانون 6 "+" "+policyList.Single(p=>p.Id==1).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p=>p.Id==1).Id
            //            },
            //            new RuleDTOWithAction
            //            {
            //                Id = 7,
            //                DictionaryName = "Rule 1",
            //                Name = "قانون 1 "+" "+policyList.Single(p=>p.Id==2).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p=>p.Id==2).Id
            //            },
            //            new RuleDTOWithAction
            //            {
            //                Id = 8,
            //                DictionaryName = "Rule 2",
            //                Name = "قانون 2 "+" "+policyList.Single(p=>p.Id==2).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p=>p.Id==2).Id
            //            },
            //            new RuleDTOWithAction
            //            {
            //                Id = 9,
            //                DictionaryName = "Rule 3",
            //                Name = "قانون 3 "+" "+policyList.Single(p=>p.Id==2).Name,
            //                VersionsCount = 1,
            //                ActionCodes = ruleActionCodes,
            //                PolicyId = policyList.Single(p=>p.Id==2).Id
            //            }

            //    };
//            RuleRuleVersion = new Dictionary<long, List<RuleVersionDTO>>
//                {
//                    {
//                        ruleList.Single(r => r.Id == 1).Id, new List<RuleVersionDTO>
//                            {
//                                new RuleVersionDTO
//                                    {
//                                        Content = 
//                                            @"
////محاسبه تعداد شاخص ها با اهمیت های مختلف
//a1=Indexes.Count(i=>i.CustomFields[""Importance""]==1 && i.IndexGroup.DictionaryName==""General"");
//b1=Indexes.Count(i=>i.CustomFields[""Importance""]==3 && i.IndexGroup.DictionaryName==""General"");
//c1=Indexes.Count(i=>i.CustomFields[""Importance""]==5 && i.IndexGroup.DictionaryName==""General"");
//d1=Indexes.Count(i=>i.CustomFields[""Importance""]==7 && i.IndexGroup.DictionaryName==""General"");
//e1=Indexes.Count(i=>i.CustomFields[""Importance""]==9 && i.IndexGroup.DictionaryName==""General"");
//
////محاسبه عدد وزنی شاخص های عمومی
//Y1=20/(9*a1+7*b1+5*c1+3*d1+e1);
//
////محاسبه وزن شاخص های عمومی
//Calculation.CustomFields[""MuchMoreImportantGeneralIndexWeight""]=9*Y1;
//Calculation.CustomFields[""MoreImportantGeneralIndexWeight""]=7*Y1;
//Calculation.CustomFields[""ImportantGeneralIndexWeight""]=5*Y1;
//Calculation.CustomFields[""LessImportantGeneralIndexWeight""]=3*Y1;
//Calculation.CustomFields[""MuchLessImportantGeneralIndexWeight""]=Y1;
//
////محاسبه تعداد شاخص ها با اهمیت های مختلف
//a2=Indexes.Count(i=>i.CustomFields[""Importance""]==1 && i.IndexGroup.DictionaryName==""General"");
//b2=Indexes.Count(i=>i.CustomFields[""Importance""]==3 && i.IndexGroup.DictionaryName==""General"");
//c2=Indexes.Count(i=>i.CustomFields[""Importance""]==5 && i.IndexGroup.DictionaryName==""General"");
//d2=Indexes.Count(i=>i.CustomFields[""Importance""]==7 && i.IndexGroup.DictionaryName==""General"");
//e2=Indexes.Count(i=>i.CustomFields[""Importance""]==9 && i.IndexGroup.DictionaryName==""General"");
//
////محاسبه عدد وزنی شاخص های تخصصی
//Y2=80/(9*a2+7*b2+5*c2+3*d2+e2);
//
////محاسبه وزن شاخص های تخصصی
//Calculation.CustomFields[""MuchMoreImportantTechnicalIndexWeight""]=9*Y2;
//Calculation.CustomFields[""MoreImportantTechnicalIndexWeight""]=9*Y2;
//Calculation.CustomFields[""ImportantTechnicalIndexWeight""]=9*Y2;
//Calculation.CustomFields[""LessImportantTechnicalIndexWeight""]=9*Y2;
//Calculation.CustomFields[""MuchLessImportantTechnicalIndexWeight""]=9*Y2;
//
////محاسبه تعداد شاخص ها با اهمیت های مختلف
//a3=Indexes.Count(i=>i.CustomFields[""Importance""]==1 && i.IndexGroup.DictionaryName==""General"");
//b3=Indexes.Count(i=>i.CustomFields[""Importance""]==3 && i.IndexGroup.DictionaryName==""General"");
//c3=Indexes.Count(i=>i.CustomFields[""Importance""]==5 && i.IndexGroup.DictionaryName==""General"");
//d3=Indexes.Count(i=>i.CustomFields[""Importance""]==7 && i.IndexGroup.DictionaryName==""General"");
//e3=Indexes.Count(i=>i.CustomFields[""Importance""]==9 && i.IndexGroup.DictionaryName==""General"");
//
////محاسبه عدد وزنی شاخص های همسان ساز
//Y3=0.1/(9*a3+7*b3+5*c3+3*d3+e3);
//
////محاسبه وزن شاخص های همسان ساز
//Calculation.CustomFields[""MuchMoreImportantHomogenizingIndexWeight""]=9*Y3;
//Calculation.CustomFields[""MoreImportantHomogenizingIndexWeight""]=9*Y3;
//Calculation.CustomFields[""ImportantHomogenizingIndexWeight""]=9*Y3;
//Calculation.CustomFields[""LessImportantHomogenizingIndexWeight""]=9*Y3;
//Calculation.CustomFields[""MuchLessImportantHomogenizingIndexWeight""]=9*Y3;
//
//                                             ",
//                                        Id = 1,
//                                        PeriodId = periodList.Single(p => p.Id == 1).Id,
//                                        VersionId = 1,
//                                    },
//                                //new RuleVersionDTO
//                                //    {
//                                //        Content = "متن قانون",
//                                //        Id = 2,
//                                //        PeriodId = periodList.Single(p => p.Id == 2).Id,
//                                //        VersionId = 2,
//                                //    },
//                                //new RuleVersionDTO
//                                //    {
//                                //        Content = "متن قانون",
//                                //        Id = 3,
//                                //        PeriodId = periodList.Single(p => p.Id == 3).Id,
//                                //        VersionId = 3,
//                                //    }
//                            }
//                    },
//                    {
//                        ruleList.Single(r => r.Id == 2).Id, new List<RuleVersionDTO>
//                            {
//                                new RuleVersionDTO
//                                    {
//                                        Content = 
//@"
//foreach(var index in Indexes)
//{
//    if(index.JobIndexGroup.DictionaryName==""General"")
                                //    {
//        if(index.CustomFields[""Importance""]==9)
//        {
//            Points[index.DictionaryName]=Calculation.CustomFields[""MuchMoreImportantGeneralIndexWeight""]*Indexes[""index""];
//        }
//        if(index.CustomFields[""Importance""]==7)
//        {
//            Points[index.DictionaryName]=Calculation.CustomFields[""MoreImportantGeneralIndexWeight""]*Indexes[""index""];
//        }
//        if(index.CustomFields[""Importance""]==5)
//        {
//            Points[index.DictionaryName]=Calculation.CustomFields[""ImportantGeneralIndexWeight""]*Indexes[""index""];
//        }
//        if(index.CustomFields[""Importance""]==3)
//        {
//            Points[index.DictionaryName]=Calculation.CustomFields[""LessImportantGeneralIndexWeight""]*Indexes[""index""];
//        }
//        if(index.CustomFields[""Importance""]==1)
//        {
//            Points[index.DictionaryName]=Calculation.CustomFields[""MuchLessImportantGeneralIndexWeight""]*Indexes[""index""];
//        }
//    }
//   Point[""TotalGeneralJobIndex""]+=Points[index.DictionaryName];
//   
//}
//Point[""AdjustedTotalTechnicalJobIndex""]=Math.Min(80,Point[""TotalTechnicalJobIndex""]*((1-0.1)/2+Point[""TotalHomogenizingJobIndex""]))
//Point[""Total""]=Point[""TotalGeneralJobIndex""]+Point[""AdjustedTotalTechnicalJobIndex""];
//
//",
//                                        Id = 4,
//                                        PeriodId = periodList.Single(p => p.Id == 1).Id,
//                                        VersionId = 1,
//                                    },
//                            }
//                    },
//                    {
//                        ruleList.Single(r => r.Id == 3).Id, new List<RuleVersionDTO>
//                            {
//                                new RuleVersionDTO
//                                    {
//                                        Content = 
//@"
//var weights=new int[4]={5,3,2,1};
//foreach(var inquiry in Inquiries)
//{
//    var total=0;
//    foreach(var employee in inquiry.Employees)
                                //    {
//        total+=weight[employee.JobPosition.Rank]*employee.Value;  
                                //    }
//    Indexes[Inquiry.DictionaryName]=total;
//    
//}
//",
//                                        Id = 7,
//                                        PeriodId = periodList.Single(p => p.Id == 1).Id,
//                                        VersionId = 1,
//                                    },

//                            }
//                    },
//                    {
//                        ruleList.Single(r => r.Id == 4).Id, new List<RuleVersionDTO>
//                            {
//                                new RuleVersionDTO
//                                    {
//                                        Content = "متن قانون",
//                                        Id = 10,
//                                        PeriodId = periodList.Single(p => p.Id == 1).Id,
//                                        VersionId = 1,
//                                    },
//                                new RuleVersionDTO
//                                    {
//                                        Content = "متن قانون",
//                                        Id = 11,
//                                        PeriodId = periodList.Single(p => p.Id == 2).Id,
//                                        VersionId = 2,
//                                    },
//                                new RuleVersionDTO
//                                    {
//                                        Content = "متن قانون",
//                                        Id = 12,
//                                        PeriodId = periodList.Single(p => p.Id == 3).Id,
//                                        VersionId = 3,
//                                    }
//                            }
//                    },
//                    {
//                        ruleList.Single(r => r.Id == 5).Id, new List<RuleVersionDTO>
//                            {
//                                new RuleVersionDTO
//                                    {
//                                        Content = "متن قانون",
//                                        Id = 13,
//                                        PeriodId = periodList.Single(p => p.Id == 1).Id,
//                                        VersionId = 1,
//                                    },
//                                new RuleVersionDTO
//                                    {
//                                        Content = "متن قانون",
//                                        Id = 14,
//                                        PeriodId = periodList.Single(p => p.Id == 2).Id,
//                                        VersionId = 2,
//                                    },
//                                new RuleVersionDTO
//                                    {
//                                        Content = "متن قانون",
//                                        Id = 15,
//                                        PeriodId = periodList.Single(p => p.Id == 3).Id,
//                                        VersionId = 3,
//                                    }
//                            }
//                    }
//                };
            JobInPeriodDescriptionList=new List<JobInPeriodDTO>
                {
                    new JobInPeriodDTO
                        {
                            JobId = jobList.Single(j=>j.Id==1).Id,
                            Name = jobList.Single(j=>j.Id==1).Name
                        },
                        new JobInPeriodDTO
                        {
                            JobId = jobList.Single(j=>j.Id==2).Id,
                            Name = jobList.Single(j=>j.Id==2).Name
                        }
                };
            
        }

        private static readonly List<int> actionCode = new List<int>
            {
                (int) ActionType.AddPeriodCalculationExec,
                (int) ActionType.ModifyPeriodCalculationExec,
                (int) ActionType.DeletePeriodCalculationExec,
                (int) ActionType.ShowPeriodCalculationResult,
                (int) ActionType.FinalizePeriodCalculationExec,
                (int) ActionType.ShowPeriodCalculationState,
            };
        private static readonly List<int> totalScoreActionCodes = new List<int>
            {
                (int) ActionType.ShowPeriodCalculationResultDetails,
                //(int) ActionType.DeletePeriodCalculation,
                //(int) ActionType.ShowPeriodCalculationResult,
                //(int) ActionType.FinalizePeriodCalculation,
            };



        public static List<CalculationDTOWithAction> calculationList;

        public static List<JobInPeriodDTO> JobInPeriodDescriptionList
        {
            get; set; }
    }
}
