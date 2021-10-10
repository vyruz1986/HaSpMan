using System;

using MediatR;

using Queries.Members.ViewModels;

namespace Queries.Members.Handlers
{
    public record GetMemberByIdQuery(
        Guid Id
    ) : IRequest<MemberDetail>;
}