using System.Linq.Expressions;

using AutoMapper.QueryableExtensions;

using Domain;

using Microsoft.EntityFrameworkCore;

using Persistence;

using Queries.Enums;
using Queries.Transactions.ViewModels;

namespace Queries.Transactions.Handlers;

public class GetTransactionsHandler : IRequestHandler<GetTransactionQuery, Paginated<TransactionSummary>>
{
    private readonly HaSpManContext _dbContext;
    private readonly IMapper _mapper;

    public GetTransactionsHandler(HaSpManContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Paginated<TransactionSummary>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _dbContext.FinancialYears
            .AsNoTracking()
            .SelectMany(y => y.Transactions)
            .Where(GetFilterCriteria(request.SearchString));

        var transactions = GetOrderedQueryable(request, baseQuery);

        var transactionViewModels = transactions
            .AsNoTracking()
            .ProjectTo<TransactionSummary>(_mapper.ConfigurationProvider)
            .Skip(request.PageIndex * request.PageSize)
            .Take(request.PageSize);

        var items = await transactionViewModels.ToListAsync(cancellationToken);
        var totalCount = await transactions.CountAsync(cancellationToken);

        return new Paginated<TransactionSummary>()
        {
            Items = items,
            Total = totalCount
        };
    }

    private static IQueryable<Transaction> GetOrderedQueryable(GetTransactionQuery request, IQueryable<Transaction> transactions)
    {
        transactions = request.SortDirection switch
        {
            SortDirection.Ascending => request.OrderBy switch
            {
                TransactionSummaryOrderOption.From => transactions.OrderBy(x => x.CounterPartyName),
                TransactionSummaryOrderOption.Amount => transactions.OrderBy(x => x.Amount),
                TransactionSummaryOrderOption.ReceivedDateTime => transactions.OrderBy(x => x.ReceivedDateTime),
                _ => transactions.OrderBy(x => x.DateFiled)
            },
            SortDirection.Descending => request.OrderBy switch
            {
                TransactionSummaryOrderOption.From => transactions.OrderByDescending(x => x.CounterPartyName),
                TransactionSummaryOrderOption.Amount => transactions.OrderByDescending(x => x.Amount),
                TransactionSummaryOrderOption.ReceivedDateTime => transactions.OrderByDescending(x =>
                    x.ReceivedDateTime),
                _ => transactions.OrderByDescending(x => x.DateFiled)
            },
            _ => transactions.OrderByDescending(x => x.DateFiled)
        };

        return transactions;
    }

    public static Expression<Func<Transaction, bool>> GetFilterCriteria(string searchString)
    {
        return t => t.CounterPartyName.ToLower().Contains(searchString) ||
                    t.Amount.ToString().Contains(searchString) ||
                    t.Description.ToLower().Contains(searchString);
    }
}
