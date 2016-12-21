using System;
using FluentMigrator;
using MITD.Core;
using MITD.Domain.Model;
using MITD.PMSSecurity.Domain;

namespace MITD.PMS.Persistence
{
    [Migration(3)]
    public class MigrationVer3 : Migration
    {
        public override void Up()
        {


            #region ActionTypes
            /* version 3 
            Insert.IntoTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ModifyJobPositionInPeriod,
                Name = ActionType.ChangeEmployeePoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });
           */
            // version 3 
            Alter.Table("Users").AddColumn("EmailStatus").AsInt32().NotNullable().WithDefaultValue(0);
            Alter.Table("Users").AddColumn("VerificationCode").AsString(50).Nullable();


            #endregion

        }

        public override void Down()
        {
            #region ActionTypes
            /*
             
             Delete.FromTable("ActionTypes").Row(new
            {
                Id = (int)ActionType.ModifyJobPositionInPeriod,
                Name = ActionType.ChangeEmployeePoint.GetAttribute<ActionInfoAttribute>().DisplayName,

            });
            */
            #endregion

            Delete.Column("EmailStatus").FromTable("Users");
            Delete.Column("VerificationCode").FromTable("Users");
        }




    }
}
