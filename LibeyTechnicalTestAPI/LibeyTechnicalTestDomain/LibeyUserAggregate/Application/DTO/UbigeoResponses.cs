using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record UbigeoResponse
    {
        public string UbigeoCode { get; init; }
        public string ProvinceCode { get; init; }
        public string RegionCode { get; init; }
        public string UbigeoDescription { get; init; }
    }
}
