using HaSpMan.Domain;
using HaSpMan.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace HaSpMan.Persistence
{
   public class HaSpManContext : DbContext
   {
      public HaSpManContext(DbContextOptions<HaSpManContext> options)
         : base(options)
      {
      }
      public DbSet<Member> Members { get; set; }

      protected override void OnModelCreating(ModelBuilder builder)
      {
         builder.HasDefaultSchema(nameof(HaSpMan));
         builder.ApplyConfigurationsFromAssembly(
            typeof(HaSpMan.Persistence.EntityConfigurations.MemberConfiguration).Assembly
         );
         base.OnModelCreating(builder);
      }
   }
}