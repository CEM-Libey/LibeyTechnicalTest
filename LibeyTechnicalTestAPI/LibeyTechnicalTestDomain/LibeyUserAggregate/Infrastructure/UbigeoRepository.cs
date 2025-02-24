using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class UbigeoRepository : IUbigeoRepository
    {
        private readonly Context _context;

        public UbigeoRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RegionResponse>> ObtenerJerarquiaUbigeosAsync()
        {
            var regiones = await _context.Region
                .Include(r => r.Provinces)
                    .ThenInclude(p => p.Ubigeos)
                .Select(r => new RegionResponse(
                    r.RegionCode ?? "Sin código",
                    r.RegionDescription ?? "Sin nombre",
                    r.Provinces.Select(p => new ProvinciaResponse(
                        p.ProvinceCode ?? "Sin código",
                        p.ProvinceDescription ?? "Sin nombre",
                        p.Ubigeos.Select(d => new DistritoResponse(
                            d.UbigeoCode ?? "Sin código",
                            d.UbigeoDescription ?? "Sin nombre"
                        )).ToList()
                    )).ToList()
                )).ToListAsync();

            return regiones;
        }
    }
}