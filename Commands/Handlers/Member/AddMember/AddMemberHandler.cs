using Commands.Extensions;
using Commands.Services;

using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Commands.Handlers.Member.AddMember;

public class AddMemberHandler : IRequestHandler<AddMemberCommand, Guid>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly HaSpManContext _dbContext;

    public AddMemberHandler(IMemberRepository memberRepository, IUserAccessor userAccessor, HaSpManContext dbContext)
    {
        _memberRepository = memberRepository;
        _userAccessor = userAccessor;
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        await EnsureMemberIsNotDuplicate(request, cancellationToken);

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

    private async Task EnsureMemberIsNotDuplicate(AddMemberCommand command, CancellationToken ct)
    {
        var memberExistsByEmail = await _dbContext.Members
            .AsNoTracking()
            .AnyAsync(m => m.Email == command.Email, ct);

        if (memberExistsByEmail)
            throw new InvalidOperationException("Can't add member with same email address as existing member");

        var memberExistsByNameAndAddress = await _dbContext.Members
            .AsNoTracking()
            .AnyAsync(m =>
                m.FirstName == command.FirstName
                && m.LastName == command.LastName
                && m.Address.Street == command.Address.Street
                && m.Address.City == command.Address.City
                && m.Address.Country == command.Address.Country
                && m.Address.ZipCode == command.Address.ZipCode
                && m.Address.HouseNumber == command.Address.HouseNumber, ct);

        if (memberExistsByNameAndAddress)
            throw new InvalidOperationException("Can't add member with same name and address as existing member");
    }
}
