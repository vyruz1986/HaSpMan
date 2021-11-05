using System;
using System.Collections.Generic;

using Domain;

using MediatR;

namespace Commands.Handlers.Transaction.EditTransaction
{
    public record EditTransactionCommand(
        Guid Id, 
        string CounterPartyName,
        Guid? MemberId,
        decimal Amount,
        Guid BankAccountId,
        DateTimeOffset ReceivedDateTime,
        string Description,
        ICollection<TransactionTypeAmount> TransactionTypeAmounts) : IRequest<Guid>;
}
