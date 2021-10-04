﻿using System;
using System.Collections.Generic;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddCreditMemberFee
{
    public record AddCreditMemberFeeTransactionCommand(CounterParty CounterParty, BankAccount BankAccount,
        decimal Amount,
        DateTimeOffset ReceivedDateTime, string Description, int Sequence,
        ICollection<Domain.Transaction.TransactionAttachment> Attachments) : IRequest<Guid>;
}