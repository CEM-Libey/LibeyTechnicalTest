namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class Province
    {
        public string ProvinceCode { get; set; }
        public string RegionCode { get; set; }
        public string ProvinceDescription { get; set; }
        public Region Region { get; set; }
        public ICollection<Ubigeo> Ubigeos { get; set; } = new List<Ubigeo>();

        public Province() { }

        public Province(string provinceCode, string regionCode, string provinceDescription)
        {
            ProvinceCode = provinceCode;
            RegionCode = regionCode;
            ProvinceDescription = provinceDescription;
        }
    }

}