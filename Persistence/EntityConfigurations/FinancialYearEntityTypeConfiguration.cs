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
        // Owned entity types cannot have inheritance hierarchies https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities#current-shortcomings
        builder.HasMany(x => x.Transactions).WithOne();
        builder.Navigation(x => x.Transactions).AutoInclude(); 
    }
}