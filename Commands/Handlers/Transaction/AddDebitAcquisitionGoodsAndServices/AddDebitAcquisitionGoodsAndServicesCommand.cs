using System;
using System.Collections.Generic;

using Domain;

using MediatR;

using Types;

namespace Commands.Handlers
{
    public record AddDebitAcquisitionGoodsAndServicesCommand(CounterParty CounterParty, BankAccount BankAccount,
        decimal Amount,
        DateTime ReceivedDateTime, string Description, int Sequence,
        ICollection<Transaction.TransactionAttachment> Attachments) : IRequest<Guid>;
}