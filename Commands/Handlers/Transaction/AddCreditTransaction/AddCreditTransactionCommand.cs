using System;

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
        Guid? MemberId): IRequest<Guid>;
}