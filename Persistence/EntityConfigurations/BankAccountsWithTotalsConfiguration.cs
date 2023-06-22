using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Persistence.Views;

namespace Persistence.EntityConfigurations;

public class BankAccountsWithTotalsConfiguration : IEntityTypeConfiguration<BankAccountsWithTotals>
{
    public void Configure(EntityTypeBuilder<BankAccountsWithTotals> builder)
    {
        builder
            .ToView(BankAccountsWithTotals.ViewName)
            .HasKey(v => v.BankAccountId);

        builder.HasOne(p => p.Account).WithOne();
    }
}
