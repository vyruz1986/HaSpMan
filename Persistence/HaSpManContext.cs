using Domain;
using Domain.Views;
using Microsoft.EntityFrameworkCore;

using Persistence.Constants;

namespace Persistence;

public class HaSpManContext : DbContext
{
    public HaSpManContext()
    {
    }

    public HaSpManContext(DbContextOptions<HaSpManContext> options)
       : base(options)
    {
    }

    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<BankAccount> BankAccounts { get; set; } = null!;
    public DbSet<BankAccountsWithTotals> BankAccountsWithTotals { get; set; } = null!;

    public DbSet<FinancialYear> FinancialYears { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(x => x.MigrationsHistoryTable("__EFMigrationsHistory", Schema.HaSpMan));

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(Schema.HaSpMan);
        builder.ApplyConfigurationsFromAssembly(
           typeof(EntityConfigurations.MemberConfiguration).Assembly

        );
        base.OnModelCreating(builder);
    }
}
