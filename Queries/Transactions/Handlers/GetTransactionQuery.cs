using Queries.Enums;
using Queries.Transactions.ViewModels;

namespace Queries.Transactions.Handlers;

public record GetTransactionQuery(string SearchString, int PageIndex, int PageSize,
    TransactionSummaryOrderOption OrderBy, SortDirection SortDirection) : IRequest<Paginated<TransactionSummary>>;

public enum TransactionSummaryOrderOption
{
    From,
    ReceivedDateTime,
    Amount
}
