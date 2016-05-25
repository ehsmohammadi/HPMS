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
            #region ActionTypes
            //Insert.IntoTable("ActionTypes").Row(new
            //{
            //    Id = (int)actiontype,
            //    Name = actiontype.GetAttribute<ActionInfoAttribute>().DisplayName,

            //});

            #endregion

            Alter.Table("Periods").AddColumn("MaxFinalPoint").AsDecimal().NotNullable().WithDefaultValue(0);
            Alter.Table("Employees").AddColumn("PointState").AsInt32().NotNullable().WithDefaultValue(1);

        }

        public override void Down()
        {
            Delete.Column("MaxFinalPoint").FromTable("Periods");
            Delete.Column("PointState").FromTable("Employees");
        }




    }
}
