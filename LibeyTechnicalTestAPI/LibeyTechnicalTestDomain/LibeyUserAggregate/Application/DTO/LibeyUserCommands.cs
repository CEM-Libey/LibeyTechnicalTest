namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record UserUpdateorCreateCommand
    {
        public string documentNumber { get; init; }
        public int documentTypeId { get; init; }
        public string name { get; init; }
        public string fathersLastName { get; init; }
        public string mothersLastName { get; init; }
        public string address { get; init; }
        public string ubigeoCode { get; init; }
        public string RegionCode { get; set; }
        public string ProvinceCode { get; set; }
        public string phone { get; init; }
        public string email { get; init; }
        public string password { get; init; }
    }
}