using AutoMapper;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Repositories;
using MediatR;

namespace FacilityAreaManagerApi.Commands.GetAllEquipmentPlacementContracts
{
    public class GetAllEquipmentPlacementContractsQueryHandler : IRequestHandler<GetAllEquipmentPlacementContractsQuery, IEnumerable<EquipmentPlacementContractResponse>>
    {
        private readonly IFacilityAreaManagerRepository repository;
        private readonly IMapper mapper;

        public GetAllEquipmentPlacementContractsQueryHandler(IFacilityAreaManagerRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<EquipmentPlacementContractResponse>> Handle(GetAllEquipmentPlacementContractsQuery request, CancellationToken cancellationToken)
        {
            var contracts = await repository.GetEquipmentPlacementContractsAsync(cancellationToken);

            return contracts.Select(mapper.Map<EquipmentPlacementContractResponse>);
        }
    }
}
