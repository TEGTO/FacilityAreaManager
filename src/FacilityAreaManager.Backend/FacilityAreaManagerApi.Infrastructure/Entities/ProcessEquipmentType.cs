using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacilityAreaManagerApi.Infrastructure.Entities
{
    public class ProcessEquipmentType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Code { get; set; } = default!;
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = default!;
        [Required]
        [Range(0, float.MaxValue)]
        public float Area { get; set; }
    }
}
