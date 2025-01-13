using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization.Requirements
{
    public class AdminRequirement : IAuthorizationRequirement
    {
        public readonly string? CookieName;

        public AdminRequirement(string cookieName)
        {
            CookieName = cookieName;
        }
    }
}
