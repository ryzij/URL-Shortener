using System.Security.Claims;

namespace URL_Shortener.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static bool CompareId(this ClaimsPrincipal claimsPrincipal, int userId)
    {
        return int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out int id) &&
            id == userId;
    }
}