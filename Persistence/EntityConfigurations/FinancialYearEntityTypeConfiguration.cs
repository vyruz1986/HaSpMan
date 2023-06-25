using Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FinancialYearEntityTypeConfiguration : IEntityTypeConfiguration<FinancialYear>
{
    public void Configure(EntityTypeBuilder<FinancialYear> builder)
    {
        builder.Property(x => x.StartDate).IsRequired();
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.IsClosed).IsRequired();
        builder.OwnsMany(x => x.Transactions);
    }
}