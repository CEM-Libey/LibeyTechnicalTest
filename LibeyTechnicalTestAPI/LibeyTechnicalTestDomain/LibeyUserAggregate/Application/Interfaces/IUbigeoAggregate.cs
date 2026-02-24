using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface IUbigeoAggregate
    {
        Task<List<ComboDTO>> comboUbigeo(string ProvinceCode);
        Task<List<ComboDTO>> comboProvince(string RegionCode);
        Task<List<ComboDTO>> comboRegion();
    }
}
