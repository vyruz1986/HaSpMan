
using Commands.Extensions;
using Commands.Services;

using Domain.Interfaces;

using MediatR;

using Persistence.Repositories;

namespace Commands;

public record DeleteMemberCommand(Guid Id) : IRequest;

public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly ISystemAuditEventRepository _systemAuditEventRepository;

    public DeleteMemberHandler(IMemberRepository memberRepository, IUserAccessor userAccessor, ISystemAuditEventRepository systemAuditEventRepository)
    {
        _memberRepository = memberRepository;
        _userAccessor = userAccessor;
        _systemAuditEventRepository = systemAuditEventRepository;
    }
    public async Task<Unit> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetById(request.Id);

        if (member == null)
            throw new ArgumentException($"No member found by Id {request.Id}", nameof(request.Id));
        var performingUser = _userAccessor.User.GetName()!;

        _memberRepository.Remove(member);

        var initalFirstName = member.FirstName[..1];
        var intialLastName = member.LastName[..1];
        var sanitzedMemberName = $"{initalFirstName}*** {intialLastName}***";

        await _memberRepository.Save(cancellationToken);

        //_systemAuditEventRepository.Add(new Domain.SystemAuditEvent(DateTimeOffset.UtcNow, performingUser, 
        //    $"Member {sanitzedMemberName} was deleted"));
        //await _systemAuditEventRepository.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}

