using Types;

namespace Domain.Extensions;

public static class AuditEventsExtensions
{
    public static ICollection<AuditEvent> AddEvent(this ICollection<AuditEvent> events, string description, string performedBy)
    {
        events.Add(new(
            DateTimeOffset.UtcNow,
            performedBy,
            description
        ));
        return events;
    }
}
