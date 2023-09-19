namespace Domain.Views;

public record BankAccountTotals(decimal Amount, long NumberOfTransactions)
{
    public const string ViewName = "vwBankAccountTotals";
}