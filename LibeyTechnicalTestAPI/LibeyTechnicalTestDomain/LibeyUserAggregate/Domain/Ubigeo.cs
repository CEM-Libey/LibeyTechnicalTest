using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class Ubigeo
    {
        public string UbigeoCode { get; private set; } = "";
        public string ProvinceCode { get; private set; } = "";
        public string RegionCode { get; private set; } = "";
        public string UbigeoDescription { get; private set; } = "";
    }
}
