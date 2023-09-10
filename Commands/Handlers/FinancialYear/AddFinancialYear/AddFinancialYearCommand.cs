using FluentValidation;

namespace Commands.Handlers.FinancialYear.AddFinancialYear;

public record AddFinancialYearCommand() : IRequest<Domain.FinancialYear>;

public class AddFinancialYearCommandValidator : AbstractValidator<AddFinancialYearCommand>
{
    public AddFinancialYearCommandValidator()
    {
    }
}