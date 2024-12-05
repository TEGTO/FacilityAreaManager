using FacilityAreaManagerApi.Infrastructure.Dtos;
using MediatR;

namespace FacilityAreaManagerApi.Commands.CreateEquipmentPlacementContract
{
    public record CreateEquipmentPlacementContractCommand(AddEquipmentPlacementContractRequest Request) : IRequest<EquipmentPlacementContractResponse>;
}
