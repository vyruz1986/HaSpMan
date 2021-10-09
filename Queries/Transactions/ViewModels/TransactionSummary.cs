using System;

using Types;

namespace Queries.Transactions.ViewModels
{
    public record TransactionSummary(
        Guid Id,
        string CounterParty,
        Guid BankAccountId,
        string Amount,
        DateTimeOffset ReceivedDateTime,
        TransactionType TransactionType,
        Guid? MemberId);
}
