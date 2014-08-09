using FluentMigrator;

namespace MITD.PMSAdmin.Persistence.NH
{
    [Migration(2)]
    public class MigrationVer2:Migration
    {
        public override void Up()
        {
            Alter.Table("CustomFieldTypes")
                  .AddColumn("MinValue").AsInt64().NotNullable()
                  .AddColumn("MaxValue").AsInt64().NotNullable();
        }

        public override void Down()
        {
            Delete.Column("MinValue").Column("MaxValue").FromTable("CustomFieldTypes");
        }
    }
}
