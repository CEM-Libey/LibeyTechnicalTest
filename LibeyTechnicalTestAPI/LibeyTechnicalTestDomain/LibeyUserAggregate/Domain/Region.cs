namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class Region
    {
        public string RegionCode { get; set; }
        public string RegionDescription { get; set; }
        public ICollection<Province> Provinces { get; set; } = new List<Province>();
        public ICollection<Ubigeo> Ubigeos { get; set; } = new List<Ubigeo>();

        public Region() { }
        public Region(string regionCode, string regionDescription)
        {
            RegionCode = regionCode;
            RegionDescription = regionDescription;
        }
    }
}