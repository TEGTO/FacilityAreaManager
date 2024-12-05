using AutoMapper;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Entities;
using FacilityAreaManagerApi.Infrastructure.Repositories;
using MediatR;

namespace FacilityAreaManagerApi.Commands.CreateProductionFacility
{
    public class CreateProductionFacilityCommandHandler : IRequestHandler<CreateProductionFacilityCommand, ProductionFacilityResponse>
    {
        private readonly IFacilityAreaManagerRepository repository;
        private readonly IMapper mapper;

        public CreateProductionFacilityCommandHandler(IFacilityAreaManagerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<ProductionFacilityResponse> Handle(CreateProductionFacilityCommand command, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<ProductionFacility>(command.Request);

            var createdEntity = await repository.CreateProductionFacilityAsync(entity, cancellationToken);

            return mapper.Map<ProductionFacilityResponse>(createdEntity);
        }
    }
}
