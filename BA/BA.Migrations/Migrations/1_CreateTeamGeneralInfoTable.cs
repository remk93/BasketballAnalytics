using BA.Migrations.Attributes;
using FluentMigrator;

namespace BA.Migrations.Migrations;

[CustomMigration(1, 1, "Create team general information table")]
[Tags("BA")]

public class CreateTeamGeneralInfoTable : Migration
{
    public override void Up()
    {
        Create.Table("Team")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(256).NotNullable()
            .WithColumn("ShortName").AsString(3).NotNullable()
            .WithColumn("Conference").AsByte().NotNullable()
            .WithColumn("Division").AsByte().NotNullable()
            .WithColumn("City").AsString(256).NotNullable()
            .WithColumn("Stadium").AsString(256).NotNullable()
            .WithColumn("Founded").AsInt32().NotNullable();

        //Create.Table("PeopleInTeam")
        //    .WithColumn("PersonId").AsInt32().NotNullable()
        //        .ForeignKey("FK_PeopleInTeam_Person", "Person", "Id")
        //    .WithColumn("TeamId").AsInt32().NotNullable()
        //        .ForeignKey("FK_PeopleInTeam_Team", "Team", "Id")
        //    .WithColumn("Role").AsByte().NotNullable();

        //Create.PrimaryKey("PK_PeopleInTeam").OnTable("PeopleInTeam").Columns("PersonId", "TeamId", "Role");
    }

    public override void Down()
    {
        //Delete.PrimaryKey("PK_PeopleInTeam").FromTable("PeopleInTeam");
        //Delete.Table("PeopleInTeam");

        Delete.Table("Team");
    }
}