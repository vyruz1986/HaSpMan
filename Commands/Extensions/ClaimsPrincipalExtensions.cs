using System.Security.Claims;

using Commands.Constants;

namespace Commands.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetName(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Name);
    }
}
