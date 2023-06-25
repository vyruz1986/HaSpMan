using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Commands.Handlers.FinancialYearConfiguration;

public record SetFinancialYearConfigurationCommand(DateTimeOffset StartDate) : IRequest;


public class SetFinancialYearConfigurationCommandValidator : AbstractValidator<SetFinancialYearConfigurationCommand>
{
    public SetFinancialYearConfigurationCommandValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty();
    }
}