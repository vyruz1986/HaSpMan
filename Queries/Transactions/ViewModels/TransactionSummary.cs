using System;

using Types;

namespace Queries.Transactions.ViewModels
{
    public record TransactionSummary(
        Guid Id,
        string CounterPartyName,
        Guid BankAccountId,
        decimal Amount,
        DateTimeOffset ReceivedDateTime,
        TransactionType TransactionType,
        Guid? MemberId);
}
