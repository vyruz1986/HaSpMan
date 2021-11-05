using System;
using System.Collections.Generic;

using Domain;

using FluentValidation;

using MediatR;

namespace Commands.Handlers.Transaction.EditTransaction
{
    public record EditTransactionCommand(
        Guid Id, 
        string CounterPartyName,
        Guid? MemberId,
        Guid BankAccountId,
        DateTimeOffset ReceivedDateTime,
        string Description,
        ICollection<TransactionTypeAmount> TransactionTypeAmounts) : IRequest<Guid>;


    public class EditTransactionCommandValidator : AbstractValidator<EditTransactionCommand>
    {
        public EditTransactionCommandValidator()
        {
            RuleForEach(x => x.TransactionTypeAmounts)
                .SetValidator(new TransactionTypeValidator());
        }
    }
}
