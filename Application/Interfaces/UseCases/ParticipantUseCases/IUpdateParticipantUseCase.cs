using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IUpdateParticipantUseCase
    {
        public Task ExecuteAsync(int id, ParticipantRequestDto participantDto, CancellationToken cancellationToken);
    }
}
