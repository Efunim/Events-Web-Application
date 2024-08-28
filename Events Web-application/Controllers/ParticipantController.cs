using AutoMapper;
using Events.Application.DTO.BaseDTO;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Interfaces.UseCases;
using Events.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_application.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ParticipantController : ControllerBase
    {
        private readonly IAddParticipantUseCase _addUseCase;
        private readonly IDeleteParticipantUseCase _deleteUseCase;
        private readonly IGetParticipantUseCase _getUseCase;
        private readonly IGetParticipantsPageUseCase _getPageUseCase;
        private readonly IUpdateParticipantUseCase _updateUseCase;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ParticipantController(
        IAddParticipantUseCase addUseCase,
        IDeleteParticipantUseCase deleteUseCase,
        IGetParticipantUseCase getUseCase,
        IGetParticipantsPageUseCase getPageUseCase,
        IUpdateParticipantUseCase updateUseCase,
        IUnitOfWork uow,
        IMapper mapper)
        {
            _addUseCase = addUseCase;
            _deleteUseCase = deleteUseCase;
            _getUseCase = getUseCase;
            _getPageUseCase = getPageUseCase;
            _updateUseCase = updateUseCase;
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetParticipant")]
        public async Task<ParticipantResponseDto> GetParticipantAsync(int id, CancellationToken cancellationToken) =>
            await _getUseCase.ExecuteAsync(id, cancellationToken);

        [HttpGet(Name = "GetParticipantsPage")]
        public async Task<IEnumerable<ParticipantResponseDto>> GetParticipantsPageAsync(CancellationToken cancellationToken,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 2)
        {
            return await _getPageUseCase.ExecuteAsync(pageIndex, pageSize, cancellationToken);
        }

        [HttpPost(Name = "InsertParticipant")]
        [Authorize(Policy = "BeAdmin")]
        public async Task<int> InsertParticipantAsync(ParticipantRequestDto participantDto, CancellationToken cancellationToken)
        {
            int id = await _addUseCase.ExecuteAsync(participantDto, cancellationToken);
            await _uow.SaveAsync(cancellationToken);

            return id;
        }
        [HttpPut(Name = "UpdateParticipant")]
        [Authorize(Policy = "BeAdmin")]
        public async Task UpdateParticipantAsync(int id, ParticipantRequestDto participantDto, CancellationToken cancellationToken)
        {
            await _updateUseCase.ExecuteAsync(id, participantDto, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }

        [HttpDelete(Name = "DeleteParticipant")]
        [Authorize(Policy = "BeAdmin")]
        public async Task DeleteParticipantAsync(int id, CancellationToken cancellationToken)
        {
            await _deleteUseCase.ExecuteAsync(id, cancellationToken);

            await _uow.SaveAsync(cancellationToken);
        }
    }
}
