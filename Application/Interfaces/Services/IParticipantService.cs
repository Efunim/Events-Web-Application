using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.Services
{
    public interface IParticipantService
    {
        Task<ParticipantResponseDto> GetParticipantAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ParticipantResponseDto>> GetAllParticipantsAsync(CancellationToken cancellationToken);
        Task<int> InsertParticipantAsync(ParticipantRequestDto participantDto, CancellationToken cancellationToken);
        Task UpdateParticipantAsync(int id, ParticipantRequestDto ParticipantDto, CancellationToken cancellationToken);
        Task DeleteParticipantAsync(int id, CancellationToken cancellationToken);
    }
}
