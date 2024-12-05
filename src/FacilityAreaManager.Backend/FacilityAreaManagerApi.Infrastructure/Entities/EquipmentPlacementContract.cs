using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityAreaManagerApi.Infrastructure.Entities
{
    public class EquipmentPlacementContract
    {
        [Key]
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        public string ProductionFacilityCode { get; set; } = default!;
        [ForeignKey("ProductionFacilityCode")]
        public ProductionFacility ProductionFacility { get; set; } = default!;
        [Required]
        public string ProcessEquipmentTypeCode { get; set; } = default!;
        [ForeignKey("ProcessEquipmentTypeCode")]
        public ProcessEquipmentType ProcessEquipmentType { get; set; } = default!;
    }
}
