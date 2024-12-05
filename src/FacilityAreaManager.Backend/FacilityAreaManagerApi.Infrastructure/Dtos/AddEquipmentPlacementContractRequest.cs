namespace FacilityAreaManagerApi.Infrastructure.Dtos
{
    public class AddEquipmentPlacementContractRequest
    {
        public string ProductionFacilityCode { get; set; } = string.Empty;
        public string ProcessEquipmentTypeCode { get; set; } = string.Empty;
        public int EquipmentQuantity { get; set; }
    }
}
