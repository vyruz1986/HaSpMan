using Domain.Views;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BankAccountTotalsConfiguration : IEntityTypeConfiguration<BankAccountTotals>
{
    public void Configure(EntityTypeBuilder<BankAccountTotals> builder)
    {
        builder.Property<Guid>("BankAccountId");

        builder
            .ToView(BankAccountTotals.ViewName)
            .HasKey("BankAccountId");
    }
}
