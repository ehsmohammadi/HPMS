using System;
using FluentMigrator;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Persistence
{
    [Migration(4)]
    public class MigrationVer4 : Migration
    {
        public override void Up()
        {


            #region ActionTypes
            //version 3 
            //Insert.IntoTable("ActionTypes").Row(new
            //{
            //    Id = (int)ActionType.ManageUnitInPeriodVerifier,
            //    Name = ActionType.ManageUnitInPeriodVerifier.GetAttribute<ActionInfoAttribute>().DisplayName,

            //});

            // version 3 
            Create.Table("PeriodUnits_Verifiers")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()

                .WithColumn("RowVersion").AsCustom("rowversion")

                .WithColumn("PeriodUnitId").AsInt64().NotNullable()
                .ForeignKey("Periods_Units", "Id")

                .WithColumn("PeriodId").AsInt64().NotNullable()
                .ForeignKey("Periods", "Id")

                .WithColumn("EmployeeNo").AsString()

                .WithColumn("EmployeeId").AsInt64().NotNullable()
                .ForeignKey("Employees", "Id");




            #endregion

        }

        public override void Down()
        {
            #region ActionTypes

           // Delete.FromTable("ActionTypes").Row(new
           //{
           //    Id = (int)ActionType.ManageUnitInPeriodVerifier,
           //    Name = ActionType.ManageUnitInPeriodVerifier.GetAttribute<ActionInfoAttribute>().DisplayName,

           //});

            #endregion

            Delete.Table("PeriodUnits_Verifiers");
        }




    }
}
