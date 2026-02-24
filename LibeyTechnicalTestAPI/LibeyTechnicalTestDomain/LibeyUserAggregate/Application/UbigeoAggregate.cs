using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class UbigeoAggregate : IUbigeoAggregate
    {
        private readonly IUbigeoRepository _ubigeoRepository;
        public UbigeoAggregate(IUbigeoRepository ubigeoRepository)
        {
            _ubigeoRepository = ubigeoRepository;

        }
        public async Task<List<ComboDTO>> comboProvince(string RegionCode)
        {
            var provinces = await _ubigeoRepository.ListProvinceByRegion(RegionCode);
            var provinceCombo = (from x in provinces
                                 select new ComboDTO(x.ProvinceCode, x.ProvinceDescription)

                                 ).ToList();
            return provinceCombo;
        }

        public async Task<List<ComboDTO>> comboRegion()
        {
            var regions = await _ubigeoRepository.ListRegion();
            var regionCombo = (from x in regions
                               select new ComboDTO(x.RegionCode, x.RegionDescription)

                                 ).ToList();
            return regionCombo;
        }

        public async Task<List<ComboDTO>> comboUbigeo(string ProvinceCode)
        {
            var ubigeos = await _ubigeoRepository.ListUbigeoByProvince(ProvinceCode);
            var ubigeosCombo = (from x in ubigeos
                                select new ComboDTO(x.UbigeoCode, x.UbigeoDescription)

                                 ).ToList();
            return ubigeosCombo;
        }
    }
}
