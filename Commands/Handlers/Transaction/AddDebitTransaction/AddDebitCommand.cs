using System;
using System.Collections.Generic;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddDebitTransaction
{
    public record AddDebitCommand(TransactionType TransactionType, CounterParty CounterParty, Guid BankAccountId,
        decimal Amount,
        DateTimeOffset ReceivedDateTime, string Description, int Sequence,
        ICollection<Domain.Transaction.TransactionAttachment> Attachments, Guid? MemberId) : IRequest<Guid>;
}