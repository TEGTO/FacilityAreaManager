using FacilityAreaManagerApi.Infrastructure.Dtos;
using MediatR;

namespace FacilityAreaManagerApi.Commands.CreateProductionFacility
{
    public record CreateProductionFacilityCommand(AddProductionFacilityRequest Request) : IRequest<ProductionFacilityResponse>;
}
