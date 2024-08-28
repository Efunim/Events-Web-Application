using Events.Application.DTO.ResponseDTO;

namespace Events.Application.Interfaces.UseCases
{
    public interface IGetParticipantsPageUseCase
    {
        public Task<IEnumerable<ParticipantResponseDto>> ExecuteAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
    }
}
