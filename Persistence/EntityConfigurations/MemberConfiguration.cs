using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Types;

namespace Persistence.EntityConfigurations
{
   public class MemberConfiguration : IEntityTypeConfiguration<Member>
   {
      public void Configure(EntityTypeBuilder<Member> builder)
      {
         builder.OwnsOne(p => p.Address).Property(p => p.Street).HasColumnType("varchar").HasMaxLength(200);
         builder.OwnsOne(p => p.Address).Property(p => p.City).HasColumnType("varchar").HasMaxLength(50);
         builder.OwnsOne(p => p.Address).Property(p => p.Country).HasColumnType("varchar").HasMaxLength(50);
         builder.OwnsOne(p => p.Address).Property(p => p.ZipCode).HasColumnType("varchar").HasMaxLength(10);
         builder.OwnsOne(p => p.Address).Property(p => p.HouseNumber).HasColumnType("varchar").HasMaxLength(5);

         builder.Property(p => p.FirstName).HasColumnType("varchar").HasMaxLength(50);
         builder.Property(p => p.LastName).HasColumnType("varchar").HasMaxLength(50);
         builder.Property(p => p.Email).HasColumnType("varchar").HasMaxLength(100);
         builder.Property(p => p.PhoneNumber).HasColumnType("varchar").HasMaxLength(50);
      }
   }
}