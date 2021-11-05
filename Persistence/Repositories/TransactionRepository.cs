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
        private readonly HaSpManContext _context;

        public TransactionRepository(IDbContextFactory<HaSpManContext> haSpManContext)
        {
            _context = haSpManContext.CreateDbContext();
        }
        public async Task<Transaction> GetByIdAsync(Guid id)
        {
            return await _context.Transactions
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }
        
        public void AddRange(IEnumerable<Transaction> transactions)
        {
            _context.Transactions.AddRange(transactions);
        }

        public void Add(Transaction member)
        {
            _context.Transactions.Add(member);
        }

        public void Remove(Transaction member)
        {
            _context.Transactions.Remove(member);
        }
        
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
    public interface ITransactionRepository
    {
        Task<Transaction> GetByIdAsync(Guid id);
        Task<IEnumerable<Transaction>> GetAllAsync();
        void AddRange(IEnumerable<Transaction> transactions);
        void Add(Transaction member);
        void Remove(Transaction member);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}