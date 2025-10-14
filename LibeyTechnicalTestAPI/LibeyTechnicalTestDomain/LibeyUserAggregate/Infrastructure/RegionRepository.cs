using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class RegionRepository : IRegionRepository
{
    private readonly Context _context;
    public RegionRepository(Context context)
    {
        _context = context;
    }
    public List<RegionResponse> GetAll()
    {
        return _context.Regions
            .Select(r => new RegionResponse
            {
                RegionCode = r.RegionCode,
                RegionDescription = r.RegionDescription
            }).ToList();
    }
}
