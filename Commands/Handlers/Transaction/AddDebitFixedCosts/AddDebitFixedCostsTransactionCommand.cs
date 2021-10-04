using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Domain;

using MediatR;

using Types;

namespace Commands.Handlers
{
    public record AddDebitFixedCostsTransactionCommand(
        CounterParty CounterParty, BankAccount BankAccount, decimal Amount,
        DateTime ReceivedDateTime, string Description, int Sequence,
        ICollection<Transaction.TransactionAttachment> Attachments) : IRequest<Guid>;
}