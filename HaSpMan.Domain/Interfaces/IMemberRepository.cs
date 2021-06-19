using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HaSpMan.Domain.Interfaces
{
   public interface IMemberRepository
   {
      Task<Member> GetById(Guid id);
      Task<Member> GetByEmail(string email);
      Task<IEnumerable<Member>> GetAllAsync();

      void Add(Member member);
      void Update(Member member);
      void Remove(Member member);
   }
}