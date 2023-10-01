
using Microsoft.EntityFrameworkCore;

using Persistence;

namespace Queries.FinancialYears;

public record GetFinancialYearByTransactionId(Guid TransactionId) : IRequest<FinancialYear?>;

public class GetFinancialYearByTransactionIdHandler : IRequestHandler<GetFinancialYearByTransactionId, FinancialYear?>
{
    private readonly HaSpManContext _context;

    public GetFinancialYearByTransactionIdHandler(HaSpManContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<FinancialYear?> GetFinancialYearByTransactionId(Guid transactionId, CancellationToken ct)
        => _context.FinancialYears
            .AsNoTracking()
            .Where(x => x.Transactions.Any(t => t.Id == transactionId))
            .Select(x => new FinancialYear(x.Id, x.StartDate, x.EndDate, x.IsClosed, x.Name))
            .SingleOrDefaultAsync(ct);

    public Task<FinancialYear?> Handle(GetFinancialYearByTransactionId request, CancellationToken cancellationToken)
        => GetFinancialYearByTransactionId(request.TransactionId, cancellationToken);
}
