using Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FinancialYearConfigurationConfiguration : IEntityTypeConfiguration<FinancialYearConfiguration>
{
    public void Configure(EntityTypeBuilder<FinancialYearConfiguration> builder)
    {
        builder.Property(x => x.StartDate).IsRequired();
    }
}