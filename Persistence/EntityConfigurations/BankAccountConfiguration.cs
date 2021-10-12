using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

using BankAccount = Domain.BankAccount;

namespace Persistence.EntityConfigurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<Domain.BankAccount> builder)
        {
            builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(p => p.AccountNumber).HasColumnType("varchar").HasMaxLength(34);
        }
    }
}