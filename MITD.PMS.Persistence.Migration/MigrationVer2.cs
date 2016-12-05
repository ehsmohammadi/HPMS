using System;
using FluentMigrator;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Persistence
{
    [Migration(2)]
    public class MigrationVer2 : Migration
    {
        public override void Up()
        {
            

            Alter.Table("Periods").AddColumn("MaxFinalPoint").AsDecimal().NotNullable().WithDefaultValue(0);
            Alter.Table("Employees").AddColumn("PointState").AsInt32().NotNullable().WithDefaultValue(1);

            #region ActionTypes
            /*
            Insert.IntoTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ConfirmAboveMaxPoint,
                Name = ActionType.ConfirmAboveMaxPoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });

            Insert.IntoTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ConfirmEmployeePoint,
                Name = ActionType.ConfirmEmployeePoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });

            Insert.IntoTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ChangeEmployeePoint,
                Name = ActionType.ChangeEmployeePoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });

            Insert.IntoTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ModifyJobPositionInPeriod,
                Name = ActionType.ChangeEmployeePoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });
           */

            #endregion

        }

        public override void Down()
        {
            #region ActionTypes
            /*
            Delete.FromTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ConfirmAboveMaxPoint,
                Name = ActionType.ConfirmAboveMaxPoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });

            Delete.FromTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ConfirmEmployeePoint,
                Name = ActionType.ConfirmEmployeePoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });

            Delete.FromTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ChangeEmployeePoint,
                Name = ActionType.ChangeEmployeePoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });
             
             Delete.FromTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ModifyJobPositionInPeriod,
                Name = ActionType.ChangeEmployeePoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });
            */
            #endregion

            Delete.Column("MaxFinalPoint").FromTable("Periods");
            Delete.Column("PointState").FromTable("Employees");
        }




    }
}
