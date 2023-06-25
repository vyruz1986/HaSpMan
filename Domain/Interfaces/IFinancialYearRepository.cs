namespace Domain.Interfaces;

public interface IFinancialYearConfigurationRepository
{
    Task<FinancialYearConfiguration?> Get(CancellationToken cancellationToken);
    void Set(FinancialYearConfiguration financialYearConfiguration);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}

public interface IFinancialYearRepository
{
    Task<FinancialYear?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(FinancialYear financialYear);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<FinancialYear?> GetMostRecentAsync(CancellationToken cancellationToken);
}