using AutoMapper;
using FacilityAreaManagerApi.Infrastructure.Dtos;
using FacilityAreaManagerApi.Infrastructure.Entities;

namespace FacilityAreaManagerApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddEquipmentPlacementContractRequest, EquipmentPlacementContract>();
            CreateMap<AddProcessEquipmentTypeRequest, ProcessEquipmentType>();
            CreateMap<AddProductionFacilityRequest, ProductionFacility>();

            CreateMap<EquipmentPlacementContract, EquipmentPlacementContractResponse>();
            CreateMap<ProcessEquipmentType, ProcessEquipmentTypeResponse>();
            CreateMap<ProductionFacility, ProductionFacilityResponse>();
        }
    }
}
