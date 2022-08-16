using FluentValidation;

namespace Commands.EditBankAccount;

public class EditBankAccountValidator : AbstractValidator<EditBankAccountCommand>
{
    public EditBankAccountValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.AccountNumber)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(34);


    }
}