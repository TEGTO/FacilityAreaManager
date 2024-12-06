using FacilityAreaManagerApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FacilityAreaManagerApi.Infrastructure.Data
{
    public class FacilityAreaManagerDbContext : DbContext
    {
        public virtual DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; }
        public virtual DbSet<ProcessEquipmentType> ProcessEquipmentTypes { get; set; }
        public virtual DbSet<ProductionFacility> ProductionFacilities { get; set; }

        public FacilityAreaManagerDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EquipmentPlacementContract>()
                .HasIndex(e => new { e.ProductionFacilityCode, e.ProcessEquipmentTypeCode })
                .IsUnique()
                .HasDatabaseName("IX_Unique_EquipmentPlacementContract");
        }
    }
}
