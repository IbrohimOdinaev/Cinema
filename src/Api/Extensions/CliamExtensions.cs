using System.Security.Claims;

namespace Cinema.Api.Extensions;

public static class ClaimExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirstValue(ClaimTypes.NameIdentifier);

        Guid userId;

        if (claim is null || !Guid.TryParse(claim, out userId))
            throw new UnauthorizedAccessException("UserId claim is missing");

        return userId;
    }
}
