using Domain.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BankAccountsWithTotalsConfiguration : IEntityTypeConfiguration<BankAccountsWithTotals>
{
    public void Configure(EntityTypeBuilder<BankAccountsWithTotals> builder)
    {
        builder
            .ToView(BankAccountsWithTotals.ViewName)
            .HasKey(v => v.BankAccountId);
    }
}
