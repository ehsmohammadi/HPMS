using System;
using System.Linq;
using FluentMigrator;
using MITD.Data.NH;
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
                        PMSMigrationUtility.CreateJobIndex(jobIndexRep, jobIndex.Key, behaviouralGroup, true, cftDic);
                    }
                    if (jobIndex.Value == performanceGroupStr)
                    {
                        var cftDic =
                            AdminMigrationUtility.DefinedCustomFields.Where(d => jobIndex.Key.CustomFieldTypeIdList.Contains(d.Id))
                                .ToDictionary(c => c, c => value.ToString());
                        PMSMigrationUtility.CreateJobIndex(jobIndexRep, jobIndex.Key, performanceGroup, true, cftDic);
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

                var supportDepartmentManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "SupportDepartmentManager"), null,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                var deleiveryAndClearanceManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "DeleiveryAndClearanceManager"), supportDepartmentManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "DeleiveryAndClearanceEmployee"), deleiveryAndClearanceManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));


                var recordedAndClassifiedDocumentsManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "RecordedAndClassifiedDocumentsManager"), supportDepartmentManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "RecordedAndClassifiedDocumentsEmployee"), recordedAndClassifiedDocumentsManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));


                var supportAndServicesManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "SupportAndServicesManager"), supportDepartmentManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "SupportAndServicesEmployee"), supportAndServicesManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                var inventoryManager = PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "InventoryManager"), supportDepartmentManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));

                PMSMigrationUtility.CreateJobPosition(jobPositionRep,
                    AdminMigrationUtility.JobPositions.Single(j => j.DictionaryName == "InventoryClerk"), inventoryManager,
                    PMSMigrationUtility.Jobs.First(), PMSMigrationUtility.Units.Single(u => u.DictionaryName == "SupportDepartment"));


                #endregion

                var employeeRep = new PMS.Persistence.NH.EmployeeRepository(uow);

                #region Employee Creation

                PMSMigrationUtility.CreateEmployee(employeeRep, "100000", "مدیر", "مدیریان");

                PMSMigrationUtility.CreateEmployee(employeeRep, "652547", "مجتبی", "یاوری");
                PMSMigrationUtility.CreateEmployee(employeeRep, "100001", "کارمند ترخیص", "کارمندیان");

                PMSMigrationUtility.CreateEmployee(employeeRep,"671061","رمضان","محمدی فیروزه");
                PMSMigrationUtility.CreateEmployee(employeeRep, "100002", "کارمند ثبت", "کارمندیان");

                PMSMigrationUtility.CreateEmployee(employeeRep,"701392","عباس","داوودی");
                PMSMigrationUtility.CreateEmployee(employeeRep, "100003", "کارمند خدمات", "کارمندیان");

                PMSMigrationUtility.CreateEmployee(employeeRep,"670151","محمد علی","بخشی");
                PMSMigrationUtility.CreateEmployee(employeeRep, "100004", "کارمند انبار", "کارمندیان");


              

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
