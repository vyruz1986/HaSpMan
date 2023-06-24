using Domain;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class FinancialYearRepository : IFinancialYearRepository
{
    
    private readonly HaSpManContext _context;

    public FinancialYearRepository(IDbContextFactory<HaSpManContext> contextFactory)
    {

        _context = contextFactory.CreateDbContext();
    }

    public Task<FinancialYear?> GetById(Guid id, CancellationToken cancellationToken)
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
}

public interface IFinancialYearRepository
{
    Task<FinancialYear?> GetById(Guid id, CancellationToken cancellationToken);
    void Add(FinancialYear financialYear);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}