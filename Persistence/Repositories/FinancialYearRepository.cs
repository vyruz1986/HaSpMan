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
}