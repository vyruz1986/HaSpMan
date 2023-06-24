using FluentValidation;

namespace Commands.Handlers.FinancialYear.AddFinancialYear;

public record AddFinancialYearCommand(DateOnly StartDate) : IRequest<Guid>;


public class AddFinancialYearCommandValidator : AbstractValidator<AddFinancialYearCommand>
{
    public AddFinancialYearCommandValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty();
    }
}