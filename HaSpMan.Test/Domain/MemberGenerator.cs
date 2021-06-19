using Bogus;
using HaSpMan.Domain;

namespace HaSpMan.Test.Domain

{
   public static class MemberGenerator
   {
      public static Faker<Member> Member => new Faker<Member>();
   }
}