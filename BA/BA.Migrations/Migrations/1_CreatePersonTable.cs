using BA.Migrations.Attributes;
using FluentMigrator;

namespace BA.Migrations.Migrations;

[CustomMigration(1, 1, "Add Person table")]
[Tags("BA")]
public class CreatePersonTable : Migration
{
    public override void Up()
    {
        Create.Table("Person")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(128).NotNullable()
            .WithColumn("Surname").AsString(128).NotNullable()
            .WithColumn("Role").AsByte().NotNullable()
            .WithColumn("Birthday").AsDateTime().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Person");
    }
}