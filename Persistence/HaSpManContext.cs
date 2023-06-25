using Domain;

using Microsoft.EntityFrameworkCore;

using Persistence.Constants;
using Persistence.Views;

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
    
    public DbSet<FinancialYear> FinancialYears { get;set; } = null!;

    public DbSet<Transaction> Transactions { get; set; } = null!;

    public DbSet<FinancialYearConfiguration> FinancialYearConfigurations { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(x => x.MigrationsHistoryTable("__EFMigrationsHistory", Schema.HaSpMan));
        }
        
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
