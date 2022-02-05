using BA.Migrations.Attributes;
using FluentMigrator;

namespace BA.Migrations.Migrations;

[CustomMigration(1, 1, "Create Team table")]
[Tags("BA")]

public class CreateTeamGeneralInfoTable : Migration
{
    public override void Up()
    {
        Create.Table("Team")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(256).NotNullable()
            .WithColumn("Code").AsString(3).NotNullable()
            .WithColumn("Conference").AsByte().NotNullable()
            .WithColumn("Division").AsByte().NotNullable()
            .WithColumn("City").AsString(256).NotNullable()
            .WithColumn("Stadium").AsString(256).NotNullable()
            .WithColumn("Founded").AsInt32().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Team");
    }
}