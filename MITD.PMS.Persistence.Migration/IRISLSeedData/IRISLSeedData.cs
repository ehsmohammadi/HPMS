using System;
using FluentMigrator;
using MITD.Data.NH;
using MITD.PMS.Persistence.NH;
using MITD.PMS.Domain.Service;

namespace MITD.PMS.Persistence
{
    [Profile("IRISL")]
    public class IRISLSeedData : Migration
    {
        private string behaviouralGroupStr = "Behavioural";
        private string performanceGroupStr = "Performance";
        public override void Up()
        {


            #region  PMS Admin

            var uows = new MITD.Domain.Repository.UnitOfWorkScope(
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

                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "تحویل و ترخیص", "DeleiveryAndClearance");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "ثبت و طبقه بندی اسناد و مدارک", "RecordedAndClassifiedDocuments");
                AdminMigrationUtility.CreateJobPosition(jobPositionRep, "پشتیبانی و خدمات", "SupportAndServices");
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

                var behaviouralGroup = PMSMigrationUtility.CreateJobIndexGroup(jobIndexRep, "گروه رفتاری",
                    "BehaviouralGroup");
                var performanceGroup = PMSMigrationUtility.CreateJobIndexGroup(jobIndexRep, "گروه عملکردی",
                    "PerformanceGroup");

                foreach (var jobIndex in AdminMigrationUtility.JobIndices)
                {
                    //PMSMigrationUtility.CreateJobIndex(jobIndexRep,);
                }

                //var jobIndexGroupGenaral = new PMS.Domain.Model.JobIndices.JobIndexGroup(jobIndexRep.GetNextId(), period, null,
                //    "گروه شاخص های عمومی", "General");
                //jobIndexRep.Add(jobIndexGroupGenaral);
                

                //foreach (var itm in GenralJobIndexList)
                //{

                //    var sharedJobIndex =
                //        new PMS.Domain.Model.JobIndices.SharedJobIndex(
                //            new PMS.Domain.Model.JobIndices.SharedJobIndexId(itm.JobIndex.Id.Id), itm.JobIndex.Name,
                //            itm.JobIndex.DictionaryName);
                //    var jobIndex = new PMS.Domain.Model.JobIndices.JobIndex(jobIndexRep.GetNextId(), period,
                //        sharedJobIndex, jobIndexGroupGenaral, itm.IsInquirable);

                //    var dicSharedCutomField = jobIndexCftList
                //        .Where(j => itm.JobIndex.CustomFieldTypeIdList.Select(i => i.Id).Contains(j.Id.Id)).Select(p =>
                //            new PMS.Domain.Model.JobIndices.SharedJobIndexCustomField(
                //                new PMS.Domain.Model.JobIndices.SharedJobIndexCustomFieldId(p.Id.Id), p.Name,
                //                p.DictionaryName,
                //                p.MinValue, p.MaxValue)).ToDictionary(s => s, s => s.DictionaryName == "Importance" ? itm.Importance : string.Empty);

                //    jobIndex.UpdateCustomFields(dicSharedCutomField);
                //    jobIndexInPeriodList.Add(jobIndex);
                //    jobIndexRep.Add(jobIndex);

                //}

              
                #endregion

                var jobRep = new PMS.Persistence.NH.JobRepository(uow);

                #region Job creation

                //foreach (var pmsAdminJob in jobList)
                //{
                //    var jobJobIndices = jobIndexInPeriodList.Select(jobIndex => new JobJobIndex(jobIndex.Id, true, true, true)).ToList();
                //    if (pmsAdminJob.DictionaryName == "TechnicalManager")
                //        jobJobIndices = new List<JobJobIndex>();

