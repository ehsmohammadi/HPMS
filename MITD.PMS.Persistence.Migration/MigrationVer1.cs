using System;
using FluentMigrator;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Persistence
{
    [Migration(1)]
    public class MigrationVer1 : Migration
    {
        public override void Up()
        {

            createNH_HiloTable();

            createPMSActionsTable();

            createUsersTable();

            createREConfigItemsTable();

            createRulesBaseTable();

            createRulesTable();

            createRulesTrailTable();

            createRuleFunctionsBaseTable();

            createRuleFunctionsTable();

            createRuleFunctionsTrailTable();

            createPeriodsTable();

            createCustomFieldTypesTable();

            createJobTable();

            createUnitTable();

            createJobPostionTable();

            createPoliciesTable();

            createPolicies_RETable();

            createPolicies_RE_RulesTable();

            createPolicies_RE_RFTable();

            createPeriods_JobsTable();

            createPeriods_Jobs_CustomFieldsTable();

            createPeriods_UnitsTable();

            createPeriods_Units_CustomFieldsTable();


            createUnits_CustomFieldsTable();
            
            createJobs_CustomFieldsTable();
            
            createPeriods_JobPositionssTable();

            createEmployees();

            createCalculationsTable();

            createJobIndexTable();

            createUnitIndexTable();
       
            createPeriod_UnitIndex();

            createPeriod_JobIndex();

            createPeriodJob_JobIndices();

            createPeriodUnit_UnitIndices();

            createInquiryTable();

            createJobIndexPointTable();

            createClaimTable();

            createLogTable();

            createSequence();

            #region NH_Hilo
            Insert.IntoTable("NH_Hilo").Row(new
                {
                    TableKey = "Periods_Jobs",
                    NextHi = 3
                }).Row(new
            {
                TableKey = "Periods_Jobs_CustomFields",
                NextHi = 3
            }).Row(new
            {
                TableKey = "REConfigItems",
                NextHi = 3
            }).Row(new
            {
                TableKey = "Periods_Units",
                NextHi = 3
            }).Row(new
            {
                TableKey = "Periods_JobPositions",
                NextHi = 3
            }).Row(new
            {
                TableKey = "Employees",
                NextHi = 3
            }).Row(new
            {
                TableKey = "Periods_Jobs_CustomFields",
                NextHi = 10
            }).Row(new
            {
                TableKey = "Employees_JobPositions",
                NextHi = 10
            }).Row(new
                {
                    TableKey = "JobPostion_InquiryConfigurationItems",
                    NextHi = 10
                }).Row(new
                {
                    TableKey = "Employees_JobCustomField_Values",
                    NextHi = 10
                }).Row(new
                {
                    TableKey = "Parties",
                    NextHi = 10
                }).Row(new
                {
                    TableKey = "Jobs",
                    NextHi = 10
                }).Row(new
                {
                    TableKey = "Logs",
                    NextHi = 10
                });



            #endregion

            #region ActionTypes

            foreach (var actiontype in Enumeration.GetAll<ActionType>())
            {
                Insert.IntoTable("ActionTypes").Row(new
                {
                    Id = actiontype.Value,
                    Name = actiontype.DisplayName,

                });
            }





            #endregion



        }

        private void createPeriod_UnitIndex()
        {
            Create.Table("Periods_AbstractUnitIndices")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("PeriodId").AsInt64().NotNullable();


            Create.Table("Periods_UnitIndexGroups")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("fk_Period_AbstractUnitIndices_UnitIndexGroups_Id", "Periods_AbstractUnitIndices", "Id")

                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(512).NotNullable()
                  .WithColumn("ParentId").AsInt64().Nullable()
                  .ForeignKey("fk_Periods_UnitIndexGroups_UnitIndexGroups_ParentId", "Periods_UnitIndexGroups", "Id");

            Create.Table("Periods_UnitIndices")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("fk_Periods_AbstractUnitIndices_Periods_UnitIndices_Id", "Periods_AbstractUnitIndices", "Id")

                  .WithColumn("UnitIndexId").AsInt64().NotNullable()
                  .ForeignKey("fk_UnitIndices_Periods_UnitIndices_UnitIndexId", "UnitIndices", "Id")
                  .WithColumn("GroupId").AsInt64().NotNullable()
                  .ForeignKey("fk_Periods_UnitIndexGroups_Periods_UnitIndices_GroupId", "Periods_UnitIndexGroups", "Id")
                  .WithColumn("IsInquireable").AsBoolean().NotNullable()
                  .WithColumn("CalculationLevel").AsInt64().Nullable()
                  .WithColumn("CalculationOrder").AsInt32().Nullable()
                  .WithColumn("RefUnitIndexId").AsInt64().Nullable()
                  .ForeignKey("Periods_UnitIndices", "Id");

            Create.Table("Periods_UnitIndices_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("PeriodUnitIndexId").AsInt64().NotNullable()
                  .ForeignKey("fk_Periods_UnitIndices_CustomFields_UnitIndexId", "Periods_UnitIndices", "Id")
                  .WithColumn("CustomFieldTypeId").AsInt64().NotNullable()
                  .ForeignKey("fk_JPeriods_UnitIndices_CustomFields_CustomFieldTypeId", "CustomFieldTypes", "Id")
                  .WithColumn("CustomFieldValue").AsString();
        }

        private void createPeriods_Units_CustomFieldsTable()
        {
            Create.Table("Periods_Units_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")

                  .WithColumn("PeriodUnitId").AsInt64().Nullable()
                  .ForeignKey("Periods_Units", "Id")

                  .WithColumn("CustomFieldId").AsInt64().NotNullable()
                  .ForeignKey("CustomFieldTypes", "Id")

                  .WithColumn("PeriodId").AsInt64().NotNullable()
                  .ForeignKey("Periods", "Id")

                  .WithColumn("UnitId").AsInt64().NotNullable()
                  .ForeignKey("Units", "Id");
        }

        private void createPeriodUnit_UnitIndices()
        {
            Create.Table("Periods_Units_UnitIndices")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()

                .WithColumn("RowVersion").AsCustom("rowversion")

                .WithColumn("PeriodUnitId").AsInt64().NotNullable()
                .ForeignKey("fk_Periods_Units_UnitIndices_PeriodUnitId", "Periods_Units", "Id")

                .WithColumn("PeriodUnitIndexId").AsInt64().NotNullable()
                .ForeignKey("fk_Periods_Units_UnitIndices_PeriodUnitIndexId", "Periods_UnitIndices", "Id")
                .WithColumn("ShowforTopLevel").AsBoolean()
                .WithColumn("ShowforSameLevel").AsBoolean()
                .WithColumn("ShowforLowLevel").AsBoolean();



        }


        public override void Down()
        {
            Delete.Table("NH_Hilo");

            Delete.Table("Periods_Claims");
            Delete.Table("Inquiry_JobIndexPoints");
            Delete.Table("JobPostion_InquiryConfigurationItems");
            Delete.Table("JobIndexPoints");
            Delete.Table("SummaryJobPositionPoints");
            Delete.Table("JobPositionPoints");
            Delete.Table("SummaryEmployeePoints");
            Delete.Table("EmployeePoints");
            Delete.Table("SummaryCalculationPoints");
            Delete.Table("CalculationPoints");
            Delete.Table("Calculations_Exceptions");
            Delete.Table("Calculations");
            Delete.Table("Policies_RE_Rules");
            Delete.Table("Policies_RE_RuleFunctions");
            Delete.Table("RulesTrail");
            Delete.Table("Rules");
            Delete.Table("RulesBase");
            Delete.Table("RuleFunctionsTrail");
            Delete.Table("RuleFunctions");
            Delete.Table("RuleFunctionsBase");
            Delete.Table("REConfigItems");

            Delete.Table("Periods_Jobs_JobIndices");

            Delete.Table("Periods_JobIndices_CustomFields");
            Delete.Table("Periods_JobIndices");
            Delete.Table("Periods_JobIndexGroups");
            Delete.Table("Periods_AbstractJobIndices");
            //---------------
            Delete.Table("Employees_JobCustomField_Values");
            Delete.Table("Employees_JobPositions");
            Delete.Table("Periods_Jobs_CustomFields");

            Delete.Table("Periods_JobPositions");
            Delete.Table("Periods_Jobs");




            Delete.Table("Periods_Units_UnitIndices");
            Delete.Table("Periods_UnitIndices_CustomFields");
            Delete.Table("Periods_UnitIndices");
            Delete.Table("Periods_UnitIndexGroups");
            Delete.Table("Periods_AbstractUnitIndices");
            Delete.Table("Periods_Units_CustomFields");
            
            
            
            
            Delete.Table("Periods_Units");

            Delete.Table("Employees_CustomFields");
            Delete.Table("Employees");

            Delete.Table("Periods");

            Delete.Table("JobIndices_CustomFields");
            Delete.Table("JobIndices");
            Delete.Table("JobIndexCategories");
            Delete.Table("AbstractJobIndices");
            
            Delete.Table("Units_CustomFields");


            Delete.Table("UnitIndices_CustomFields");
            Delete.Table("UnitIndices");
            Delete.Table("UnitIndexCategories");
            Delete.Table("AbstractUnitIndices");

            Delete.Table("Jobs_CustomFields");

            Delete.Table("Jobs");

            Delete.Table("Units");
                     
            Delete.Table("Jobpositions");

            Delete.Table("Policies_RE");
            Delete.Table("Policies");
            
            Delete.Table("CustomFieldTypes");  
            

            Delete.Table("ExceptionLogs");
            Delete.Table("EventLogs");
            Delete.Table("Logs");

            Delete.Table("Users_Groups");
            Delete.Table("Parties_CustomActions");
            Delete.Table("Users_WorkListUsers");
            Delete.Table("Users");
            Delete.Table("Groups");
            Delete.Table("Parties");
            Delete.Table("ActionTypes");
        


            Execute.Sql("Drop Sequence [dbo].[PeriodSeq] ");
            Execute.Sql("Drop Sequence [dbo].[PeriodJobCustomFieldsSeq] ");
            Execute.Sql("Drop Sequence [dbo].[PeriodJobsSeq] ");
            Execute.Sql("Drop Sequence [dbo].[Jobseq] ");
            Execute.Sql("Drop Sequence [dbo].[CustomFieldTypeSeq] ");
            Execute.Sql("Drop Sequence [dbo].[PolicySeq] ");
            Execute.Sql("Drop Sequence [dbo].[UnitSeq] ");
            Execute.Sql("Drop sequence [dbo].[JobPositionSeq]");
            Execute.Sql("Drop sequence [dbo].[RuleSeq]");
            Execute.Sql("Drop sequence [dbo].[RuleFunctionSeq]");
            Execute.Sql("Drop sequence [dbo].[REConfigItemSeq]");
            Execute.Sql("Drop sequence [dbo].[EmployeeSeq]");
            Execute.Sql("Drop sequence [dbo].[CalculationSeq]");
            Execute.Sql("Drop sequence [dbo].[AbstractJobIndexSeq]");
            Execute.Sql("Drop sequence [dbo].[AbstractUnitIndexSeq]");
            Execute.Sql("Drop sequence [dbo].[Periods_AbstractJobIndexSeq]");
            Execute.Sql("Drop sequence [dbo].[JobIndexPointSeq]");
            Execute.Sql("Drop sequence [dbo].[Inquiry_JobIndexPointsSeq]");
            Execute.Sql("Drop sequence [dbo].[Periods_ClaimsSeq]");
            Execute.Sql("Drop sequence [dbo].[Calculations_ExceptionsSeq]");




        }




        private void createInquiryTable()
        {
            Create.Table("JobPostion_InquiryConfigurationItems")
                .WithColumn("Id").AsInt64().PrimaryKey()

                .WithColumn("RowVersion").AsCustom("rowversion")

                .WithColumn("InquirerId").AsInt64().NotNullable()
                .ForeignKey("Employees", "Id")

                .WithColumn("InquirerEmployeeNo").AsString()

                .WithColumn("PeriodInquirerJobPositionId").AsInt64().Nullable()
                .ForeignKey("Periods_JobPositions", "Id")

                .WithColumn("InquirerJobPositionId").AsInt64().Nullable()
                .ForeignKey("JobPositions", "Id")

                .WithColumn("PeriodId").AsInt64().NotNullable()
                .ForeignKey("Periods", "Id")

                .WithColumn("InquirySubjectId").AsInt64().NotNullable()
                .ForeignKey("Employees", "Id")

                .WithColumn("SubjectEmployeeNo").AsString()

                .WithColumn("PeriodInquirySubjectJobPositionId").AsInt64().Nullable()
                .ForeignKey("Periods_JobPositions", "Id")

                .WithColumn("InquirySubjectJobPositionId").AsInt64().NotNullable()
                .ForeignKey("JobPositions", "Id")

                .WithColumn("IsAutoGenerated").AsBoolean().NotNullable()
                .WithColumn("IsPermitted").AsBoolean().NotNullable()
                .WithColumn("InquirerJobPositionLevel").AsInt32().NotNullable();

            Create.Table("Inquiry_JobIndexPoints")
                .WithColumn("Id").AsInt64().PrimaryKey()

                .WithColumn("RowVersion").AsCustom("rowversion")

                .WithColumn("PeriodJobIndexId").AsInt64().NotNullable()
                .ForeignKey("Periods_JobIndices", "Id")

                .WithColumn("ConfigurationItemId").AsInt64().NotNullable()
                .ForeignKey("JobPostion_InquiryConfigurationItems", "Id")

                .WithColumn("InquirerId").AsInt64().NotNullable()
                .ForeignKey("Employees", "Id")

                .WithColumn("InquirerEmployeeNo").AsString()

                .WithColumn("PeriodInquirerJobPositionId").AsInt64().Nullable()
                .ForeignKey("Periods_JobPositions", "Id")

                .WithColumn("InquirerJobPositionId").AsInt64().Nullable()
                .ForeignKey("JobPositions", "Id")

                .WithColumn("PeriodId").AsInt64().NotNullable()
                .ForeignKey("Periods", "Id")

                .WithColumn("InquirySubjectId").AsInt64().NotNullable()
                .ForeignKey("Employees", "Id")

                .WithColumn("SubjectEmployeeNo").AsString()

                .WithColumn("PeriodInquirySubjectJobPositionId").AsInt64().Nullable()
                .ForeignKey("Periods_JobPositions", "Id")

                .WithColumn("InquirySubjectJobPositionId").AsInt64().NotNullable()
                .ForeignKey("JobPositions", "Id")

                .WithColumn("JobIndexValue").AsString().Nullable();


        }

        private void createJobIndexPointTable()
        {
            Create.Table("CalculationPoints")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("PeriodId").AsInt64().ForeignKey("Periods", "Id").NotNullable()
                .WithColumn("CalculationId").AsInt64().ForeignKey("Calculations", "Id").NotNullable()
                .WithColumn("Value").AsDecimal().NotNullable()
                .WithColumn("Name").AsString(255)
                .WithColumn("IsFinal").AsBoolean().NotNullable();

            Create.Table("SummaryCalculationPoints")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("CalculationPoints", "Id");

            Create.Table("EmployeePoints")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("CalculationPoints", "Id")
                .WithColumn("PeriodId").AsInt64().ForeignKey("Periods", "Id").NotNullable()
                .WithColumn("EmployeeId").AsInt64().ForeignKey("Employees", "Id").NotNullable()
                .WithColumn("EmployeeNo").AsString(255).NotNullable();
            Create.ForeignKey().FromTable("EmployeePoints").ForeignColumns(new string[2] { "EmployeeNo", "PeriodId" })
                .ToTable("Employees").PrimaryColumns(new string[2] { "EmployeeNo", "PeriodId" });

            Create.Table("SummaryEmployeePoints")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("EmployeePoints", "Id");

            Create.Table("JobPositionPoints")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("EmployeePoints", "Id")
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("JobPositionId").AsInt64().ForeignKey("Periods_JobPositions", "Id").NotNullable()
                .WithColumn("PeriodId").AsInt64().NotNullable()
                .WithColumn("SharedJobPositionId").AsInt64().NotNullable();
            Create.ForeignKey().FromTable("JobPositionPoints").ForeignColumns(new string[2] { "PeriodId", "SharedJobPositionId" })
                .ToTable("Periods_JobPositions").PrimaryColumns(new string[2] { "PeriodId", "JobPositionId" });

            Create.Table("SummaryJobPositionPoints")
                  .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("JobPositionPoints", "Id");

            Create.Table("JobIndexPoints")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("JobPositionPoints", "Id")
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("JobIndexId").AsInt64().ForeignKey("Periods_JobIndices", "Id").NotNullable();
        }

        private void createClaimTable()
        {
            Create.Table("Periods_Claims")
               .WithColumn("Id").AsInt64().PrimaryKey()
               .WithColumn("RowVersion").AsCustom("rowversion")
               .WithColumn("PeriodId").AsInt64().NotNullable().ForeignKey("Periods_Claims_PeriodId", "Periods", "Id")
               .WithColumn("EmployeeNo").AsString(255).NotNullable()
               .WithColumn("Title").AsString(128).Nullable()
               .WithColumn("ClaimDate").AsDateTime().NotNullable()
               .WithColumn("ResponseDate").AsDateTime().Nullable()
               .WithColumn("ClaimStateId").AsInt32().NotNullable()
               .WithColumn("ClaimTypeId").AsInt32().NotNullable()
               .WithColumn("Request").AsString(512).Nullable()
               .WithColumn("Response").AsString(512).Nullable();

            Create.ForeignKey().FromTable("Periods_Claims").ForeignColumns(new string[2] { "EmployeeNo", "PeriodId" })
                .ToTable("Employees").PrimaryColumns(new string[2] { "EmployeeNo", "PeriodId" });

        }

        private void createLogTable()
        {
            Create.Table("Logs")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("Guid").AsGuid().Unique()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("Code").AsString(100).NotNullable()
                  .WithColumn("LogLevelId").AsInt32().NotNullable()
                  .WithColumn("UserId").AsInt64().Nullable().ForeignKey("Users", "Id")
                  .WithColumn("UserName").AsString(100).Nullable()
                  .WithColumn("ClassName").AsString(200).Nullable()
                  .WithColumn("MethodName").AsString(200).Nullable()
                  .WithColumn("LogDate").AsDateTime().NotNullable()
                  .WithColumn("Title").AsString(200).NotNullable()
                  .WithColumn("Messages").AsString(4000).Nullable();

            Create.Table("ExceptionLogs")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("Logs", "Id");

            Create.Table("EventLogs")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("Logs", "Id");
        }


        private void createPeriod_JobIndex()
        {

            Create.Table("Periods_AbstractJobIndices")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("PeriodId").AsInt64().NotNullable();


            Create.Table("Periods_JobIndexGroups")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("fk_Period_AbstractJobIndices_JobIndexGroups_Id", "Periods_AbstractJobIndices", "Id")

                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(512).NotNullable()
                  .WithColumn("ParentId").AsInt64().Nullable()
                  .ForeignKey("fk_Periods_JobIndexGroups_JobIndexGroups_ParentId", "Periods_JobIndexGroups", "Id");

            Create.Table("Periods_JobIndices")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("fk_Periods_AbstractJobIndices_Periods_JobIndices_Id", "Periods_AbstractJobIndices", "Id")

                  .WithColumn("JobIndexId").AsInt64().NotNullable()
                  .ForeignKey("fk_JobIndices_Periods_JobIndices_JobIndexId", "JobIndices", "Id")
                  .WithColumn("GroupId").AsInt64().NotNullable()
                  .ForeignKey("fk_Periods_JobIndexGroups_Periods_JobIndices_GroupId", "Periods_JobIndexGroups", "Id")
                  .WithColumn("IsInquireable").AsBoolean().NotNullable()
                  .WithColumn("CalculationLevel").AsInt64().Nullable()
                  .WithColumn("CalculationOrder").AsInt32().Nullable()
                  .WithColumn("RefJobIndexId").AsInt64().Nullable()
                  .ForeignKey("Periods_JobIndices", "Id");

            Create.Table("Periods_JobIndices_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("PeriodJobIndexId").AsInt64().NotNullable()
                  .ForeignKey("fk_Periods_JobIndices_CustomFields_JobIndexId", "Periods_JobIndices", "Id")
                  .WithColumn("CustomFieldTypeId").AsInt64().NotNullable()
                  .ForeignKey("fk_JPeriods_JobIndices_CustomFields_CustomFieldTypeId", "CustomFieldTypes", "Id")
                  .WithColumn("CustomFieldValue").AsString();

        }

        private void createPeriodJob_JobIndices()
        {
            Create.Table("Periods_Jobs_JobIndices")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()

                .WithColumn("RowVersion").AsCustom("rowversion")

                .WithColumn("PeriodJobId").AsInt64().NotNullable()
                .ForeignKey("fk_Periods_Jobs_JobIndices_PeriodJobId", "Periods_Jobs", "Id")

                .WithColumn("PeriodJobIndexId").AsInt64().NotNullable()
                .ForeignKey("fk_Periods_Jobs_JobIndices_PeriodJobIndexId", "Periods_JobIndices", "Id")
                .WithColumn("ShowforTopLevel").AsBoolean()
                .WithColumn("ShowforSameLevel").AsBoolean()
                .WithColumn("ShowforLowLevel").AsBoolean();



        }

        private void createPeriods_JobPositionssTable()
        {
            Create.Table("Periods_JobPositions")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("PeriodId").AsInt64().NotNullable().ForeignKey("Periods_JobPositions_PeriodId", "Periods", "Id")
                .WithColumn("JobPositionId").AsInt64().NotNullable().ForeignKey("Periods_JobPositions_JobPositionId", "JobPositions", "Id")
                .WithColumn("ParentId").AsInt64().Nullable().ForeignKey("JobPositions_JobPositions_ParentId", "Periods_JobPositions", "Id")
                .WithColumn("PeriodUnitId").AsInt64().NotNullable().ForeignKey("JobPosition_Periods_Units_PeriodUnitId", "Periods_Units", "Id")
                .WithColumn("UnitId").AsInt64().NotNullable()
                .WithColumn("PeriodJobId").AsInt64().NotNullable().ForeignKey("JobPosition_Periods_Jobs_PeriodJobsId", "Periods_Jobs", "Id")
                .WithColumn("JobId").AsInt64().NotNullable();

            Create.UniqueConstraint().OnTable("Periods_JobPositions").Columns(new[] { "PeriodId", "JobPositionId" });
        }

        private void createRuleFunctionsTable()
        {
            Create.Table("RuleFunctions")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("RuleFunctionsBase", "Id");
        }

        private void createRuleFunctionsTrailTable()
        {
            Create.Table("RuleFunctionsTrail")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("RuleFunctionsBase", "Id")
                .WithColumn("RuleFunctionId").AsInt64().ForeignKey("RuleFunctions", "Id")
                .WithColumn("EffectiveDate").AsDateTime().NotNullable();
        }

        private void createCalculationsTable()
        {
            Create.Table("Calculations")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("State").AsInt32().NotNullable()
                .WithColumn("Name").AsString(100).NotNullable().Unique("idx_Name_Unique")
                .WithColumn("PolicyId").AsInt64().NotNullable().ForeignKey("fk_Policies_Calculations_PolicyId", "Policies", "Id")
                .WithColumn("PeriodId").AsInt64().NotNullable().ForeignKey("fk_Periods_Calculations_PeriodId", "Periods", "Id")
                .WithColumn("IsDeterministic").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("StartRunTime").AsDateTime().Nullable()
                .WithColumn("EndRunTime").AsDateTime().Nullable()
                .WithColumn("LibraryText").AsCustom("ntext").Nullable()
                .WithColumn("Rules").AsCustom("ntext").Nullable()
                .WithColumn("TotalEmployeesCount").AsInt64().Nullable()
                .WithColumn("EmployeesCalculatedCount").AsInt64().Nullable()
                .WithColumn("Messages").AsCustom("ntext").Nullable()
                .WithColumn("employeeIdDelimetedList").AsCustom("ntext").NotNullable()
                .WithColumn("LastCalculatedPath").AsInt32().Nullable()
                .WithColumn("LastCalculatedEmployeeId").AsInt64().Nullable().ForeignKey("Employees", "Id")
                .WithColumn("LastCalculatedEmployeeNo").AsString(255).Nullable()
                .WithColumn("LastCalculatedEmployeePeriodId").AsInt64().Nullable().ForeignKey("Periods", "Id");


            Create.Table("Calculations_Exceptions")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("CalculationId").AsInt64().Nullable().ForeignKey("Calculations", "Id")
                  .WithColumn("EmployeeId").AsInt64().Nullable().ForeignKey("Employees", "Id")
                  .WithColumn("EmployeeNo").AsString(200)
                  .WithColumn("PeriodId").AsInt64().Nullable()
                  .WithColumn("CalculationPathNo").AsInt32()
                  .WithColumn("Message").AsString(1024);

        }

        private void createPolicies_RE_RFTable()
        {
            Create.Table("Policies_RE_RuleFunctions")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("PolicyId").AsInt64().NotNullable().ForeignKey("fk_Policies_RE_RuleFunctions_Policies_RE_Id", "Policies_RE", "Id")
                .WithColumn("RuleFunctionId").AsInt64().NotNullable().ForeignKey("fk_Policies_RE_RuleFunctions_RuleFunctions_Id", "RuleFunctions", "Id");
        }

        private void createPolicies_RE_RulesTable()
        {
            Create.Table("Policies_RE_Rules")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("RowVersion").AsCustom("rowversion")

                .WithColumn("PolicyId").AsInt64().NotNullable().ForeignKey("fk_Policies_RE_Rules_Policies_RE_Id", "Policies_RE", "Id")
                .WithColumn("RuleId").AsInt64().NotNullable().ForeignKey("fk_Policies_RE_Rules_Rules_Id", "Rules", "Id");
        }

        private void createPolicies_RETable()
        {
            Create.Table("Policies_RE")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("fk_Policies_PoliciesRE_Id", "Policies", "Id");
        }

        private void createJobIndexTable()
        {
            Create.Table("AbstractJobIndices")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(256).NotNullable();

            Create.Table("JobIndexCategories")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("fk_AbstractJobIndices_JobIndexCategories_Id", "AbstractJobIndices", "Id")

                  .WithColumn("ParentId").AsInt64().Nullable()
                  .ForeignKey("fk_JobIndexCategories_JobIndexCategories_ParentId", "JobIndexCategories", "Id");

            Create.Table("JobIndices")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("fk_AbstractJobIndices_JobIndices_Id", "AbstractJobIndices", "Id")

                  .WithColumn("CategoryId").AsInt64().NotNullable()
                  .ForeignKey("fk_JobIndexCategories_JobIndices_CategoryId", "JobIndexCategories", "Id");

            Create.Table("JobIndices_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("RowVersion").AsCustom("rowversion")

                  .WithColumn("JobIndexId").AsInt64().NotNullable()
                  .ForeignKey("fk_JobIndices_CustomFields_JobIndexId", "JobIndices", "Id")

                  .WithColumn("CustomFieldTypeId").AsInt64().NotNullable()
                  .ForeignKey("fk_JobIndices_CustomFields_CustomFieldTypeId", "CustomFieldTypes", "Id");
        }

        private void createUnitIndexTable()
        {
            Create.Table("AbstractUnitIndices")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(256).NotNullable();

            Create.Table("UnitIndexCategories")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("fk_AbstractUnitIndices_UnitIndexCategories_Id", "AbstractUnitIndices", "Id")

                  .WithColumn("ParentId").AsInt64().Nullable()
                  .ForeignKey("fk_UnitIndexCategories_UnitIndexCategories_ParentId", "UnitIndexCategories", "Id");

            Create.Table("UnitIndices")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .ForeignKey("fk_AbstractUnitIndices_UnitIndices_Id", "AbstractUnitIndices", "Id")

                  .WithColumn("CategoryId").AsInt64().NotNullable()
                  .ForeignKey("fk_UnitIndexCategories_UnitIndices_CategoryId", "UnitIndexCategories", "Id");

            Create.Table("UnitIndices_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("RowVersion").AsCustom("rowversion")

                  .WithColumn("UnitIndexId").AsInt64().NotNullable()
                  .ForeignKey("fk_UnitIndices_CustomFields_UnitIndexId", "UnitIndices", "Id")

                  .WithColumn("CustomFieldTypeId").AsInt64().NotNullable()
                  .ForeignKey("fk_UnitIndices_CustomFields_CustomFieldTypeId", "CustomFieldTypes", "Id");
        }

        private void createEmployees()
        {
            Create.Table("Employees")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("EmployeeNo").AsString(255).NotNullable()
                .WithColumn("PeriodId").AsInt64().ForeignKey("Periods", "Id").NotNullable()
                .WithColumn("FirstName").AsString(512).NotNullable()
                .WithColumn("LastName").AsString(512).NotNullable();

            Create.UniqueConstraint("Idx_Unique_EmployeeNo_PeriodId").OnTable("Employees").Columns(new string[2] { "EmployeeNo", "PeriodId" });

            Create.Table("Employees_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("EmployeeId").AsInt64().NotNullable()
                  .ForeignKey("Employees", "Id")
                  .WithColumn("CustomFieldTypeId").AsInt64().NotNullable()
                  .ForeignKey("CustomFieldTypes", "Id")
                  .WithColumn("CustomFieldValue").AsString().Nullable();

            Create.Table("Employees_JobPositions")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("RowVersion").AsCustom("rowversion")

                .WithColumn("EmployeeId").AsInt64().Nullable()
                .ForeignKey("Employees", "Id")

                .WithColumn("EmployeeNo").AsString().NotNullable()

                .WithColumn("PeriodJobPositionId").AsInt64()
                .ForeignKey("Periods_JobPositions", "Id")

                .WithColumn("JobPositionId").AsInt64().NotNullable()
                .ForeignKey("JobPositions", "Id")

                .WithColumn("PeriodId").AsInt64().NotNullable()
                .ForeignKey("Periods", "Id")

                .WithColumn("FromDate").AsDateTime()
                .WithColumn("ToDate").AsDateTime()
                .WithColumn("WorkTimePercent").AsInt32()
                .WithColumn("JobPositionWeight").AsInt32();


            Create.Table("Employees_JobCustomField_Values")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("RowVersion").AsCustom("rowversion")

                .WithColumn("EmployeeJobPositionId").AsInt64().Nullable()
                .ForeignKey("Employees_JobPositions", "Id")

                .WithColumn("PeriodJobCustomFieldTypeId").AsInt64()
                .ForeignKey("Periods_Jobs_CustomFields", "Id")

                .WithColumn("PeriodId").AsInt64().NotNullable()
                .ForeignKey("Periods", "Id")

                .WithColumn("JobId").AsInt64().NotNullable()
                .ForeignKey("Jobs", "Id")

                .WithColumn("CustomFieldId").AsInt64().NotNullable()
                .ForeignKey("CustomFieldTypes", "Id")

                .WithColumn("CustomFieldValue").AsString();


        }

        private void createRulesBaseTable()
        {
            Create.Table("RulesBase")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("RuleText").AsCustom("ntext")
                .WithColumn("RuleType").AsInt32().NotNullable()
                .WithColumn("ExecuteOrder").AsInt32().NotNullable();
        }

        private void createRulesTable()
        {
            Create.Table("Rules")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("RulesBase", "Id");
        }

        private void createRulesTrailTable()
        {
            Create.Table("RulesTrail")
                .WithColumn("Id").AsInt64().PrimaryKey().ForeignKey("RulesBase", "Id")
                .WithColumn("RuleId").AsInt64().NotNullable().ForeignKey("Rules", "Id")
                .WithColumn("EffectiveDate").AsDateTime().NotNullable();
        }

        private void createREConfigItemsTable()
        {
            Create.Table("REConfigItems")
                .WithColumn("DbId").AsInt64().PrimaryKey()
                .WithColumn("RowVersion").AsCustom("rowversion")
                .WithColumn("Name").AsString(100).Unique("idx_Name_Unique").NotNullable().Unique()
                .WithColumn("Value").AsString(1024);
        }

        private void createPeriods_UnitsTable()
        {
            Create.Table("Periods_Units")
                 .WithColumn("Id").AsInt64().PrimaryKey()
                 .WithColumn("RowVersion").AsCustom("rowversion")
                 .WithColumn("PeriodId").AsInt64().NotNullable().ForeignKey("Periods_units_PeriodId", "Periods", "Id")
                .WithColumn("UnitId").AsInt64().NotNullable().ForeignKey("Periods_units_UnitId", "Units", "Id")
                .WithColumn("ParentId").AsInt64().Nullable().ForeignKey("Units_Units_ParentId", "Periods_Units", "Id");

            Create.UniqueConstraint("uk_PeriodId_UnitId").OnTable("Periods_Units").Columns(new[] { "PeriodId", "UnitId" });
        }

        private void createSequence()
        {

            Execute.Sql(
                "create sequence [dbo].[PeriodJobsSeq] as [bigint] start with 10 increment by 1 minvalue -9223372036854775808 maxValue 9223372036854775807  cache");
            Execute.Sql(
                "create sequence [dbo].[UnitSeq] as [bigint] start with 10 increment by 1 minvalue -9223372036854775808 maxValue 9223372036854775807  cache");
            Execute.Sql(
                "create sequence [dbo].[JobPositionSeq] as [bigint] start with 10 increment by 1 minvalue -9223372036854775808 maxValue 9223372036854775807  cache");
            Execute.Sql(
                "create sequence [dbo].[Jobseq] as [bigint] start with 10 increment by 1 minvalue -9223372036854775808 maxValue 9223372036854775807  cache");
            Execute.Sql(
                "create sequence [dbo].[CustomFieldTypeSeq] as [bigint] start with 10 increment by 1 minvalue -9223372036854775808 maxValue 9223372036854775807  cache");
            Execute.Sql(
                "create sequence [dbo].[Inquiry_JobIndexPointsSeq] as [bigint] start with 10 increment by 1 minvalue -9223372036854775808 maxValue 9223372036854775807  cache");
            Execute.Sql(@"
CREATE SEQUENCE [dbo].[PeriodSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");

            Execute.Sql(@"
CREATE SEQUENCE [dbo].[PolicySeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");

            Execute.Sql(@"
CREATE SEQUENCE [dbo].[CalculationSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");
            Execute.Sql(@"
CREATE SEQUENCE [dbo].[EmployeeSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");
            Execute.Sql(@"
CREATE SEQUENCE [dbo].[RuleSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");
            Execute.Sql(@"
CREATE SEQUENCE [dbo].[RuleFunctionSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");
            Execute.Sql(@"
CREATE SEQUENCE [dbo].[REConfigItemSeq] 
 AS [bigint]
 START WITH 3
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");

            Execute.Sql(@"
CREATE SEQUENCE [dbo].[PeriodJobCustomFieldsSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");

            Execute.Sql(@"
CREATE SEQUENCE [dbo].[AbstractJobIndexSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");

            Execute.Sql(@"
CREATE SEQUENCE [dbo].[AbstractUnitIndexSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");


            Execute.Sql(@"
CREATE SEQUENCE [dbo].[Periods_AbstractJobIndexSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");



            Execute.Sql(@"
CREATE SEQUENCE [dbo].[JobIndexPointSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");


            Execute.Sql(@"
CREATE SEQUENCE [dbo].[Periods_ClaimsSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");



            Execute.Sql(@"
CREATE SEQUENCE [dbo].[Calculations_ExceptionsSeq] 
 AS [bigint]
 START WITH 10
 INCREMENT BY 1
 MINVALUE -9223372036854775808
 MAXVALUE 9223372036854775807
 CACHE ");


        }

        private void createPeriods_JobsTable()
        {
            Create.Table("Periods_Jobs")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")

                  .WithColumn("PeriodId").AsInt64().NotNullable()
                  .WithColumn("JobId").AsInt64().NotNullable();

            Create.ForeignKey("Periods_jobs_JobId")
               .FromTable("Periods_Jobs").ForeignColumn("JobId")
               .ToTable("Jobs").PrimaryColumn("Id");

            Create.ForeignKey("Periods_jobs_PeriodId")
              .FromTable("Periods_Jobs").ForeignColumn("PeriodId")
              .ToTable("Periods").PrimaryColumn("Id");
        }

        private void createPeriods_Jobs_CustomFieldsTable()
        {
            Create.Table("Periods_Jobs_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")

                  .WithColumn("PeriodJobId").AsInt64().Nullable()
                  .ForeignKey("Periods_Jobs", "Id")

                  .WithColumn("CustomFieldId").AsInt64().NotNullable()
                  .ForeignKey("CustomFieldTypes", "Id")

                  .WithColumn("PeriodId").AsInt64().NotNullable()
                  .ForeignKey("Periods", "Id")

                  .WithColumn("JobId").AsInt64().NotNullable()
                  .ForeignKey("Jobs", "Id");

        }

        private void createPeriodsTable()
        {
            Create.Table("Periods")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                   .WithColumn("State").AsInt32().NotNullable()
                  .WithColumn("Name").AsString(256).NotNullable()
                .WithColumn("StartDate").AsDateTime().Nullable()
                .WithColumn("EndDate").AsDateTime().Nullable()
                .WithColumn("Active").AsBoolean().NotNullable();
        }

        private void createJobs_CustomFieldsTable()
        {
            Create.Table("Jobs_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("RowVersion").AsCustom("rowversion")

                  .WithColumn("JobId").AsInt64().NotNullable()
                  .WithColumn("CustomFieldTypeId").AsInt64().NotNullable();

            Create.ForeignKey("Jobs_CustomFields_JobId")
                  .FromTable("Jobs_CustomFields").ForeignColumn("JobId")
                  .ToTable("Jobs").PrimaryColumn("Id");

            Create.ForeignKey("Jobs_CustomFields_CustomFieldId")
                  .FromTable("Jobs_CustomFields").ForeignColumn("CustomFieldTypeId")
                  .ToTable("CustomFieldTypes").PrimaryColumn("Id");
        }

        private void createUnits_CustomFieldsTable()
        {
            Create.Table("Units_CustomFields")
                  .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                  .WithColumn("RowVersion").AsCustom("rowversion")

                  .WithColumn("UnitId").AsInt64().NotNullable()
                  .WithColumn("CustomFieldTypeId").AsInt64().NotNullable();

            Create.ForeignKey("Units_CustomFields_UnitId")
                  .FromTable("Units_CustomFields").ForeignColumn("UnitId")
                  .ToTable("Units").PrimaryColumn("Id");

            Create.ForeignKey("Units_CustomFields_CustomFieldId")
                  .FromTable("Units_CustomFields").ForeignColumn("CustomFieldTypeId")
                  .ToTable("CustomFieldTypes").PrimaryColumn("Id");
        }


        private void createUnitTable()
        {
            Create.Table("Units")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(512).NotNullable().Unique("Units_Unique_DictionaryName")
                ;
        }

        private void createJobPostionTable()
        {
            Create.Table("JobPositions")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(512).NotNullable().Unique("JobPositions_Unique_DictionaryName")
                ;
        }

        private void createJobTable()
        {
            Create.Table("Jobs")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(512).NotNullable()
                ;
            Create.UniqueConstraint("Jobs_Unique_DictionaryName")
                  .OnTable("Jobs").Column("DictionaryName");
        }

        private void createPoliciesTable()
        {
            Create.Table("Policies")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(512).NotNullable()
                ;
            Create.UniqueConstraint("Policies_Unique_DictionaryName")
                  .OnTable("Policies").Column("DictionaryName");
        }

        private void createRuleFunctionsBaseTable()
        {
            Create.Table("RuleFunctionsBase")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("FunctionsText").AsCustom("ntext").NotNullable();
        }

        private void createCustomFieldTypesTable()
        {
            Create.Table("CustomFieldTypes")
                  .WithColumn("Id").AsInt64().PrimaryKey()
                  .WithColumn("RowVersion").AsCustom("rowversion")
                  .WithColumn("EntityTypeId").AsInt64().NotNullable()
                  .WithColumn("Name").AsString(256).NotNullable()
                  .WithColumn("DictionaryName").AsString(512).NotNullable()
                  .WithColumn("MaxValue").AsDouble().Nullable()
                  .WithColumn("MinValue").AsDouble().Nullable()
                  .WithColumn("TypeId").AsString(50).NotNullable().WithDefaultValue("Int");
            Create.UniqueConstraint("CustomFieldTypes_Unique_DictionaryName")
                  .OnTable("CustomFieldTypes").Column("DictionaryName");
        }

        private void createNH_HiloTable()
        {
            Create.Table("NH_Hilo")
                  .WithColumn("TableKey")
                  .AsString(100)
                  .NotNullable()
                  .WithColumn("NextHi")
                  .AsInt64()
                  .NotNullable();
        }

        private void createPMSActionsTable()
        {
            Create.Table("ActionTypes")
                .WithColumn("Id")
                .AsInt32()
                .NotNullable().PrimaryKey()
                .WithColumn("Name")
                .AsString(512).Unique()
                .NotNullable();
        }


        private void createUsersTable()
        {
            Create.Table("Parties")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("PartyName").AsString(100).NotNullable().Unique()
                .WithColumn("RowVersion").AsCustom("rowversion");

            Create.Table("Users")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .ForeignKey("Parties", "Id")
                .WithColumn("FirstName").AsString(100).Nullable()
                .WithColumn("LastName").AsString(100).Nullable()
                .WithColumn("Email").AsString(100).Nullable()
                .WithColumn("Active").AsBoolean().NotNullable();

            Create.Table("Groups")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .ForeignKey("Parties", "Id")
                .WithColumn("Description").AsString(256).Nullable();

            Create.Table("Users_Groups")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("UserId").AsInt64().NotNullable()
                .ForeignKey("Users", "Id")
                .WithColumn("GroupId").AsInt64().NotNullable()
                .ForeignKey("Groups", "Id");


            Create.Table("Users_WorkListUsers")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("Users", "Id")
                .WithColumn("WorkListUserId").AsInt64().NotNullable().ForeignKey("Users", "Id");

            Create.Table("Parties_CustomActions")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()

                .WithColumn("PartyId").AsInt64().NotNullable()
                .ForeignKey("Parties", "Id")

                .WithColumn("ActionTypeId").AsInt32().NotNullable()
                .ForeignKey("ActionTypes", "Id")

                .WithColumn("IsGrant").AsBoolean().NotNullable();
        }


    }
}
