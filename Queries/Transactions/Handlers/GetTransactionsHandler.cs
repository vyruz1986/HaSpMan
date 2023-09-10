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
    private readonly IDbContextFactory<HaSpManContext> _contextFactory;
    private readonly IMapper _mapper;

    public GetTransactionsHandler(IDbContextFactory<HaSpManContext> contextFactory, IMapper mapper)
    {
        _contextFactory = contextFactory;
        _mapper = mapper;
    }
    public async Task<Paginated<TransactionSummary>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
    {
        var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var transactions =
            context.FinancialYears
            .SelectMany(x => x.Transactions)
            .AsNoTracking()
            .Where(GetFilterCriteria(request.SearchString));

        var totalCount = await transactions.CountAsync(cancellationToken);

        transactions = GetOrderedQueryable(request, transactions);

        var transactionViewModels =
            transactions
                .ProjectTo<TransactionSummary>(_mapper.ConfigurationProvider)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize);

        var items = await transactionViewModels.ToListAsync(cancellationToken);

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
            _ => transactions
        };

        return transactions;
    }

    public static Expression<Func<Transaction, bool>> GetFilterCriteria(string searchString)
    {
        return t => t.CounterPartyName.ToLower().Contains(searchString) ||
                    t.Amount.ToString().Contains(searchString);
    }
}
