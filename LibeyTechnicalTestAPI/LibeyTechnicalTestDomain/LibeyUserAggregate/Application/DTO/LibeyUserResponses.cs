namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record LibeyUserResponse
    {
        public string DocumentNumber { get; init; }
        public int DocumentTypeId { get; init; }
        public string Name { get; init; }
        public string FathersLastName { get; init; }
        public string MothersLastName { get; init; }
        public string Address { get; init; }
        public string RegionCode { get; init; }
        public string ProvinceCode { get; init; }       
        public string UbigeoCode { get; init; }
        public string Phone { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public bool Active { get; init; }
    }

    public record UserListResponse
    {
        public List<LibeyUserResponse> Users { get; init; } = new();
        public int TotalCount { get; init; }
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }

    public record DocumentTypeResponse
    {
        public int Id { get; init; }
        public string Description { get; init; }
    }

    public record UbigeoResponse
    {
        public string UbigeoCode { get; init; }
        public string ProvinceCode { get; init; }
        public string RegionCode { get; init; }
        public string Description { get; init; }
        public string ProvinceDescription { get; init; }
        public string RegionDescription { get; init; }
    }
}