using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ParticipantController(IParticipantService participantService) : ControllerBase
    {
        [HttpGet(Name = "GetParticipant")]
        public async Task<ParticipantResponseDto> GetParticipantAsync(int id, CancellationToken cancellationToken) =>
            await participantService.GetParticipantAsync(id, cancellationToken);

        [HttpGet(Name = "GetAllParticipants")]
        public async Task<IEnumerable<ParticipantResponseDto>> GetAllParticipantsAsync(CancellationToken cancellationToken) =>
            await participantService.GetAllParticipantsAsync(cancellationToken);

        [HttpGet(Name = "InsertParticipant")]
        public async Task<int> InsertParticipantAsync(ParticipantRequestDto participantDto, CancellationToken cancellationToken) =>
            await participantService.InsertParticipantAsync(participantDto, cancellationToken);

        [HttpGet(Name = "UpdateParticipant")]
        public async Task UpdateParticipantAsync(int id, ParticipantRequestDto participantDto, CancellationToken cancellationToken) =>
            await participantService.UpdateParticipantAsync(id, participantDto, cancellationToken);

        [HttpGet(Name = "DeleteParticipant")]
        public async Task DeleteParticipantAsync(int id, CancellationToken cancellationToken) =>
            await participantService.DeleteParticipantAsync(id, cancellationToken);
    }
}
