using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Domain;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly HaSpManContext _context;

        public BankAccountRepository(IDbContextFactory<HaSpManContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        public void Add(BankAccount account)
        {
            _context.BankAccounts.Add(account);
        }

        public async Task<IEnumerable<BankAccount>> GetAllAsync(CancellationToken ct)
        {
            return await _context.BankAccounts.ToListAsync(ct);
        }

        public async Task<BankAccount> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.BankAccounts.FirstOrDefaultAsync(b => b.Id == id, ct);
        }

        public void Remove(BankAccount account)
        {

            _context.BankAccounts.Remove(account);
        }

        public async Task SaveAsync(CancellationToken ct)
        {
            
            await _context.SaveChangesAsync(ct);
        }
    }
}