using System;
using System.Collections.Generic;

using Commands.Handlers.Transaction.AddDebitTransaction;

using Domain;

using FluentValidation;

using MediatR;

namespace Commands.Handlers.Transaction.AddCreditTransaction
{
    public record AddCreditTransactionCommand(
        string CounterPartyName,
        Guid BankAccountId,
        DateTimeOffset ReceivedDateTime,
        string Description,
        Guid? MemberId,
        ICollection<TransactionTypeAmount> TransactionTypeAmounts) : IRequest<Guid>;


    public class AddCreditTransactionCommandValidator : AbstractValidator<AddCreditTransactionCommand>
    {
        public AddCreditTransactionCommandValidator()
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