using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IGetParticipantUseCase
    {
        public Task<ParticipantResponseDto> ExecuteAsync(int id, CancellationToken cancellationToken);
    }
}
