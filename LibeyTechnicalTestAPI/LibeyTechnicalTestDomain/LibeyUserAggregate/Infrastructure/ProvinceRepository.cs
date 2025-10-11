using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly Context _context;
        public ProvinceRepository(Context context)
        {
            _context = context;
        }
        public List<ProvinceResponse> FindResponse(string regionCode)
        {
            var q = from province in _context.Provinces.Where(x => x.RegionCode.Equals(regionCode))
                    select new ProvinceResponse()
                    {
                        ProvinceCode = province.ProvinceCode,
                        RegionCode = province.RegionCode,
                        ProvinceDescription = province.ProvinceDescription,
                    };
            var list = q.ToList();
            if (list.Any()) return list;
            else return new List<ProvinceResponse>();
        }
    }
}
