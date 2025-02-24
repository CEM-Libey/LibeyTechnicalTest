namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record LibeyUserResponse
    {
        public string DocumentNumber { get; init; }
        public int DocumentTypeId { get; init; }
        public string DocumentTypeDescription { get; init; }
        public string Name { get; init; }
        public string FathersLastName { get; init; }
        public string MothersLastName { get; init; }
        public string Address { get; init; }
        public string RegionCode { get; init; }
        public string RegionDescription { get; init; }
        public string ProvinceCode { get; init; }
        public string ProvinceDescription { get; init; }
        public string UbigeoCode { get; init; }
        public string UbigeoDescription { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public bool Active { get; init; }
    }
}

