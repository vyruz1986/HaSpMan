using Domain;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class FinancialYearConfigurationRepository : IFinancialYearConfigurationRepository
{
    private readonly HaSpManContext _dbContext;

    public FinancialYearConfigurationRepository(HaSpManContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<FinancialYearConfiguration?> Get(CancellationToken cancellationToken)
    {
        return _dbContext.FinancialYearConfigurations.SingleOrDefaultAsync(cancellationToken);
    }

    public void Set(FinancialYearConfiguration financialYearConfiguration)
    {
        _dbContext.FinancialYearConfigurations.Update(financialYearConfiguration);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}