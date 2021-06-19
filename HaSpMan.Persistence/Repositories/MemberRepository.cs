using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HaSpMan.Domain;
using HaSpMan.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HaSpMan.Persistence.Repositories
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

      public void Update(Member member)
      {
         _context.Members.Update(member);
      }
   }
}