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
    public class RegionRepository : IRegionRepository
    {

        private readonly Context _context;
        public RegionRepository(Context context)
        {
            _context = context;
        }
        public List<RegionResponse> FindResponse()
        {
            var q = from region in _context.Regions
                    select new RegionResponse()
                    {
                        RegionCode = region.RegionCode,
                        RegionDescription = region.RegionDescription,
                    };
            var list = q.ToList();
            if (list.Any()) return list;
            else return new List<RegionResponse>();
        }
    }
}
