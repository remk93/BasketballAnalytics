using BA.Migrations.Attributes;
using FluentMigrator;

namespace BA.Migrations.Migrations;

[CustomMigration(1, 3, "Add team logo field")]
[Tags("BA")]
public class AddTeamLogoField : Migration
{
    public override void Up()
    {
        Create.Column("LogoId").OnTable("Team").AsInt32().NotNullable()
            .ForeignKey("FK_Team_File", "File", "Id");
    }

    public override void Down()
    {
        Delete.Column("LogoId").FromTable("Team");
    }
}