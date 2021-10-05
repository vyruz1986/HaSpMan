using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBankAccountRepository
    {
        Task<BankAccount> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<BankAccount>> GetAllAsync(CancellationToken ct);
        void Add(BankAccount account);
        void Remove(BankAccount account);
        Task SaveAsync(CancellationToken ct);
    }
}