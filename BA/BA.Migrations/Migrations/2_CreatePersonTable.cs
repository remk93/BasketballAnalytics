using BA.Migrations.Attributes;
using FluentMigrator;

namespace BA.Migrations.Migrations;

[CustomMigration(1, 2, "Create Person table")]
[Tags("BA")]
public class CreatePersonTable : Migration
{
    public override void Up()
    {
        Create.Table("Person")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(128).NotNullable()
            .WithColumn("Surname").AsString(128).NotNullable()
            .WithColumn("Birthday").AsDateTime().NotNullable()
            .WithColumn("Role").AsByte().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Person");
    }
}