namespace FacilityAreaManagerApi.Infrastructure.Dtos
{
    public class AddProductionFacilityRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float StandardAreaForEquipment { get; set; }
    }
}
