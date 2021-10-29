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
        private readonly IDbContextFactory<HaSpManContext> _haSpManContext;

        public TransactionRepository(IDbContextFactory<HaSpManContext> haSpManContext)
        {
            _haSpManContext = haSpManContext;
        }
        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            var context = _haSpManContext.CreateDbContext();
            return await context.Transactions
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            var context = _haSpManContext.CreateDbContext();
            return await context.Transactions.ToListAsync();
        }

        public async Task<int> GetLastTransactionSequence()
        {
            var context = _haSpManContext.CreateDbContext();
            var maxValue = await context.Transactions          
                .MaxAsync(x => (int?)x.Sequence);
            return maxValue ?? 0;
        }

        public void AddRange(IEnumerable<Transaction> transactions)
        {
            var context = _haSpManContext.CreateDbContext();
            context.Transactions.AddRange(transactions);
        }

        public void Add(Transaction member)
        {
            var context = _haSpManContext.CreateDbContext();
            context.Transactions.Add(member);
        }

        public void Remove(Transaction member)
        {var context = _haSpManContext.CreateDbContext();
            context.Transactions.Remove(member);
        }
        
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            var context = _haSpManContext.CreateDbContext();
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllAsync();

        Task<int> GetLastTransactionSequence();

        void AddRange(IEnumerable<Transaction> transactions);
        void Add(Transaction member);
        void Remove(Transaction member);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}