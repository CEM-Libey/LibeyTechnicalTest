using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface IUbigeoRepository
    {
        Task<IEnumerable<RegionResponse>> ObtenerJerarquiaUbigeosAsync();

    }
}
