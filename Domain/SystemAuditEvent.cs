
using Types;

namespace Domain;

public record SystemAuditEvent : AuditEvent
{
    public SystemAuditEvent(DateTimeOffset timestamp, string performedBy, string description) : base(timestamp, performedBy, description)
    {
    }
}
