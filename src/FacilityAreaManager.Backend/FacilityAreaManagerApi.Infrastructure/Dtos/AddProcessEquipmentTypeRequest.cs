namespace FacilityAreaManagerApi.Infrastructure.Dtos
{
    public class AddProcessEquipmentTypeRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
    }
}
