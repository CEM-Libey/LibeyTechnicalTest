using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface IUbigeoRepository 
    {
        Task<List<Ubigeo>> ListByCode(List<string> codes);

        Task<List<Ubigeo>> ListUbigeoByProvince(string ProvinceCode);

        Task<List<Province>> ListProvinceByRegion(string RegionCode);

        Task<List<Region>> ListRegion();
    }
}
