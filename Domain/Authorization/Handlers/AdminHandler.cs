﻿using Domain.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Domain.Authorization.Handlers
{
    public class AdminHandler : AuthorizationHandler<AdminRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            httpContext!.Request.Cookies.TryGetValue("admin", out string? value);

            if (value is null)
            {
                context.Fail();

                return Task.CompletedTask;
            }

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
