using System;
using System.Collections.Generic;

using Domain;

using FluentValidation;

using MediatR;

namespace Commands.Handlers.Transaction.AddDebitTransaction
{
    public record AddDebitTransactionCommand(
        string CounterPartyName,
        Guid BankAccountId,
        DateTimeOffset ReceivedDateTime, 
        string Description, 
        Guid? MemberId,
        ICollection<TransactionTypeAmount> TransactionTypeAmounts) : IRequest<Guid>;

    public class AddDebitTransactionCommandValidator : AbstractValidator<AddDebitTransactionCommand>
    {
        public AddDebitTransactionCommandValidator()
        {
            RuleForEach(x => x.TransactionTypeAmounts)
                .SetValidator(new TransactionTypeValidator());
        }
    }
}