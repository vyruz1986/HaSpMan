
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Types;

namespace Persistence.EntityConfigurations;

//public class SystemAuditEventConfiguration : IEntityTypeConfiguration<AuditEvent>
//{
//    public void Configure(EntityTypeBuilder<AuditEvent> builder)
//    {
//        builder.ToTable("System_AuditEvents");
//        builder.Property(e => e.PerformedBy).HasColumnType("varchar").HasMaxLength(100);
//        builder.Property(e => e.Description).HasColumnType("varchar").HasMaxLength(1000);
//    }
//}
