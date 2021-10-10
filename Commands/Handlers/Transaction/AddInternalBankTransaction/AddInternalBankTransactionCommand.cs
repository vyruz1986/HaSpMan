using System;
using System.Collections.Generic;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddInternalBankTransaction
{
    public record AddInternalBankTransactionCommand(BankAccount From, BankAccount To, decimal Amount,
        DateTimeOffset ReceivedDateTime, string Description,
        ICollection<TransactionAttachment> Attachments) : IRequest<Guid>;
}