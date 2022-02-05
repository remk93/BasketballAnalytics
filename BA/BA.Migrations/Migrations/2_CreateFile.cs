using BA.Migrations.Attributes;
using FluentMigrator;

namespace BA.Migrations.Migrations;

[CustomMigration(1, 2, "Create File table")]
[Tags("BA")]
public class CreateFile : Migration
{
    public override void Up()
    {
        Create.Table("File")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(256).NotNullable()
            .WithColumn("Link").AsString(256).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("File");
    }
}