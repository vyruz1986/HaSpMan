using System;

using Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;

using Types;

using BankAccount = Domain.BankAccount;

namespace Persistence.EntityConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.BankAccountId).IsRequired();
            builder.Property(x => x.DateFiled).IsRequired();
            builder.Property(x => x.TransactionType).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Sequence).IsRequired();
            builder.HasIndex(x => new { x.BankAccountId, x.Sequence }).IsUnique();
            builder.OwnsOne(x => x.CounterParty, CounterpartyConfiguration());
            builder.OwnsMany(x => x.Attachments, AttachmentConfiguration());

        }

        private Action<OwnedNavigationBuilder<Transaction, Transaction.TransactionAttachment>> AttachmentConfiguration()
        {
            return cfg =>
            {
                cfg.Property(x => x.Name).IsRequired();
                cfg.Property(x => x.BlobURI).IsRequired();
            };
        }

        private Action<OwnedNavigationBuilder<Transaction, CounterParty>> CounterpartyConfiguration()
        {
            return cfg =>
            {
                cfg.Property(x => x.Name).HasMaxLength(200);
            };
        }
    }
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<Domain.BankAccount> builder)
        {
            builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(100);
            builder.Property(p => p.AccountNumber).HasColumnType("varchar").HasMaxLength(34);
        }
    }
}