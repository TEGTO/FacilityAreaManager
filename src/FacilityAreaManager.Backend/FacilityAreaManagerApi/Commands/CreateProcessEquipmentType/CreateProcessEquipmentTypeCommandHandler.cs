using AutoMapper;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Entities;
using FacilityAreaManagerApi.Infrastructure.Repositories;
using MediatR;

namespace FacilityAreaManagerApi.Commands.CreateProcessEquipmentType
{
    public class CreateProcessEquipmentTypeCommandHandler : IRequestHandler<CreateProcessEquipmentTypeCommand, ProcessEquipmentTypeResponse>
    {
        private readonly IFacilityAreaManagerRepository repository;
        private readonly IMapper mapper;

        public CreateProcessEquipmentTypeCommandHandler(IFacilityAreaManagerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ProcessEquipmentTypeResponse> Handle(CreateProcessEquipmentTypeCommand command, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ProcessEquipmentType>(command.Request);

            var createdEntity = await repository.CreateProcessEquipmentTypeAsync(entity, cancellationToken);

            if (createdEntity == null)
            {
                throw new InvalidOperationException("Created entity is null!");
            }

            return mapper.Map<ProcessEquipmentTypeResponse>(createdEntity);
        }
    }
}
