namespace Queries.Transactions.ViewModels;

public record TransactionDetail(
    Guid Id,
    string Description,
    string CounterPartyName,
    Guid? MemberId,
    Guid BankAccountId,
    decimal Amount,
    DateTimeOffset ReceivedDateTime,
    ICollection<TransactionTypeAmount> TransactionTypeAmounts,
    ICollection<TransactionAttachment> TransactionAttachments,
    TransactionType TransactionType,
    bool ReadOnly);

public record TransactionTypeAmount(Types.TransactionType TransactionType, decimal Amount);
public record TransactionAttachment(string Name, string FullPath);