using Domain;

using FluentValidation;

using Types;

namespace Commands.Handlers.Transaction;

public class TransactionTypeValidator : AbstractValidator<TransactionTypeAmount>
{
    public TransactionTypeValidator()
    {
        RuleFor(x => x.Amount).GreaterThan(0m);

        RuleFor(x => x.TransactionType)
            .IsInEnum();
    }
}
