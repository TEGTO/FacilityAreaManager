using AutoMapper;
using FacilityAreaManagerApi.BackgroundServices;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Entities;
using FacilityAreaManagerApi.Infrastructure.Repositories;
using MediatR;

namespace FacilityAreaManagerApi.Commands.CreateEquipmentPlacementContract
{
    public class CreateEquipmentPlacementContractCommandHandler : IRequestHandler<CreateEquipmentPlacementContractCommand, EquipmentPlacementContractResponse>
    {
        private readonly IFacilityAreaManagerRepository repository;
        private readonly IMapper mapper;
        private readonly IContractProcessingBackgroundService backgroundService;

        public CreateEquipmentPlacementContractCommandHandler(IFacilityAreaManagerRepository repository, IMapper mapper, IContractProcessingBackgroundService backgroundService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.backgroundService = backgroundService;
        }

        public async Task<EquipmentPlacementContractResponse> Handle(CreateEquipmentPlacementContractCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var facility = await repository.GetProductionFacilityByCodeAsync(request.ProductionFacilityCode, cancellationToken);

            if (facility == null)
            {
                throw new InvalidDataException($"Facility is not found with code '{request.ProductionFacilityCode}'");
            }

            var equipmentType = await repository.GetProcessEquipmentTypeByCodeAsync(request.ProcessEquipmentTypeCode, cancellationToken);

            if (equipmentType == null)
            {
                throw new InvalidDataException($"Equipment is not found with code '{request.ProcessEquipmentTypeCode}'");
            }

            var freeArea = facility.StandardAreaForEquipment;

            var requiredArea = request.EquipmentQuantity * equipmentType.Area;
            if (requiredArea > freeArea)
            {
                throw new InvalidOperationException(
                    $"Insufficient free area. Required: {requiredArea}, Available: {freeArea}.");
            }

            var entity = mapper.Map<EquipmentPlacementContract>(request);

            var createdEntity = await repository.CreateEquipmentPlacementContractAsync(entity, cancellationToken);

            backgroundService.EnqueueLog($"Contract {createdEntity.Code} has been created.");

            return mapper.Map<EquipmentPlacementContractResponse>(createdEntity);
        }
    }
}
