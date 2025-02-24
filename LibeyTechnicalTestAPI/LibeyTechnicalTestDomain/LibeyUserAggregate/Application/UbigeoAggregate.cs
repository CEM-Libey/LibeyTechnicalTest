using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class UbigeoAggregate : IUbigeoAggregate
    {
        private readonly IUbigeoRepository _ubigeoRepository;

        public UbigeoAggregate(IUbigeoRepository ubigeoRepository)
        {
            _ubigeoRepository = ubigeoRepository;
        }

        public async Task<UbigeoResponse> ObtenerJerarquiaUbigeosAsync()
        {
            var regiones = await _ubigeoRepository.ObtenerJerarquiaUbigeosAsync();
            return new UbigeoResponse(regiones.ToList());  
        }
    }
}