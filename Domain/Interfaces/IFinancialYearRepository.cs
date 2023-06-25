namespace Domain.Interfaces;

public interface IFinancialYearRepository
{
    Task<FinancialYear?> GetById(Guid id, CancellationToken cancellationToken);
    void Add(FinancialYear financialYear);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}