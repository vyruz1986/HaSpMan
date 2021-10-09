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
            builder.Property(x => x.CounterPartyName).IsRequired();
            builder.HasIndex(x => new { x.BankAccountId, x.Sequence }).IsUnique();
            builder.OwnsMany(x => x.Attachments, AttachmentConfiguration("Transaction_Attachments"));

        }

        private Action<OwnedNavigationBuilder<Transaction, TransactionAttachment>> AttachmentConfiguration(string tableName)
        {
            return cfg =>
            {
                cfg.ToTable(tableName);
                cfg.Property(x => x.Name).IsRequired();
                cfg.Property(x => x.BlobURI).IsRequired();
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