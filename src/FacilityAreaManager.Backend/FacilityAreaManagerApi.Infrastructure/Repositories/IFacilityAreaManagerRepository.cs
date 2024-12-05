using FacilityAreaManagerApi.Infrastructure.Entities;

namespace FacilityAreaManagerApi.Infrastructure.Repositories
{
    public interface IFacilityAreaManagerRepository
    {
        public Task<EquipmentPlacementContract> CreateEquipmentPlacementContractAsync(EquipmentPlacementContract entity, CancellationToken cancellationToken);
        public Task<ProcessEquipmentType> CreateProcessEquipmentTypeAsync(ProcessEquipmentType entity, CancellationToken cancellationToken);
        public Task<ProductionFacility> CreateProductionFacilityAsync(ProductionFacility entity, CancellationToken cancellationToken);
        public Task<IEnumerable<EquipmentPlacementContract>> GetEquipmentPlacementContractsAsync(CancellationToken cancellationToken);
        public Task<ProcessEquipmentType?> GetProcessEquipmentTypeByCodeAsync(string code, CancellationToken cancellationToken);
        public Task<ProductionFacility?> GetProductionFacilityByCodeAsync(string code, CancellationToken cancellationToken);

    }
}