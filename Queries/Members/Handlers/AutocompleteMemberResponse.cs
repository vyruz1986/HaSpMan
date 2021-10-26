using System.Collections.Generic;

namespace Queries.Members.Handlers
{
    public record AutocompleteMemberResponse(IReadOnlyList<AutocompleteMember> Members);
}