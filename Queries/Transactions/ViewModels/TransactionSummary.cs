namespace Queries.Transactions.ViewModels;

public record TransactionSummary(
    Guid Id,
    string CounterPartyName,
    Guid BankAccountId,
    decimal Amount,
    DateTimeOffset ReceivedDateTime,
    DateTimeOffset DateFiled,
    Guid? MemberId,
    string Description,
    ICollection<TransactionTypeAmount> TransactionTypeAmounts,
    bool ReadOnly);
