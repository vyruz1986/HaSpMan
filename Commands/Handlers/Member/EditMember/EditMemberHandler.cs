using Commands.Extensions;
using Commands.Services;

using Domain.Interfaces;

namespace Commands.Handlers.Member.EditMember;

public class EditMemberHandler : IRequestHandler<EditMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUserAccessor _userAccessor;

    public EditMemberHandler(IMemberRepository memberRepository, IUserAccessor userAccessor)
    {
        _memberRepository = memberRepository;
        _userAccessor = userAccessor;
    }

    public async Task<Unit> Handle(EditMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetById(request.Id);

        if (member == null)
            throw new ArgumentException($"No member found by Id {request.Id}", nameof(request.Id));

        var performingUser = _userAccessor.User.GetName()!;
        member.ChangeAddress(request.Address, performingUser);
        member.ChangeEmail(request.Email, performingUser);
        member.ChangeMembershipExpiryDate(request.MembershipExpiryDate, performingUser);
        member.ChangeMembershipFee(request.MembershipFee, performingUser);
        member.ChangeName(request.FirstName, request.LastName, performingUser);
        member.ChangePhoneNumber(request.PhoneNumber, performingUser);

        await _memberRepository.Save(cancellationToken);

        return Unit.Value;
    }
}
