using FacilityAreaManagerApi.Infrastructure.Dtos;
using MediatR;

namespace FacilityAreaManagerApi.Commands.GetAllEquipmentPlacementContracts
{
    public record GetAllEquipmentPlacementContractsQuery() : IRequest<IEnumerable<EquipmentPlacementContractResponse>>;
}
