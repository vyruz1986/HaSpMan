using System;
using System.Collections.Generic;

using Domain;

using MediatR;

using Types;

namespace Commands.Handlers
{
    public record AddInternalBankTransactionCommand(BankAccount From, BankAccount To, decimal Amount,
        DateTime ReceivedDateTime, string Description, int FromSequence, int ToSequence,
        ICollection<Transaction.TransactionAttachment> Attachments) : IRequest<Guid>;
}