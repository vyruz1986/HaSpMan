using Commands.Extensions;
using Commands.Services;

using Persistence.Repositories;

namespace Commands.Handlers.Member.ExtendMembership;

public class ExtendMembershipHandler : IRequestHandler<ExtendMembershipCommand>
{
    private readonly IMemberRepository _repository;
    private readonly IUserAccessor _userAccessor;

    public ExtendMembershipHandler(IMemberRepository repository, IUserAccessor userAccessor)
    {
        _repository = repository;
        _userAccessor = userAccessor;
    }

    public async Task Handle(ExtendMembershipCommand request, CancellationToken cancellationToken)
    {
        var member = await _repository.GetById(request.Id)
            ?? throw new ArgumentException($"No member found by Id {request.Id}", nameof(request.Id));

        var performingUser = _userAccessor.User.GetName()!;

        member.ChangeMembershipExpiryDate(request.NewMembershipExpirationDate, performingUser);

        await _repository.Save(cancellationToken);
    }
}
