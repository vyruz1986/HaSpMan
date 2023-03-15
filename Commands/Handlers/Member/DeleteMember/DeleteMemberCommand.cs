using Domain.Interfaces;

namespace Commands.Handlers.Member.DeleteMember;

public record DeleteMemberCommand(Guid Id) : IRequest;

public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand>
{
    private readonly IMemberRepository _memberRepository;

    public DeleteMemberHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetById(request.Id);

        if (member == null)
            throw new ArgumentException($"No member found by Id {request.Id}", nameof(request.Id));

        _memberRepository.Remove(member);

        await _memberRepository.Save(cancellationToken);
    }
}

