namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class Ubigeo
    {
        public string UbigeoCode { get; set; }
        public string ProvinceCode { get; set; }
        public string RegionCode { get; set; }
        public string UbigeoDescription { get; set; }
        public Province Province { get; set; }
        public Region Region { get; set; }
        public ICollection<LibeyUser> LibeyUsers { get; set; } = new List<LibeyUser>();

        public Ubigeo() { }

        public Ubigeo(string ubigeoCode, string provinceCode, string regionCode, string ubigeoDescription)
        {
            UbigeoCode = ubigeoCode;
            ProvinceCode = provinceCode;
            RegionCode = regionCode;
            UbigeoDescription = ubigeoDescription;
        }
    }
}