using System;
using System.Collections.Generic;

using Domain;

using FluentValidation;

using MediatR;

using Types;

namespace Commands.Handlers.Transaction.AddDebitTransaction
{
    public record AddDebitTransactionCommand(
        TransactionType TransactionType, 
        string CounterPartyName,
        Guid BankAccountId,
        decimal Amount,
        DateTimeOffset ReceivedDateTime, 
        string Description, 
        Guid? MemberId) : IRequest<Guid>;

    public class AddDebitTransactionCommandValidator : AbstractValidator<AddDebitTransactionCommand>
    {
        public AddDebitTransactionCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0m);
        }
    }
}