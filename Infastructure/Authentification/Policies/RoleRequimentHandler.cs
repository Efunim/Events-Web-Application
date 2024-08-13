using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Events.Infastructure.Authentification.Policies
{
    public class RoleRequimentHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "Admin"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
