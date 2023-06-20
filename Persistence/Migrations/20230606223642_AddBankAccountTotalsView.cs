using Microsoft.EntityFrameworkCore.Migrations;

using Persistence.Constants;
using Persistence.Views;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class AddBankAccountTotalsView : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"CREATE VIEW {Schema.HaSpMan}.{BankAccountsWithTotals.ViewName}
                                        WITH SCHEMABINDING
                                        AS
                                            SELECT
                                                SUM(t.Amount) AS Total,
                                                t.BankAccountId,
                                                COUNT_BIG(*) AS NumberOfTransactions
                                            FROM
                                                {Schema.HaSpMan}.Transactions t
                                                GROUP BY t.BankAccountId");

        migrationBuilder.Sql($@"CREATE UNIQUE CLUSTERED INDEX IX_{BankAccountsWithTotals.ViewName}
	                                    ON {Schema.HaSpMan}.{BankAccountsWithTotals.ViewName}(BankAccountId)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($"DROP VIEW {Schema.HaSpMan}.{BankAccountsWithTotals.ViewName}");
    }
}
