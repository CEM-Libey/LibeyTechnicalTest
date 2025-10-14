namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class Region
    {
        public string RegionCode { get; set; }
        public string RegionDescription { get; set; }
        public Region(string regionCode, string regionDescription) { 
            RegionCode = regionCode;
            RegionDescription = regionDescription;
        }
    }
}