                //    var job = new PMS.Domain.Model.Jobs.Job(period, new PMS.Domain.Model.Jobs.SharedJob(
                //        new PMS.Domain.Model.Jobs.SharedJobId(pmsAdminJob.Id.Id), pmsAdminJob.Name, pmsAdminJob.DictionaryName), jobCftList
                //        .Where(j => pmsAdminJob.CustomFieldTypeIdList.Select(i => i.Id)
                //            .Contains(j.Id.Id)).Select(p =>
                //                new PMS.Domain.Model.Jobs.JobCustomField(new PMS.Domain.Model.Jobs.JobCustomFieldId(period.Id, new SharedJobCustomFieldId(p.Id.Id), new SharedJobId(pmsAdminJob.Id.Id))
                //                    , new SharedJobCustomField(new SharedJobCustomFieldId(p.Id.Id), p.Name, p.DictionaryName, p.MinValue, p.MaxValue, p.TypeId))).ToList(), jobJobIndices);
                //    jobInPeriodList.Add(job);
                //    jobRep.Add(job);
                //}

                #endregion

                var unitRep = new PMS.Persistence.NH.UnitRepository(uow);

                #region Unit Creation

                //foreach (var pmsAdminUnit in unitList)
                //{
                //    var unit = new PMS.Domain.Model.Units.Unit(period, new PMS.Domain.Model.Units.SharedUnit(
                //        new PMS.Domain.Model.Units.SharedUnitId(pmsAdminUnit.Id.Id), pmsAdminUnit.Name, pmsAdminUnit.DictionaryName), null);
                //    unitInPeriodList.Add(unit);
                //    unitRep.Add(unit);
                //}

                #endregion

                var jobPositionRep = new PMS.Persistence.NH.JobPositionRepository(uow);

                #region JobPosition Creation

                //var jpIndex = 0;
                //var jobPositionParent = new PMS.Domain.Model.JobPositions.JobPosition(period,
                //        new Domain.Model.JobPositions.SharedJobPosition(new Domain.Model.JobPositions.SharedJobPositionId(technicalManagerJobPosition.Id.Id), technicalManagerJobPosition.Name, technicalManagerJobPosition.DictionaryName)
                //        , null,
                //        jobInPeriodList.Single(j => j.DictionaryName == "TechnicalManager"),
                //        unitInPeriodList.First()
                //        );
                //jobPositionInPeriodList.Add(jobPositionParent);
                //jobPositionRep.Add(jobPositionParent);
                //foreach (var pmsAdminJobPosition in jobPositionList.Where(j => j.DictionaryName != "TechnicalInspectorJobPosition").ToList())
                //{
                //    var jobPosition = new PMS.Domain.Model.JobPositions.JobPosition(period,
                //        new Domain.Model.JobPositions.SharedJobPosition(new Domain.Model.JobPositions.SharedJobPositionId(pmsAdminJobPosition.Id.Id), pmsAdminJobPosition.Name, pmsAdminJobPosition.DictionaryName)
                //        , jobPositionParent,
                //        jobInPeriodList.First(),
                //        unitInPeriodList[jpIndex]
                //        );
                //    jobPositionInPeriodList.Add(jobPosition);
                //    jobPositionRep.Add(jobPosition);
                //    jpIndex++;
                //}

                #endregion

                var employeeRep = new PMS.Persistence.NH.EmployeeRepository(uow);

                #region Employee Creation




                //var employee1 =
                //        new PMS.Domain.Model.Employees.Employee(
                //            ((1 + 1) * 2000).ToString(), period, "کارمند" + 1,
                //            "کارمندیان" + 1);

                //var jobPositionInPeriod1 = jobPositionInPeriodList[1];

                //var jobcustomFields1 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod1.JobId)).CustomFields;
                //if (jobcustomFields1 != null && jobcustomFields1.Count != 0)
                //{
                //    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee1, jobPositionInPeriod1, period.StartDate, period.EndDate, 100, 1,
                //    jobcustomFields1.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value1)).ToList()
                //    );
                //    employee1.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                //}

                //empList.Add(employee1);
                //employeeRep.Add(employee1);


                //var employee2 =
                //            new PMS.Domain.Model.Employees.Employee(
                //                ((2 + 2) * 2000).ToString(), period, "کارمند" + 2,
                //                "کارمندیان" + 2);

                //var jobPositionInPeriod2 = jobPositionInPeriodList[2];

