using FluentValidation;

namespace Commands.Handlers.FinancialYear.CloseFinancialYear;

public record CloseFinancialYearCommand(Guid Id) : IRequest;

public class CloseFinancialYearCommandValidator : AbstractValidator<CloseFinancialYearCommand>
{
    public CloseFinancialYearCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}