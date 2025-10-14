namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class Ubigeo {
        public string UbigeoCode { get; set; }
        public string ProvinceCode { get; set; }
        public string RegionCode { get; set; }
        public string UbigeoDescription { get; set; }
        public Ubigeo(string ubigeoCode, string provinceCode, string regionCode, string ubigeoDescription) {
            UbigeoCode = ubigeoCode;
            ProvinceCode = provinceCode;
            RegionCode = regionCode;
            UbigeoDescription = ubigeoDescription;
        }
    }
}