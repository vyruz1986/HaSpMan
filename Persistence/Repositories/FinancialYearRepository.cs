using Domain;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class FinancialYearRepository : IFinancialYearRepository
{

    private readonly HaSpManContext _context;

    public FinancialYearRepository(IDbContextFactory<HaSpManContext> contextFactory)
    {

        _context = contextFactory.CreateDbContext();
    }

    public Task<FinancialYear?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.FinancialYears.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task<FinancialYear?> GetTransactionAsync(Guid transactionId, CancellationToken cancellationToken)
    {
        return _context.FinancialYears
            .Include(x => x.Transactions.Where(t => t.Id == transactionId))
            .FirstOrDefaultAsync(x => x.Transactions.Any(t => t.Id == transactionId), cancellationToken);
    }

    public void Add(FinancialYear financialYear)
    {
        _context.FinancialYears.Add(financialYear);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<FinancialYear?> GetMostRecentAsync(CancellationToken cancellationToken)
    {
        return _context.FinancialYears.OrderByDescending(x => x.StartDate).FirstOrDefaultAsync(cancellationToken);
    }

    public Task<FinancialYear?> GetFinancialYearByTransactionId(Guid transactionId, CancellationToken cancellationToken)
    {
        return _context.FinancialYears
            .SingleOrDefaultAsync(x => x.Transactions.Any(t => t.Id == transactionId), cancellationToken);
    }

    public Task<FinancialYear?> GetFinancialYearByDateAsync(DateTimeOffset dateTime, CancellationToken cancellationToken)
    {
        return _context.FinancialYears.SingleOrDefaultAsync(x => x.StartDate <= dateTime && x.EndDate >= dateTime,
            cancellationToken);
    }
}