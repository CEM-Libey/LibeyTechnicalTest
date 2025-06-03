namespace LibeyTechnicalTestDomain.EFCore.Entities
{
    public class Ubigeo
    {
        public string UbigeoCode { get; set; }
        public string ProvinceCode { get; set; }
        public string RegionCode { get; set; }
        
        // Navegaciones
        public Province Province { get; set; }
        public Region Region { get; set; }
    }
} 