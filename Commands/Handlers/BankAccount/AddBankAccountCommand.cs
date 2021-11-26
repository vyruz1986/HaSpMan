using System;

using FluentValidation;

using MediatR;

namespace Commands.Handlers.BankAccount
{
    public record AddBankAccountCommand(
        string Name,
        string AccountNumber
    ) : IRequest<Guid>;

    public class AddAccountCommandValidator : AbstractValidator<AddBankAccountCommand>
    {
        public AddAccountCommandValidator()
        {
            RuleFor(x => x.AccountNumber)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(34);

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}