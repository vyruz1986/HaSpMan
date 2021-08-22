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
        private readonly HaSpManContext _context;
        public MemberRepository(HaSpManContext context)
        {
            _context = context;

        }
        public void Add(Member member)
        {
            _context.Members.Add(member);
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetByEmail(string email)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<Member> GetById(Guid id)
        {
            return await _context.Members.FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Remove(Member member)
        {
            _context.Members.Remove(member);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}