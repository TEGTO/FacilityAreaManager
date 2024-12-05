using FacilityAreaManagerApi.Infrastructure.Dtos;
using FluentValidation;

namespace FacilityAreaManagerApi.Infrastructure.Validators
{
    public class AddProcessEquipmentTypeRequestValidator : AbstractValidator<AddProcessEquipmentTypeRequest>
    {
        public AddProcessEquipmentTypeRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Area).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
