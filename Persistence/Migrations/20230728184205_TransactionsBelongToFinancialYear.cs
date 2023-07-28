using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class TransactionsBelongToFinancialYear : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("INSERT INTO " +
                             "HaspMan.FinancialYears (Id, StartDate, EndDate, IsClosed) " +
                             "VALUES (newId(), CAST('2022-09-01 00:00:00.0000000 +01:00' AS DATETIMEOFFSET),CAST('2023-08-31 00:00:00.0000000 +01:00' AS DATETIMEOFFSET), 0)");

        migrationBuilder.DropIndex("IX_Transactions_FinancialYearId", schema: "HaspMan", table: "Transactions");
        migrationBuilder.AlterColumn<Guid>(
            "FinancialYearId", 
            schema:"HaspMan",
            table: "Transactions",
            nullable: false);
        migrationBuilder.CreateIndex(
            name: "IX_Transactions_FinancialYearId",
            schema: "HaSpMan",
            table: "Transactions",
            column: "FinancialYearId");

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}