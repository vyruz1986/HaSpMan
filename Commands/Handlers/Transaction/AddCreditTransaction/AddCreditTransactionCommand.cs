using System;
using System.Collections.Generic;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddCreditTransaction
{
    public record AddCreditTransactionCommand(
        string CounterPartyName,
        Guid BankAccountId,
        decimal Amount,
        DateTimeOffset ReceivedDateTime,
        string Description,
        Guid? MemberId,
        ICollection<TransactionTypeAmount> TransactionTypeAmounts) : IRequest<Guid>;
}