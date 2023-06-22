using Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class CreditTransactionConfiguration : IEntityTypeConfiguration<CreditTransaction>
{

    public void Configure(EntityTypeBuilder<CreditTransaction> builder)
    {
        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.BankAccountId).IsRequired();
        builder.Property(x => x.DateFiled).IsRequired();
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.CounterPartyName)
            .IsRequired()
            .HasMaxLength(120);

        builder.OwnsMany(x => x.Attachments,
            AttachmentConfiguration("Transaction_Attachments"));
        builder.OwnsMany(x => x.TransactionTypeAmounts,
            TransactionTypeAmountConfiguration("Transaction_TransactionTypeAmounts"));

    }

    private static Action<OwnedNavigationBuilder<CreditTransaction, TransactionTypeAmount>> TransactionTypeAmountConfiguration(string tableName)
    {
        return cfg =>
        {
            cfg.ToTable(tableName);
            cfg.Property(x => x.Amount).IsRequired();
            cfg.Property(x => x.TransactionType).IsRequired();
        };
    }

    private static Action<OwnedNavigationBuilder<CreditTransaction, TransactionAttachment>> AttachmentConfiguration(string tableName)
    {
        return cfg =>
        {
            cfg.ToTable(tableName);
            cfg.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            cfg.Property(x => x.FullPath)
                .IsRequired()
                .HasMaxLength(1000);
        };
    }
}

public class DebitTransactionConfiguration : IEntityTypeConfiguration<DebitTransaction>
{

    public void Configure(EntityTypeBuilder<DebitTransaction> builder)
    {


        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.BankAccountId).IsRequired();
        builder.Property(x => x.DateFiled).IsRequired();
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.CounterPartyName)
            .IsRequired()
            .HasMaxLength(120);

        builder.OwnsMany(x => x.Attachments,
            AttachmentConfiguration("Transaction_Attachments"));
        builder.OwnsMany(x => x.TransactionTypeAmounts,
            TransactionTypeAmountConfiguration("Transaction_TransactionTypeAmounts"));

    }

    private static Action<OwnedNavigationBuilder<DebitTransaction, TransactionTypeAmount>> TransactionTypeAmountConfiguration(string tableName)
    {
        return cfg =>
        {
            cfg.ToTable(tableName);
            cfg.Property(x => x.Amount).IsRequired();
            cfg.Property(x => x.TransactionType).IsRequired();
        };
    }

    private static Action<OwnedNavigationBuilder<DebitTransaction, TransactionAttachment>> AttachmentConfiguration(string tableName)
    {
        return cfg =>
        {
            cfg.ToTable(tableName);
            cfg.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);
            cfg.Property(x => x.FullPath)
                .IsRequired()
                .HasMaxLength(1000);
        };
    }
}
