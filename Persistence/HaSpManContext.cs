using Domain;
using Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
   public class HaSpManContext : DbContext
   {
      public HaSpManContext(DbContextOptions<HaSpManContext> options)
         : base(options)
      {
      }
      public DbSet<Member> Members { get; set; } = null!;

      public DbSet<Transaction> Transactions { get; set; } = null!;

      protected override void OnModelCreating(ModelBuilder builder)
      {
         builder.HasDefaultSchema("HaSpMan");
         builder.ApplyConfigurationsFromAssembly(
            typeof(Persistence.EntityConfigurations.MemberConfiguration).Assembly
         );
         base.OnModelCreating(builder);
      }
   }
}