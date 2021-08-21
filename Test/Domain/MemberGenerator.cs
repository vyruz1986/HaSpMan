using Bogus;
using Domain;

namespace Test.Domain

{
   public static class MemberGenerator
   {
      public static Faker<Member> Member => new Faker<Member>();
   }
}