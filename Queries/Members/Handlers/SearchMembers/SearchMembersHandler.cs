using System.Linq.Expressions;

using Domain;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.Enums;
using Queries.Members.ViewModels;

namespace Queries.Members.Handlers.SearchMembers;

public class SearchMembersHandler : IRequestHandler<SearchMembersQuery, Paginated<MemberSummary>>
{
    private readonly HaSpManContext _context;

    public SearchMembersHandler(HaSpManContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Paginated<MemberSummary>> Handle(SearchMembersQuery request, CancellationToken cancellationToken)
    {
        var memberQueryable = _context.Members
            .AsNoTracking()
            .Where(GetTextFilterCriteria(request.SearchString));

        if (!request.ShowActive)
        {
            memberQueryable = memberQueryable.Where(m => m.MembershipExpiryDate != null && m.MembershipExpiryDate.Value.Date < DateTimeOffset.Now.Date);
        }

        if (!request.ShowInactive)
        {
            memberQueryable = memberQueryable.Where(m => m.MembershipExpiryDate == null || m.MembershipExpiryDate!.Value.Date >= DateTimeOffset.Now.Date);
        }

        var total = await memberQueryable.CountAsync(cancellationToken);

        var orderedQueryable = GetOrderedQueryable(request, memberQueryable);

        var memberSummaryQueryable = orderedQueryable
            .Select(x => new MemberSummary(x.Id, x.Name, x.Address.ToString(), x.Email!, x.PhoneNumber!, x.IsActive(), x.MembershipExpiryDate))
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize);

        var items = await memberSummaryQueryable.ToListAsync(cancellationToken: cancellationToken);

        return new Paginated<MemberSummary>
        {
            Items = items,
            Total = total
        };
    }

    private static IQueryable<Member> GetOrderedQueryable(SearchMembersQuery request, IQueryable<Member> memberQueryable)
    {
        if (request.SortDirection == SortDirection.Ascending)
        {
            memberQueryable = request.OrderBy switch
            {
                MemberSummaryOrderOption.Address => memberQueryable
                    .OrderBy(m => m.Address.ZipCode)
                    .ThenBy(m => m.Address.City)
                    .ThenBy(m => m.Address.Street)
                    .ThenBy(m => m.Address.HouseNumber),
                MemberSummaryOrderOption.Email => memberQueryable
                    .OrderBy(m => m.Email),
                MemberSummaryOrderOption.Name => memberQueryable
                    .OrderBy(m => m.FirstName)
                    .ThenBy(m => m.LastName),
                MemberSummaryOrderOption.PhoneNumber => memberQueryable
                    .OrderBy(m => m.PhoneNumber),
                MemberSummaryOrderOption.IsActive => memberQueryable
                    .OrderBy(Member.IsActiveExpression),
                MemberSummaryOrderOption.MembershipExpiryDate => memberQueryable
                    .OrderBy(m => m.MembershipExpiryDate),
                _ => memberQueryable
                    .OrderBy(m => m.FirstName)
                    .ThenBy(m => m.LastName)
            };
        }

        if (request.SortDirection == SortDirection.Descending)
        {
            memberQueryable = request.OrderBy switch
            {
                MemberSummaryOrderOption.Address => memberQueryable
                    .OrderByDescending(m => m.Address.ZipCode)
                    .ThenBy(m => m.Address.City)
                    .ThenBy(m => m.Address.Street)
                    .ThenBy(m => m.Address.HouseNumber),
                MemberSummaryOrderOption.Email => memberQueryable
                    .OrderByDescending(m => m.Email),
                MemberSummaryOrderOption.Name => memberQueryable
                    .OrderByDescending(m => m.FirstName)
                    .ThenBy(m => m.LastName),
                MemberSummaryOrderOption.PhoneNumber => memberQueryable
                    .OrderByDescending(m => m.PhoneNumber),
                MemberSummaryOrderOption.IsActive => memberQueryable
                    .OrderByDescending(Member.IsActiveExpression),
                MemberSummaryOrderOption.MembershipExpiryDate => memberQueryable
                    .OrderByDescending(m => m.MembershipExpiryDate),
                _ => memberQueryable
                    .OrderByDescending(m => m.FirstName)
                    .ThenBy(m => m.LastName)
            };
        }

        return memberQueryable;
    }

    private static Expression<Func<Member, bool>> GetTextFilterCriteria(string searchString)
    {
        var lowerCaseSearchString = searchString.ToLower();
        return m => m.Address.City.ToLower().Contains(lowerCaseSearchString) ||
                   m.Address.Country.ToLower().Contains(lowerCaseSearchString) ||
                   m.Address.HouseNumber.ToLower().Contains(lowerCaseSearchString) ||
                   m.Address.Street.ToLower().Contains(lowerCaseSearchString) ||
                   m.Address.ZipCode.ToLower().Contains(lowerCaseSearchString) ||
                   (m.Email != null && m.Email.ToLower().Contains(lowerCaseSearchString)) ||
                   m.FirstName.ToLower().Contains(lowerCaseSearchString) ||
                   m.LastName.ToLower().Contains(lowerCaseSearchString) ||
                   (m.PhoneNumber != null && m.PhoneNumber.ToLower().Contains(lowerCaseSearchString));
    }
}
