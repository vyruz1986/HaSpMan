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
            RuleForEach(x => x.TransactionTypeAmounts)
                .SetValidator(new TransactionTypeValidator());
        }
    }
}