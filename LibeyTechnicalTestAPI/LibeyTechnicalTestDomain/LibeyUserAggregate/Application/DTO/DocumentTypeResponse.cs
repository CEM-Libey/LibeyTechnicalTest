namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record DocumentTypeResponse
    {
        public int DocumentTypeId { get; init; }
        public string DocumentTypeDescription { get; init; }
    }
}
