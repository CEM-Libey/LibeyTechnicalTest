using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class UbigeoRepository : IUbigeoRepository
{
    private readonly Context _context;
    public UbigeoRepository(Context context)
    {
        _context = context;
    }
    public List<UbigeoResponse> GetAll()
    {
        return _context.Ubigeo
            .Select(r => new UbigeoResponse
            {
                UbigeoCode = r.UbigeoCode,
                ProvinceCode = r.ProvinceCode,
                RegionCode = r.RegionCode,
                UbigeoDescription = r.UbigeoDescription
            }).ToList();
    }
}
