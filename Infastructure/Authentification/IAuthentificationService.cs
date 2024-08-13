using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Services;
using Events.Application;

namespace Events.Infastructure.Authentification
{
    public interface IAuthentificationService
    {
        Task<AuthenticateResponse?> AuthenticateAsync(UserRequestDto userRequest, CancellationToken cancellationToken);
        string HashPassword(string password);
    }
}
