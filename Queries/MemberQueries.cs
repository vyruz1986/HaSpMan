using System;
using System.Linq.Expressions;

using Domain;

using MediatR;

using Queries.Enums;

namespace Queries
{
    public record SearchMembersQuery(
        string SearchString,
        int PageIndex,
        int PageSize,
        MemberSummaryOrderOption OrderBy,
        SortDirection SortDirection
    ) : IRequest<Paginated<MemberSummary>>;

    public enum MemberSummaryOrderOption
    {
        Name,
        Address,
        Email,
        PhoneNumber
    }
}
