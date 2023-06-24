using FluentValidation;

namespace Commands.Handlers.FinancialYear;

public record AddFinancialYearCommand(DateOnly StartDate) : IRequest<Guid>;


public class AddFinancialYearCommandValidator : AbstractValidator<AddFinancialYearCommand>
{
    public AddFinancialYearCommandValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty();
    }
}