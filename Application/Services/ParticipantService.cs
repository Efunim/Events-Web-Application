using AutoMapper;
using Events.Application.DTO.RequestDTO;
using Events.Application.DTO.ResponseDTO;
using Events.Application.Exceptions;
using Events.Application.Interfaces.Repositories;
using Events.Application.Interfaces.Services;
using Events.Application.Validators;
using Events.Domain.Entities;

namespace Events.Application.Services
{
    public class ParticipantService(IUnitOfWork uow, IMapper mapper) : IParticipantService
    {
        private readonly IParticipantRepository repository = uow.ParticipantRepository;

        public async Task<ParticipantResponseDto> GetParticipantAsync(int id, CancellationToken cancellationToken)
        {
            var participant = await ServiceHelper.GetEntityAsync
                (repository.GetByIdAsync, id, cancellationToken);

            return mapper.Map<Participant, ParticipantResponseDto>(participant);
        }

        public async Task<IEnumerable<ParticipantResponseDto>> GetAllParticipantsAsync(CancellationToken cancellationToken)
        {
            var participants = await repository.GetAllAsync(cancellationToken);

            return mapper.Map<IEnumerable<Participant>, IEnumerable<ParticipantResponseDto>>(participants);
        }

        public async Task<int> InsertParticipantAsync(ParticipantRequestDto participantDto, CancellationToken cancellationToken)
        {
            await ValidateCategory(participantDto, cancellationToken);
            var participant = mapper.Map<ParticipantRequestDto, Participant>(participantDto);

            var id = await repository.InsertAsync(participant, cancellationToken);
            await uow.SaveAsync(cancellationToken);

            return id;
        }

        public async Task UpdateParticipantAsync(int id, ParticipantRequestDto participantDto, CancellationToken cancellationToken)
        {
            await ValidateCategory(participantDto, cancellationToken);

            var participant = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            mapper.Map(participantDto, participant);
            repository.Update(participant);

            await uow.SaveAsync(cancellationToken);
        }

        public async Task DeleteParticipantAsync(int id, CancellationToken cancellationToken)
        {
            var participant = await ServiceHelper.GetEntityAsync(repository.GetByIdAsync, id, cancellationToken);
            repository.Delete(participant);

            await uow.SaveAsync(cancellationToken);
        }

        private async Task ValidateCategory(ParticipantRequestDto participant, CancellationToken cancellationToken)
        {
            ParticipantRequestDtoValidator validator = new ParticipantRequestDtoValidator();
            var results = await validator.ValidateAsync(participant);
            if (!results.IsValid)
            {
                throw new ValidationException(results.Errors);
            }
        }
    }
}
