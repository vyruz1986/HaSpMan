using Domain.Views;

using Microsoft.EntityFrameworkCore.Migrations;

using Persistence.Constants;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class AddBankAccountTotalsView : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"CREATE VIEW {Schema.HaSpMan}.{BankAccountTotals.ViewName}
                                        WITH SCHEMABINDING
                                        AS
                                            SELECT
                                                SUM(t.Amount) AS Total,
                                                t.BankAccountId,
                                                COUNT_BIG(*) AS NumberOfTransactions
                                            FROM
                                                {Schema.HaSpMan}.Transactions t
                                                GROUP BY t.BankAccountId");

        migrationBuilder.Sql($@"CREATE UNIQUE CLUSTERED INDEX IX_{BankAccountTotals.ViewName}
	                                    ON {Schema.HaSpMan}.{BankAccountTotals.ViewName}(BankAccountId)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($"DROP VIEW {Schema.HaSpMan}.{BankAccountTotals.ViewName}");
    }
}
