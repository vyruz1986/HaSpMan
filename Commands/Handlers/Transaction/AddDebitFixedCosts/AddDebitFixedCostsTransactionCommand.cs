using System;
using System.Collections.Generic;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddDebitFixedCosts
{
    public record AddDebitFixedCostsTransactionCommand(
        CounterParty CounterParty, BankAccount BankAccount, decimal Amount,
        DateTime ReceivedDateTime, string Description, int Sequence,
        ICollection<Domain.Transaction.TransactionAttachment> Attachments) : IRequest<Guid>;
}