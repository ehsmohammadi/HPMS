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
            alterPeriodsTable();
            #region ActionTypes
            //Insert.IntoTable("ActionTypes").Row(new
            //{
            //    Id = (int)actiontype,
            //    Name = actiontype.GetAttribute<ActionInfoAttribute>().DisplayName,

            //});

            #endregion

        }

        public override void Down()
        {
            Delete.Column("MaxFinalPoint").FromTable("Periods");
        }


        private void alterPeriodsTable()
        {
            Alter.Table("Periods").AddColumn("MaxFinalPoint").AsDecimal().NotNullable().WithDefaultValue(0);
        }




    }
}
