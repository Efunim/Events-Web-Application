using Events.Application.Interfaces.UseCases;
using Events.Infastructure.Authentification;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Events_Web_application.Infrastructure
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IGetUserUseCase getUseCase)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, getUseCase, token);
            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, IGetUserUseCase getUseCase, string token)
        {
            var cancellationToken = context.RequestAborted;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = AuthConfiguration.ISSUER,
                    ValidAudience = AuthConfiguration.AUDIENCE,
                    IssuerSigningKey = AuthConfiguration.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                context.Items["User"] = await getUseCase.ExecuteAsync(userId, cancellationToken);
            }
            catch
            {
                //Do nothing if JWT validation fails
                // user is not attached to context so the request won't have access to secure routes
            }
        }
    }
}
