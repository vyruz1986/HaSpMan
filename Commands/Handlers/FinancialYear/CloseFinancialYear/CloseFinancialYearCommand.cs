using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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