using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain.Configuration;


namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class ConfigurationRepository : IConfigurationRepository
    {

        private readonly Context _context;
        public ConfigurationRepository(Context context)
        {
            _context = context;
        }

        public List<DocumentType> GetDocumentType()
        {
            return _context.DocumentType.ToList();
        }

        public List<Province> GetProvince(string idRegion)
        {
            return _context.Province.Where(x=>x.RegionCode==idRegion).ToList();
        }

        public List<Region> GetRegion()
        {
            return _context.Region.ToList();
        }

        public List<Ubigeo> GetUbigeo(string idRegion, string idProvince)
        {
            return _context.Ubigeo.Where(x => x.RegionCode == idRegion && x.ProvinceCode==idProvince).ToList();
        }
    }
}
