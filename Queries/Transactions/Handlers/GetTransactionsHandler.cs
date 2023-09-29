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

        var financialYears = context.FinancialYears
            .AsNoTracking();

        // financialYears = request.SortDirection switch
        // {
        //     SortDirection.Descending => request.OrderBy switch
        //     {
        //         TransactionSummaryOrderOption.From => financialYears.Include(f => f.Transactions.OrderByDescending(t => t.CounterPartyName)),
        //         TransactionSummaryOrderOption.Amount => financialYears.Include(f => f.Transactions.OrderByDescending(t => t.Amount)),
        //         _ => financialYears.Include(f => f.Transactions.OrderByDescending(t => t.ReceivedDateTime)),
        //     },
        //     _ => request.OrderBy switch
        //     {
        //         TransactionSummaryOrderOption.From => financialYears.Include(f => f.Transactions.OrderBy(t => t.CounterPartyName)),
        //         TransactionSummaryOrderOption.Amount => financialYears.Include(f => f.Transactions.OrderBy(t => t.Amount)),
        //         _ => financialYears.Include(f => f.Transactions.OrderBy(t => t.ReceivedDateTime)),
        //     }
        // };

        var transactions = financialYears
            .SelectMany(y => y.Transactions.Select(t => new TransactionSummary(
                t.Id,
                t.CounterPartyName,
                t.BankAccountId,
                t.Amount,
                t.ReceivedDateTime,
                t.DateFiled,
                t.MemberId,
                t.Description,
                t.TransactionTypeAmounts.Select(tamt => new ViewModels.TransactionTypeAmount(tamt.TransactionType, tamt.Amount)),
                y.IsClosed)))
            .OrderBy(t => t.CounterPartyName);

        // var transactions =
        //     context.FinancialYears
        //     .SelectMany(x => x.Transactions)
        //     .AsNoTracking()
        //     .Where(GetFilterCriteria(request.SearchString));

        var totalCount = await transactions.CountAsync(cancellationToken);

        // transactions = GetOrderedQueryable(request, transactions);

        var transactionViewModels =
            transactions
                // .ProjectTo<TransactionSummary>(_mapper.ConfigurationProvider)
                .Skip(request.PageIndex * request.PageSize)
                .Take(request.PageSize);

        var items = await transactionViewModels.ToListAsync(cancellationToken);

        return new Paginated<TransactionSummary>()
        {
            Items = items,
            Total = totalCount
        };

    }

    private static IOrderedEnumerable<Transaction> GetOrderedQueryable(GetTransactionQuery request, IEnumerable<Transaction> transactions)
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

        return (IOrderedEnumerable<Transaction>)transactions;
    }

    public static Expression<Func<Transaction, bool>> GetFilterCriteria(string searchString)
    {
        return t => t.CounterPartyName.ToLower().Contains(searchString) ||
                    t.Amount.ToString().Contains(searchString) ||
                    t.Description.ToLower().Contains(searchString);
    }
}
