using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class AddFinancialYearAsAggregateRoot : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "FinancialYearId",
            schema: "HaSpMan",
            table: "Transactions",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "Locked",
            schema: "HaSpMan",
            table: "Transactions",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateTable(
            name: "FinancialYearConfigurations",
            schema: "HaSpMan",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FinancialYearConfigurations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "FinancialYears",
            schema: "HaSpMan",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                IsClosed = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FinancialYears", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_FinancialYearId",
            schema: "HaSpMan",
            table: "Transactions",
            column: "FinancialYearId");

        migrationBuilder.AddForeignKey(
            name: "FK_Transactions_FinancialYears_FinancialYearId",
            schema: "HaSpMan",
            table: "Transactions",
            column: "FinancialYearId",
            principalSchema: "HaSpMan",
            principalTable: "FinancialYears",
            principalColumn: "Id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Transactions_FinancialYears_FinancialYearId",
            schema: "HaSpMan",
            table: "Transactions");

        migrationBuilder.DropTable(
            name: "FinancialYearConfigurations",
            schema: "HaSpMan");

        migrationBuilder.DropTable(
            name: "FinancialYears",
            schema: "HaSpMan");

        migrationBuilder.DropIndex(
            name: "IX_Transactions_FinancialYearId",
            schema: "HaSpMan",
            table: "Transactions");

        migrationBuilder.DropColumn(
            name: "FinancialYearId",
            schema: "HaSpMan",
            table: "Transactions");

        migrationBuilder.DropColumn(
            name: "Locked",
            schema: "HaSpMan",
            table: "Transactions");
    }
}