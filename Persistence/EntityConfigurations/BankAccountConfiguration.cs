using Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Types;

namespace Persistence.EntityConfigurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(EntityTypeBuilder<BankAccount> builder)
    {
        builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(100);
        builder.Property(p => p.AccountNumber).HasColumnType("varchar").HasMaxLength(34);

        builder.OwnsMany(p => p.AuditEvents, AuditEventConfiguration("BankAccount_AuditEvents"));

        builder.HasOne(a => a.Totals).WithOne();
    }

    private static Action<OwnedNavigationBuilder<BankAccount, AuditEvent>> AuditEventConfiguration(string tableName)
    {
        return cfg =>
        {
            cfg.ToTable(tableName);
            cfg.Property(e => e.PerformedBy).HasColumnType("varchar").HasMaxLength(100);
            cfg.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(1000);
        };
    }
}
