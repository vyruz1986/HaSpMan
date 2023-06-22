using Domain;

namespace Persistence.Views;

public record BankAccountsWithTotals(Guid BankAccountId, decimal Total, long NumberOfTransactions)
{
    public const string ViewName = "vwBankAccountTotals";
    public BankAccount Account { get; set; } = null!;
}