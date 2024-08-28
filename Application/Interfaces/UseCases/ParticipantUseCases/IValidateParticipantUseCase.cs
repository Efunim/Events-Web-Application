using Events.Application.DTO.RequestDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IValidateParticipantUseCase
    {
        public Task ExecuteAsync(ParticipantRequestDto participantDto, CancellationToken cancellationToken);
    }
}
