using Commands.Extensions;
using Commands.Services;

using Domain.Interfaces;

namespace Commands.Handlers.Member.AddMember;

public class AddMemberHandler : IRequestHandler<AddMemberCommand, Guid>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public AddMemberHandler(IMemberRepository memberRepository, IMapper mapper, IUserAccessor userAccessor)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<Guid> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        var newMember = new Domain.Member(
            firstName: request.FirstName,
            lastName: request.LastName,
            address: request.Address,
            membershipFee: request.MembershipFee,
            performedBy: _userAccessor.User.GetName() ?? throw new Exception("Command performed by user with no name"),
            membershipExpiryDate: request.MembershipExpiryDate,
            email: request.Email,
            phoneNumber: request.PhoneNumber
        );

        _memberRepository.Add(newMember);
        await _memberRepository.Save(cancellationToken);
        return newMember.Id;
    }
}
