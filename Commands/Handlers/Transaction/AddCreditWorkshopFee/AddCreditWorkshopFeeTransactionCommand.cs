﻿using System;
using System.Collections.Generic;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddCreditWorkshopFee
{
    public record AddCreditWorkshopFeeTransactionCommand(CounterParty CounterParty, BankAccount BankAccount,
        decimal Amount,
        DateTimeOffset ReceivedDateTime, string Description, int Sequence,
        ICollection<Domain.Transaction.TransactionAttachment> Attachments) : IRequest<Guid>;
}