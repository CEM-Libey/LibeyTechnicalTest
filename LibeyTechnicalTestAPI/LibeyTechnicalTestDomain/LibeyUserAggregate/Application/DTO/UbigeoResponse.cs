namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record DistritoResponse(string CodigoDistrito, string NombreDistrito);

    public record ProvinciaResponse(string CodigoProvincia, string NombreProvincia, List<DistritoResponse> Distritos);

    public record RegionResponse(string CodigoRegion, string NombreRegion, List<ProvinciaResponse> Provincias);

    public record UbigeoResponse(List<RegionResponse> Regiones);

}

