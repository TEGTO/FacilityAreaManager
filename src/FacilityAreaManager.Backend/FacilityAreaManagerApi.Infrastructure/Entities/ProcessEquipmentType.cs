using System.ComponentModel.DataAnnotations;

namespace FacilityAreaManagerApi.Infrastructure.Entities
{
    public class ProcessEquipmentType
    {
        [Key]
        [Required]
        public string Code { get; set; } = default!;
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = default!;
        [Required]
        [Range(0, double.MaxValue)]
        public float Area { get; set; }
    }
}
