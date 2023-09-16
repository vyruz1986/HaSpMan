using Domain.Views;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

/// <inheritdoc />
public partial class FixCreditTransactionAmountOnTotalsView : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"CREATE OR ALTER VIEW HaSpMan.vwBankAccountTotals
                                        WITH SCHEMABINDING
                                        AS
                                            SELECT
                                                SUM(
                                                    CASE t.Discriminator
                                                        WHEN 'CreditTransaction' THEN t.Amount * -1
                                                        ELSE t.Amount
                                                    END
                                                ) AS Total,
                                                t.BankAccountId,
                                                COUNT_BIG(*) AS NumberOfTransactions
                                            FROM
                                                HaSpMan.Transactions t
                                                GROUP BY t.BankAccountId");

        migrationBuilder.Sql($@"CREATE UNIQUE CLUSTERED INDEX IX_{BankAccountsWithTotals.ViewName}
	                                    ON HaSpMan.{BankAccountsWithTotals.ViewName}(BankAccountId)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"CREATE OR ALTER VIEW HaSpMan.vwBankAccountTotals
                                        WITH SCHEMABINDING
                                        AS
                                            SELECT
                                                SUM(t.Amount) AS Total,
                                                t.BankAccountId,
                                                COUNT_BIG(*) AS NumberOfTransactions
                                            FROM
                                                HaSpMan.Transactions t
                                                GROUP BY t.BankAccountId");
    }
}
