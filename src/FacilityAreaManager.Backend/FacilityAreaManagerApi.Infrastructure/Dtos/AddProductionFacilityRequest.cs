﻿namespace FacilityAreaManagerApi.Infrastructure.Dtos
{
    public class AddProductionFacilityRequest
    {
        public string Name { get; set; } = string.Empty;
        public float StandardAreaForEquipment { get; set; }
    }
}
