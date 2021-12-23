
using Domain;

namespace Queries.Transactions.ViewModels;

public record TransactionSummary(
    Guid Id,
    string CounterPartyName,
    Guid BankAccountId,
    decimal Amount,
    DateTimeOffset ReceivedDateTime,
    Guid? MemberId,
    string Description,
    ICollection<TransactionTypeAmount> TransactionTypeAmounts);
