using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Commands.Handlers.Member.ExtendMembership;

public record ExtendMembershipCommand(Guid Id, DateTime NewMembershipExpirationDate) : IRequest;

public class ExtendMembershipCommandValidator : AbstractValidator<ExtendMembershipCommand>
{
    public ExtendMembershipCommandValidator()
    {
        RuleFor(x => x.NewMembershipExpirationDate)
            .NotEmpty()
            .GreaterThan(DateTime.Now);
    }
}