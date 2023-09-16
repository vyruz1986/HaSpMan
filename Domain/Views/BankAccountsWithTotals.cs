namespace Domain.Views;

public record BankAccountsWithTotals(Guid BankAccountId, decimal Total, long NumberOfTransactions)
{
    public const string ViewName = "vwBankAccountTotals";
}