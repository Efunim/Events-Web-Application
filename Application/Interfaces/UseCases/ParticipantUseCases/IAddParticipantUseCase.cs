using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IAddParticipantUseCase
    {
        public Task<int> ExecuteAsync(ParticipantRequestDto participantDto, CancellationToken cancellationToken);
    }
}
