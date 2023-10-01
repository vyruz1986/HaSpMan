namespace Domain.Interfaces;

public interface IFinancialYearRepository
{
    Task<FinancialYear?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(FinancialYear financialYear);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<FinancialYear?> GetMostRecentAsync(CancellationToken cancellationToken);

    Task<FinancialYear?> GetFinancialYearByTransactionId(Guid transactionId, CancellationToken cancellationToken);
}