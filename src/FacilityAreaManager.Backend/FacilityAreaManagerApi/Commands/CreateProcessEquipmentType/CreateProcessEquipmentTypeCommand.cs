using FacilityAreaManagerApi.Infrastructure.Dtos;
using MediatR;

namespace FacilityAreaManagerApi.Commands.CreateProcessEquipmentType
{
    public record CreateProcessEquipmentTypeCommand(AddProcessEquipmentTypeRequest Request) : IRequest<ProcessEquipmentTypeResponse>;
}
