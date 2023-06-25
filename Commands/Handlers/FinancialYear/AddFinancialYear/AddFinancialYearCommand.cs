using FluentValidation;

namespace Commands.Handlers.FinancialYear.AddFinancialYear;

public record AddFinancialYearCommand() : IRequest<Guid>;


public class AddFinancialYearCommandValidator : AbstractValidator<AddFinancialYearCommand>
{
    public AddFinancialYearCommandValidator()
    {
    }
}