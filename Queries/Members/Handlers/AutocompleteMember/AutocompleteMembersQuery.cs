using MediatR;

namespace Queries.Members.Handlers.AutocompleteMember;

public record AutocompleteMembersQuery(string SearchString) : IRequest<AutocompleteMemberResponse>;
public record AutocompleteMemberResponse(IReadOnlyList<SearchMembers.AutocompleteMember> Members);
