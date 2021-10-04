using System;
using System.Collections.Generic;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddInternalBank
{
    public record AddInternalBankTransactionCommand(BankAccount From, BankAccount To, decimal Amount,
        DateTime ReceivedDateTime, string Description, int FromSequence, int ToSequence,
        ICollection<Domain.Transaction.TransactionAttachment> Attachments) : IRequest<Guid>;
}