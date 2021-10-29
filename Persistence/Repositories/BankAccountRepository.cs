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
        private readonly IDbContextFactory<HaSpManContext> _contextFactory;

        public BankAccountRepository(IDbContextFactory<HaSpManContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Add(BankAccount account)
        {
            var context = _contextFactory.CreateDbContext();
            context.BankAccounts.Add(account);
        }

        public async Task<IEnumerable<BankAccount>> GetAllAsync(CancellationToken ct)
        {
            var context = _contextFactory.CreateDbContext();
            return await context.BankAccounts.ToListAsync(ct);
        }

        public async Task<BankAccount> GetByIdAsync(Guid id, CancellationToken ct)
        {var context = _contextFactory.CreateDbContext();
            return await context.BankAccounts.FirstOrDefaultAsync(b => b.Id == id, ct);
        }

        public void Remove(BankAccount account)
        {
            var context = _contextFactory.CreateDbContext();
            context.BankAccounts.Remove(account);
        }

        public async Task SaveAsync(CancellationToken ct)
        {
            var context = _contextFactory.CreateDbContext();
            await context.SaveChangesAsync(ct);
        }
    }
}