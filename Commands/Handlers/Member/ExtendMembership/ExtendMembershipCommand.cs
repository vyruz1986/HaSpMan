using FluentValidation;

namespace Commands.Handlers.Member.ExtendMembership;

public record ExtendMembershipCommand(Guid Id, DateTime NewMembershipExpirationDate) : IRequest;

public class ExtendMembershipCommandValidator : AbstractValidator<ExtendMembershipCommand>
{
    public ExtendMembershipCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
        RuleFor(x => x.NewMembershipExpirationDate)
            .NotEmpty()
            .GreaterThan(DateTime.Now);
    }
}