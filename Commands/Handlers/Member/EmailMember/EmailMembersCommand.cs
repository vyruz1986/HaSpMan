using Commands.Services;

namespace Commands.Handlers.Member.EmailMember;
public record EmailMembersCommand(IEnumerable<Guid> MemberIds, string Subject, string Message) : IRequest<IEnumerable<SendError>>;
