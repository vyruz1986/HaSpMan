using MediatR;

using Queries.Members.ViewModels;

namespace Queries.Members.Handlers.GetMemberById;

public record GetMemberByIdQuery(
    Guid Id
) : IRequest<MemberDetail>;
