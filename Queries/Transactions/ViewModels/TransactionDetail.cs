using System;
using System.Collections.Generic;

using Domain;

using Types;

namespace Queries.Transactions.ViewModels
{
    public record TransactionDetail(
        Guid Id,
        string Description,
        string CounterPartyName,
        Guid? MemberId,
        Guid BankAccountId,
        decimal Amount,
        DateTimeOffset ReceivedDateTime,
        ICollection<TransactionTypeAmount> TransactionTypeAmounts);
}