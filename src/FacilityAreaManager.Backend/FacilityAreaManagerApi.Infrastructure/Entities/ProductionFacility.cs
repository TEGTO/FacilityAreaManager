using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityAreaManagerApi.Infrastructure.Entities
{
    public class ProductionFacility
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Code { get; set; } = default!;
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = default!;
        [Required]
        [Range(0, float.MaxValue)]
        public float StandardAreaForEquipment { get; set; }
    }
}
