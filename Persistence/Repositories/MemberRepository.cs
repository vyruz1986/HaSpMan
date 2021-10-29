using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Domain;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{

    public class MemberRepository : IMemberRepository
    {
        private readonly IDbContextFactory<HaSpManContext> _contextFactory;
        public MemberRepository(IDbContextFactory<HaSpManContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public void Add(Member member)
        {
            var context = _contextFactory.CreateDbContext();
            context.Members.Add(member);
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            var context = _contextFactory.CreateDbContext();
            return await context.Members.ToListAsync();
        }

        public async Task<Member> GetByEmail(string email)
        {var context = _contextFactory.CreateDbContext();
            return await context.Members.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<Member> GetById(Guid id)
        {var context = _contextFactory.CreateDbContext();
            return await context.Members.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Member member)
        {var context = _contextFactory.CreateDbContext();
            context.Members.Remove(member);
        }

        public async Task Save()
        {
            var context = _contextFactory.CreateDbContext();
            await context.SaveChangesAsync();
        }

    }
}