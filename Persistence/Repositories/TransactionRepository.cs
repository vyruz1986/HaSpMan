using System;
using System.Collections.Generic;
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
        public async Task<Transaction> GetById(Guid id)
        {
            return await _haSpManContext.Transactions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _haSpManContext.Transactions.ToListAsync();
        }

        public void Add(Transaction member)
        {
            _haSpManContext.Transactions.Add(member);
        }

        public void Remove(Transaction member)
        {
            _haSpManContext.Transactions.Remove(member);
        }

        public async Task SaveAsync()
        {
            await _haSpManContext.SaveChangesAsync();
        }
    }
    public interface ITransactionRepository
    {
        Task<Transaction> GetById(Guid id);
        Task<IEnumerable<Transaction>> GetAllAsync();
        void Add(Transaction member);
        void Remove(Transaction member);
        Task SaveAsync();
    }
}