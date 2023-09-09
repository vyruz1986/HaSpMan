using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class IncreaseAddressHouseNumberLength : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Address_HouseNumber",
            schema: "HaSpMan",
            table: "Members",
            type: "varchar(15)",
            maxLength: 15,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(5)",
            oldMaxLength: 5);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Address_HouseNumber",
            schema: "HaSpMan",
            table: "Members",
            type: "varchar(5)",
            maxLength: 5,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(15)",
            oldMaxLength: 15);
    }
}
