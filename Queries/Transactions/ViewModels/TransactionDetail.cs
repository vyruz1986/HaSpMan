
using Domain;

namespace Queries.Transactions.ViewModels;

public record TransactionDetail(
    Guid Id,
    string Description,
    string CounterPartyName,
    Guid? MemberId,
    Guid BankAccountId,
    decimal Amount,
    DateTimeOffset ReceivedDateTime,
    ICollection<TransactionTypeAmount> TransactionTypeAmounts);
