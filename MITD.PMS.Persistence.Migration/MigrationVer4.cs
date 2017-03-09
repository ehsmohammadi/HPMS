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
            Insert.IntoTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ManageUnitInPeriodVerifier,
                Name = ActionType.ManageUnitInPeriodVerifier.GetAttribute<ActionInfoAttribute>().DisplayName,

            });

            //version 3 
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


            Alter.Table("Employees").AddColumn("CalculatedPoint").AsDecimal().NotNullable().WithDefaultValue(1);

            #endregion

            //-- ================================================
            //-- Template generated from Template Explorer using:
            //-- Create Procedure (New Menu).SQL
            //--
            //-- Use the Specify Values for Template Parameters 
            //-- command (Ctrl-Shift-M) to fill in the parameter 
            //-- values below.
            //--
            //-- This block of comments will not be included in
            //-- the definition of the procedure.
            //-- ================================================
            //SET ANSI_NULLS ON
            //GO
            //SET QUOTED_IDENTIFIER ON
            //GO
            //-- =============================================
            //-- Author:		Ehsan mohammadi
            //-- Create date: 
            //-- Description:	
            //-- =============================================
            //CREATE PROCEDURE GetAllSubordinatesEmployeeNo 
            //    -- Add the parameters for the stored procedure here
            //    @EmployeeNo nvarchar(255), 
            //    @PeriodId bigint 
            //AS
            //BEGIN
            //    -- SET NOCOUNT ON added to prevent extra result sets from
            //    -- interfering with SELECT statements.
            //    SET NOCOUNT ON;

            //    -- Insert statements for procedure here
            //    WITH    q AS 
            //        (
            //        SELECT  *
            //        FROM    Periods_Units
            //        WHERE   ParentID in (select PeriodUnitId from PeriodUnits_Verifiers where EmployeeNo=@EmployeeNo and PeriodId=@PeriodId) -- this condition defines the ultimate ancestors in your chain, change it as appropriate
            //        UNION ALL
            //        SELECT  m.*
            //        FROM    Periods_Units m
            //        JOIN    q
            //        ON      m.parentID = q.Id
            //        )		
            //select EmployeeNo from Employees_JobPositions where PeriodJobPositionId in 
            //(
            //  select Id from Periods_JobPositions where PeriodUnitId in 
            //  (
            //    select q.Id from q union select PeriodUnitId from PeriodUnits_Verifiers where EmployeeNo=@EmployeeNo and PeriodId=@PeriodId
            //  )
            //)
            //END
            //GO

        }

        public override void Down()
        {
            #region ActionTypes

            Delete.FromTable("ActionTypes").Row(new
           {
               Id = (int)ActionType.ManageUnitInPeriodVerifier,
               Name = ActionType.ManageUnitInPeriodVerifier.GetAttribute<ActionInfoAttribute>().DisplayName,

           });

            #endregion

            Delete.Table("PeriodUnits_Verifiers");
            Delete.Column("CalculatedPoint").FromTable("Employees");
        }




    }
}
