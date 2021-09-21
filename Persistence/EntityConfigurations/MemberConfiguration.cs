using System;

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
            builder.OwnsOne(p => p.Address, AddressConfiguration());

            builder.Property(p => p.FirstName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(p => p.LastName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(p => p.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(p => p.PhoneNumber).HasColumnType("varchar").HasMaxLength(50);

            builder.OwnsMany(p => p.AuditEvents, AuditEventConfiguration("Member_AuditEvents"));
        }

        private static Action<OwnedNavigationBuilder<Member, Address>> AddressConfiguration()
        {
            return cfg =>
            {
                cfg.Property(p => p.Street).HasColumnType("varchar").HasMaxLength(200);
                cfg.Property(p => p.City).HasColumnType("varchar").HasMaxLength(50);
                cfg.Property(p => p.Country).HasColumnType("varchar").HasMaxLength(50);
                cfg.Property(p => p.ZipCode).HasColumnType("varchar").HasMaxLength(10);
                cfg.Property(p => p.HouseNumber).HasColumnType("varchar").HasMaxLength(15);
            };
        }

        private static Action<OwnedNavigationBuilder<Member, AuditEvent>> AuditEventConfiguration(string tableName)
        {
            return cfg =>
            {
                cfg.ToTable(tableName);
                cfg.Property(e => e.PerformedBy).HasColumnType("varchar").HasMaxLength(100);
                cfg.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(1000);
            };
        }
    }
}