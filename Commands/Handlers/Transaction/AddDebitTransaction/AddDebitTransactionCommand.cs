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
            RuleFor(x => x.CounterPartyName)
                .MaximumLength(120);
            RuleFor(x => x.Description)
                .MaximumLength(1000);
            RuleFor(x => x.BankAccountId)
                .NotEmpty();
            When(x => x.MemberId != null, () =>
            {
                RuleFor(x => x.MemberId)
                    .NotEmpty();
            });

            RuleFor(x => x.ReceivedDateTime)
                .LessThanOrEqualTo(x => DateTimeOffset.Now);
            RuleForEach(x => x.TransactionTypeAmounts)
                .SetValidator(new TransactionTypeValidator());
        }
    }
}