                //var jobcustomFields2 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod2.JobId)).CustomFields;
                //if (jobcustomFields2 != null && jobcustomFields2.Count != 0)
                //{
                //    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee2, jobPositionInPeriod2, period.StartDate, period.EndDate, 100, 1,
                //    jobcustomFields2.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value2)).ToList()
                //    );
                //    employee2.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                //}

                //empList.Add(employee2);
                //employeeRep.Add(employee2);


                //var employee3 =
                //           new PMS.Domain.Model.Employees.Employee(
                //               ((3 + 3) * 2000).ToString(), period, "کارمند" + 3,
                //               "کارمندیان" + 3);

                //var jobPositionInPeriod3 = jobPositionInPeriodList[3];

                //var jobcustomFields3 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod3.JobId)).CustomFields;
                //if (jobcustomFields3 != null && jobcustomFields3.Count != 0)
                //{
                //    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee3, jobPositionInPeriod3, period.StartDate, period.EndDate, 100, 1,
                //    jobcustomFields3.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value3)).ToList()
                //    );
                //    employee3.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                //}

                //empList.Add(employee3);
                //employeeRep.Add(employee3);


                //var employee4 =
                //           new PMS.Domain.Model.Employees.Employee(
                //              ((4 + 4) * 2000).ToString(), period, "کارمند" + 4,
                //               "کارمندیان" + 4);

                //var jobPositionInPeriod4 = jobPositionInPeriodList[4];

                //var jobcustomFields4 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod4.JobId)).CustomFields;
                //if (jobcustomFields4 != null && jobcustomFields4.Count != 0)
                //{
                //    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee4, jobPositionInPeriod4, period.StartDate, period.EndDate, 100, 1,
                //    jobcustomFields4.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value4)).ToList()
                //    );
                //    employee4.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                //}

                //empList.Add(employee4);
                //employeeRep.Add(employee4);


                //var employee5 =
                //           new PMS.Domain.Model.Employees.Employee(
                //              ((5 + 5) * 2000).ToString(), period, "کارمند" + 5,
                //               "کارمندیان" + 5);

                //var jobPositionInPeriod5 = jobPositionInPeriodList[5];

                //var jobcustomFields5 = jobInPeriodList.Single(j => j.Id.Equals(jobPositionInPeriod5.JobId)).CustomFields;
                //if (jobcustomFields5 != null && jobcustomFields5.Count != 0)
                //{
                //    var employeeJobPosition = new Domain.Model.Employees.EmployeeJobPosition(employee5, jobPositionInPeriod5, period.StartDate, period.EndDate, 100, 1,
                //    jobcustomFields5.Select(j => new EmployeeJobCustomFieldValue(j.Id, docSeedValues.Single(d => d.DicName == j.DictionaryName).Value5)).ToList()
                //    );
                //    employee5.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition }, periodManagerService);

                //}

                //empList.Add(employee5);
                //employeeRep.Add(employee5);

                //var employee0 =
                //       new PMS.Domain.Model.Employees.Employee(
                //          ((0 + 1) * 2000).ToString(), period, "مدیر",
                //           "امور");

                //var jobPositionInPeriod0 = jobPositionInPeriodList[0];
                //var employeeJobPosition0 = new Domain.Model.Employees.EmployeeJobPosition(employee0, jobPositionInPeriod0, period.StartDate, period.EndDate, 100, 1,
                //null
                //);
                //employee0.AssignJobPositions(new List<Domain.Model.Employees.EmployeeJobPosition> { employeeJobPosition0 }, periodManagerService);
                //empList.Add(employee0);
                //employeeRep.Add(employee0);


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

            
            #endregion




            //using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            //{
            //    var jobPositionRep = new PMS.Persistence.NH.JobPositionRepository(uow);
            //    var jobRep = new PMS.Persistence.NH.JobRepository(uow);
            //    var jobIndexRep = new PMS.Persistence.NH.JobIndexRepository(uow);
            //    var inquiryRep = new InquiryJobIndexPointRepository(uow);
            //    var inquiryConfiguratorService = new JobPositionInquiryConfiguratorService(jobPositionRep);

