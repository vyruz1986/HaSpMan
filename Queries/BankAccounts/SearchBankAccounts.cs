using System.Linq.Expressions;

using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using Persistence;
using Persistence.Views;

using Queries.Enums;

namespace Queries.BankAccounts;

public record SearchBankAccountsQuery(
    string SearchString,
    int PageIndex,
    int PageSize,
    BankAccountDetailOrderOption OrderBy,
    SortDirection SortDirection
) : IRequest<Paginated<BankAccountDetailWithTotal>>;

public enum BankAccountDetailOrderOption
{
    Name,
    AccountNumber,
}

public class SearchBankAccountsHandler : IRequestHandler<SearchBankAccountsQuery, Paginated<BankAccountDetailWithTotal>>
{
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;
    private readonly IMapper _mapper;

    public SearchBankAccountsHandler(IDbContextFactory<HaSpManContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }

    public async Task<Paginated<BankAccountDetailWithTotal>> Handle(SearchBankAccountsQuery request, CancellationToken cancellationToken)
    {
        var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var bankAccountsQueryable = context.BankAccountsWithTotals
            .AsNoTracking()
            .Where(GetFilterCriteria(request.SearchString));

        var total = await bankAccountsQueryable.CountAsync(cancellationToken);

        bankAccountsQueryable = GetOrderedQueryable(request, bankAccountsQueryable);

        var bankAccountsDetailQueryable = bankAccountsQueryable
            .ProjectTo<BankAccountDetailWithTotal>(_mapper.ConfigurationProvider)
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize);

        var items = await bankAccountsDetailQueryable.ToListAsync(cancellationToken: cancellationToken);

        return new Paginated<BankAccountDetailWithTotal>
        {
            Items = items,
            Total = total
        };
    }

    private static IQueryable<BankAccountsWithTotals> GetOrderedQueryable(SearchBankAccountsQuery request, IQueryable<BankAccountsWithTotals> memberQueryable)
    {
        if (request.SortDirection == SortDirection.Ascending)
        {
            memberQueryable = request.OrderBy switch
            {
                BankAccountDetailOrderOption.Name => memberQueryable
                    .OrderBy(m => m.Account.Name),
                BankAccountDetailOrderOption.AccountNumber => memberQueryable
                    .OrderBy(m => m.Account.AccountNumber),
                _ => memberQueryable
                    .OrderBy(m => m.Account.Name)
            };
        }

        if (request.SortDirection == SortDirection.Descending)
        {
            memberQueryable = request.OrderBy switch
            {
                BankAccountDetailOrderOption.Name => memberQueryable
                    .OrderByDescending(m => m.Account.Name),
                BankAccountDetailOrderOption.AccountNumber => memberQueryable
                    .OrderByDescending(m => m.Account.AccountNumber),
                _ => memberQueryable
                    .OrderByDescending(m => m.Account.Name)
            };
        }

        return memberQueryable;
    }

    private static Expression<Func<BankAccountsWithTotals, bool>> GetFilterCriteria(string searchString)
    {
        var lowerCaseSearchString = searchString.ToLower();
        return m => m.Account.Name.ToLower().Contains(lowerCaseSearchString) ||
                   m.Account.AccountNumber.ToLower().Contains(lowerCaseSearchString);
    }
}