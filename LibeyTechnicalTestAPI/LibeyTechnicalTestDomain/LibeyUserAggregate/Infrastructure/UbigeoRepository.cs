using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class UbigeoRepository : IUbigeoRepository
    {
        private readonly Context _context;
        public UbigeoRepository(Context context)
        {
            _context = context;
        }

        public Task<List<Ubigeo>> ListByCode(List<string> codes)
        {
            var ubigeos = _context.Ubigeos.Where(x => codes.Contains(x.UbigeoCode)).ToListAsync();
            return ubigeos;
        }

        public Task<List<Province>> ListProvinceByRegion(string RegionCode)
        {
            var provinces = _context.Provinces.Where(x => x.RegionCode == RegionCode).ToListAsync();
            return provinces;
        }

        public Task<List<Region>> ListRegion()
        {
            var regions = _context.Regions.ToListAsync();
            return regions;
        }

        public  Task<List<Ubigeo>> ListUbigeoByProvince(string ProvinceCode)
        {
            var ubigeos =  _context.Ubigeos.Where(x => x.ProvinceCode == ProvinceCode).ToListAsync();
            return ubigeos;
        }
    }
}
