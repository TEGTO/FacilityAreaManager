using FacilityAreaManagerApi.Infrastructure.Dtos;
using FluentValidation;

namespace FacilityAreaManagerApi.Infrastructure.Validators
{
    public class AddEquipmentPlacementContractRequestValidator : AbstractValidator<AddEquipmentPlacementContractRequest>
    {
        public AddEquipmentPlacementContractRequestValidator()
        {
            RuleFor(x => x.ProcessEquipmentTypeCode).NotNull().NotEmpty();
            RuleFor(x => x.ProductionFacilityCode).NotNull().NotEmpty();
            RuleFor(x => x.EquipmentQuantity).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
