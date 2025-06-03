namespace LibeyTechnicalTestDomain.EFCore.Entities
{
    public class Province
    {
        public string ProvinceCode { get; set; }
        public string RegionCode { get; set; }
        public string ProvinceDescription { get; set; }
        
        // Navegación
        public Region Region { get; set; }
    }
} 