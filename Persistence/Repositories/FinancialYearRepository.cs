using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class FinancialYearRepository : IFinancialYearRepository
{
    
    private readonly HaSpManContext _context;

    public FinancialYearRepository(IDbContextFactory<HaSpManContext> contextFactory)
    {

        _context = contextFactory.CreateDbContext();
    }

    public void Add(Domain.FinancialYear financialYear)
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
    void Add(Domain.FinancialYear financialYear);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}