using MediatR;

using Queries.Members.Handlers;

namespace Queries.Members
{
    public record AutocompleteMembersQuery(string SearchString) : IRequest<AutocompleteMemberResponse>;
}