using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Commands.Extensions;
using Commands.Services;

using Domain.Interfaces;

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

public class ExtendMembershipHandler : IRequestHandler<ExtendMembershipCommand>
{
    private readonly IMemberRepository _repository;
    private readonly IUserAccessor _userAccessor;

    public ExtendMembershipHandler(IMemberRepository repository, IUserAccessor userAccessor)
    {
        _repository = repository;
        _userAccessor = userAccessor;
    }

    public async Task<Unit> Handle(ExtendMembershipCommand request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetById(request.Id);

        if (member == null)
            throw new ArgumentException($"No member found by Id {request.Id}", nameof(request.Id));

        var performingUser = _userAccessor.User.GetName()!;

        member.ChangeMembershipExpiryDate(request.NewMembershipExpirationDate, performingUser);

        await _repository.Save(cancellationToken);

        return Unit.Value;
    }
}