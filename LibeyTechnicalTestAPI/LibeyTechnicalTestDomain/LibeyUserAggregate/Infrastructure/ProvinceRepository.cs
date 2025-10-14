using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class ProvinceRepository : IProvinceRepository
{
    private readonly Context _context;
    public ProvinceRepository(Context context)
    {
        _context = context;
    }
    public List<ProvinceResponse> GetAll()
    {
        return _context.Provinces
            .Select(p => new ProvinceResponse
            {
                ProvinceCode = p.ProvinceCode,
                RegionCode = p.RegionCode,
                ProvinceDescription = p.ProvinceDescription
            }).ToList();
    }
}
