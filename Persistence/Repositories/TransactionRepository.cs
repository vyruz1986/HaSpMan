using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Domain;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HaSpManContext _haSpManContext;

        public TransactionRepository(HaSpManContext haSpManContext)
        {
            _haSpManContext = haSpManContext;
        }
        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            return await _haSpManContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _haSpManContext.Transactions.ToListAsync();
        }

        public async Task<int> GetLastTransactionForBankAccount(Guid bankAccountId)
        {
            var maxValue = await _haSpManContext.Transactions
                .Where(x => x.BankAccountId == bankAccountId)
                .MaxAsync(x => (int?)x.Sequence);
            return maxValue ?? 0;
        }

        public void AddRange(IEnumerable<Transaction> transactions)
        {
            _haSpManContext.Transactions.AddRange(transactions);
        }

        public void Add(Transaction member)
        {
            _haSpManContext.Transactions.Add(member);
        }

        public void Remove(Transaction member)
        {
            _haSpManContext.Transactions.Remove(member);
        }
        
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _haSpManContext.SaveChangesAsync(cancellationToken);
        }
    }
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<int> GetLastTransactionForBankAccount(Guid bankAccountId);

        void AddRange(IEnumerable<Transaction> transactions);
        void Add(Transaction member);
        void Remove(Transaction member);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}