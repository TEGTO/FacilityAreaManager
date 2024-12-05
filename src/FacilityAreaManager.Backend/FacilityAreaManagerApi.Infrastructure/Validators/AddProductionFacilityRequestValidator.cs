using FacilityAreaManagerApi.Infrastructure.Dtos;
using FluentValidation;

namespace FacilityAreaManagerApi.Infrastructure.Validators
{
    public class AddProductionFacilityRequestValidator : AbstractValidator<AddProductionFacilityRequest>
    {
        public AddProductionFacilityRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.StandardAreaForEquipment).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
