using FacilityAreaManagerApi.Commands.CreateEquipmentPlacementContract;
using FacilityAreaManagerApi.Commands.CreateProcessEquipmentType;
using FacilityAreaManagerApi.Commands.CreateProductionFacility;
using FacilityAreaManagerApi.Commands.GetAllEquipmentPlacementContracts;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FacilityAreaManagerApi.Controllers
{
    [ApiController]
    [Route("facilitycontracts")]
    public class FacilityContractsController : ControllerBase
    {
        private readonly IMediator mediator;

        public FacilityContractsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("contract")]
        public async Task<ActionResult<EquipmentPlacementContractResponse>> CreateEquipmentPlacementContract
            (AddEquipmentPlacementContractRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new CreateEquipmentPlacementContractCommand(request), cancellationToken);
            return Ok(response);
        }

        [HttpPost("equipment")]
        public async Task<ActionResult<ProcessEquipmentTypeResponse>> CreateProcessEquipmentType
            (AddProcessEquipmentTypeRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new CreateProcessEquipmentTypeCommand(request), cancellationToken);
            return Ok(response);
        }

        [HttpPost("facility")]
        public async Task<ActionResult<ProductionFacilityResponse>> CreateProductionFacility
            (AddProductionFacilityRequest request, CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new CreateProductionFacilityCommand(request), cancellationToken);
            return Ok(response);

        }

        [HttpGet("contracts")]
        public async Task<ActionResult<IEnumerable<EquipmentPlacementContractResponse>>> GetAllEquipmentPlacementContracts
            (CancellationToken cancellationToken)
        {
            var response = await mediator.Send(new GetAllEquipmentPlacementContractsQuery(), cancellationToken);
            return Ok(response);
        }
    }
}
