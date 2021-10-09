using System;
using System.Collections.Generic;

using Domain;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddCreditTransaction
{
    public record AddCreditTransactionCommand(
        TransactionType TransactionType, 
        string CounterPartyName, 
        Guid BankAccountId,
        decimal Amount,
        DateTimeOffset ReceivedDateTime, 
        string Description, 
        int Sequence,
        Guid? MemberId): IRequest<Guid>;
}