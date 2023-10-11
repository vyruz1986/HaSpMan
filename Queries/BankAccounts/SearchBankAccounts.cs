using System.Linq.Expressions;

using AutoMapper.QueryableExtensions;

using Domain;

using Microsoft.EntityFrameworkCore;

using Persistence;

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
    private readonly HaSpManContext _context;
    private readonly IMapper _mapper;

    public SearchBankAccountsHandler(HaSpManContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Paginated<BankAccountDetailWithTotal>> Handle(SearchBankAccountsQuery request, CancellationToken cancellationToken)
    {
        var bankAccountsQueryable = _context.BankAccounts
            .AsNoTracking()
            .Where(GetFilterCriteria(request.SearchString));

        var total = await bankAccountsQueryable.CountAsync(cancellationToken);

        bankAccountsQueryable = GetOrderedQueryable(request, bankAccountsQueryable);

        var bankAccountsDetailQueryable = bankAccountsQueryable
            .ProjectTo<BankAccountDetailWithTotal>(_mapper.ConfigurationProvider)
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize);

        var items = await bankAccountsDetailQueryable.ToListAsync(cancellationToken);

        return new Paginated<BankAccountDetailWithTotal>
        {
            Items = items,
            Total = total
        };
    }

    private static IQueryable<BankAccount> GetOrderedQueryable(SearchBankAccountsQuery request, IQueryable<BankAccount> memberQueryable)
    {
        if (request.SortDirection == SortDirection.Ascending)
        {
            memberQueryable = request.OrderBy switch
            {
                BankAccountDetailOrderOption.Name => memberQueryable
                    .OrderBy(m => m.Name),
                BankAccountDetailOrderOption.AccountNumber => memberQueryable
                    .OrderBy(m => m.AccountNumber),
                _ => memberQueryable
                    .OrderBy(m => m.Name)
            };
        }

        if (request.SortDirection == SortDirection.Descending)
        {
            memberQueryable = request.OrderBy switch
            {
                BankAccountDetailOrderOption.Name => memberQueryable
                    .OrderByDescending(m => m.Name),
                BankAccountDetailOrderOption.AccountNumber => memberQueryable
                    .OrderByDescending(m => m.AccountNumber),
                _ => memberQueryable
                    .OrderByDescending(m => m.Name)
            };
        }

        return memberQueryable;
    }

    private static Expression<Func<BankAccount, bool>> GetFilterCriteria(string searchString)
    {
        var lowerCaseSearchString = searchString.ToLower();
        return m => m.Name.ToLower().Contains(lowerCaseSearchString) ||
                   m.AccountNumber.ToLower().Contains(lowerCaseSearchString);
    }
}