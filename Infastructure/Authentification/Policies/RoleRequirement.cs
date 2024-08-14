using Microsoft.AspNetCore.Authorization;

namespace Events.Infastructure.Authentification.Policies
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public RoleRequirement() { }
    }
}
