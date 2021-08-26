using System;

namespace Types
{
    public record AuditEvent(
        DateTimeOffset Timestamp,
        string PerformedBy,
        string Description
    );
}