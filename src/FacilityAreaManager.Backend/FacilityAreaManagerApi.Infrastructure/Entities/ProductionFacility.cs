using System.ComponentModel.DataAnnotations;

namespace FacilityAreaManagerApi.Infrastructure.Entities
{
    public class ProductionFacility
    {
        [Key]
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = default!;
        [Required]
        [Range(0, double.MaxValue)]
        public float StandardAreaForEquipment { get; set; }
    }
}
