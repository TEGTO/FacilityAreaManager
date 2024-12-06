using DatabaseControl.Repositories;
using FacilityAreaManagerApi.Infrastructure.Data;
using FacilityAreaManagerApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacilityAreaManagerApi.Infrastructure.Repositories
{
    public class FacilityAreaManagerRepository : IFacilityAreaManagerRepository
    {
        protected readonly IDatabaseRepository<FacilityAreaManagerDbContext> repository;

        public FacilityAreaManagerRepository(IDatabaseRepository<FacilityAreaManagerDbContext> repository)
        {
            this.repository = repository;
        }

        public async Task<EquipmentPlacementContract> CreateEquipmentPlacementContractAsync(EquipmentPlacementContract entity, CancellationToken cancellationToken)
        {
            return await repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ProcessEquipmentType> CreateProcessEquipmentTypeAsync(ProcessEquipmentType entity, CancellationToken cancellationToken)
        {
            return await repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ProductionFacility> CreateProductionFacilityAsync(ProductionFacility entity, CancellationToken cancellationToken)
        {
            return await repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ProcessEquipmentType?> GetProcessEquipmentTypeByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var queryable = await repository.GetQueryableAsync<ProcessEquipmentType>(cancellationToken).ConfigureAwait(false);
            return await queryable.FirstOrDefaultAsync(x => x.Code == code, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ProductionFacility?> GetProductionFacilityByCodeAsync(string code, CancellationToken cancellationToken)
        {
            var queryable = await repository.GetQueryableAsync<ProductionFacility>(cancellationToken).ConfigureAwait(false);
            return await queryable.FirstOrDefaultAsync(x => x.Code == code, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<EquipmentPlacementContract>> GetEquipmentPlacementContractsAsync(CancellationToken cancellationToken)
        {
            var queryable = await repository.GetQueryableAsync<EquipmentPlacementContract>(cancellationToken).ConfigureAwait(false);
            return await queryable.ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