            //    foreach (var jobPosition in jobPositionInPeriodList)
            //    {
            //        var jobp = jobPositionRep.GetBy(jobPosition.Id);
            //        jobp.ConfigeInquirer(inquiryConfiguratorService, false);
            //    }
            //    uow.Commit();
            //}

            //using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            //{
            //    var jobPositionRep = new PMS.Persistence.NH.JobPositionRepository(uow);
            //    var jobRep = new PMS.Persistence.NH.JobRepository(uow);
            //    var jobIndexRep = new PMS.Persistence.NH.JobIndexRepository(uow);
            //    var inquiryRep = new InquiryJobIndexPointRepository(uow);
            //    var inquiryConfiguratorService = new JobPositionInquiryConfiguratorService(jobPositionRep);
            //    var periodRep = new PeriodRepository(uow);

            //    foreach (var jobPosition in jobPositionInPeriodList)
            //    {
            //        var jobp = jobPositionRep.GetBy(jobPosition.Id);
            //        foreach (var itm in jobp.ConfigurationItemList)
            //        {
            //            var job = jobRep.GetById(itm.JobPosition.JobId);
            //            foreach (var jobIndexId in job.JobIndexList)
            //            {
            //                var jobIndex = jobIndexRep.GetById(jobIndexId.JobIndexId);
            //                if ((jobIndex as JobIndex).IsInquireable)
            //                {
            //                    string value = string.Empty;
            //                    if (jobPosition.DictionaryName.Contains("1"))
            //                        value =
            //                            docSeedValues.Single(
            //                                d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
            //                                .Value1;
            //                    if (jobPosition.DictionaryName.Contains("2"))
            //                        value =
            //                            docSeedValues.Single(
            //                                d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
            //                                .Value2;
            //                    if (jobPosition.DictionaryName.Contains("3"))
            //                        value =
            //                            docSeedValues.Single(
            //                                d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
            //                                .Value3;
            //                    if (jobPosition.DictionaryName.Contains("4"))
            //                        value =
            //                            docSeedValues.Single(
            //                                d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
            //                                .Value4;
            //                    if (jobPosition.DictionaryName.Contains("5"))
            //                        value =
            //                            docSeedValues.Single(
            //                                d => d.DicName == (jobIndex as Domain.Model.JobIndices.JobIndex).DictionaryName)
            //                                .Value5;
            //                    var id = inquiryRep.GetNextId();
            //                    var inquiryIndexPoint = new Domain.Model.InquiryJobIndexPoints.InquiryJobIndexPoint(
            //                        new Domain.Model.InquiryJobIndexPoints.InquiryJobIndexPointId(id),
            //                        itm, jobIndex as Domain.Model.JobIndices.JobIndex, value);
            //                    inquiryRep.Add(inquiryIndexPoint);
            //                }

            //            }
            //        }
            //    }
            //    var p = periodRep.GetById(period.Id);
            //    p.State = new PeriodInquiryCompletedState();
            //    uow.Commit();
            //}

            //using (var uow = uows.CurrentUnitOfWork as NHUnitOfWork)
            //{
            //    EventPublisher publisher = new EventPublisher();
            //    var rebps = new RuleBasedPolicyEngineService(new LocatorProvider("PMSDb"), publisher);
            //    var policyRep = new MITD.PMS.Persistence.NH.PolicyRepository(uow,
            //        new PolicyConfigurator(rebps));
            //    var periodRep = new PeriodRepository(uow);
            //    var pmsPolicy = policyRep.GetById(new PolicyId(policy.Id.Id));
            //    var p = periodRep.GetById(period.Id);
            //    var calcRep = new CalculationRepository(uow);
            //    var calc = new Calculation(calcRep.GetNextId(), p, pmsPolicy,
            //        "محاسبه آزمایشی", DateTime.Now, empList[0].Id.EmployeeNo + ";" + empList[1].Id.EmployeeNo);
            //    calcRep.Add(calc);
            //    uow.Commit();
            //}


        }



        public override void Down()
        {
        }
    }

}
