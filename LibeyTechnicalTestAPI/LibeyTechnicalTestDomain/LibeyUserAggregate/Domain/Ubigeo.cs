using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Domain
{
    public class Ubigeo
    {
        public string UbigeoCode { get; internal set; }
        public string ProvinceCode { get; internal set; }
        public string RegionCode { get; internal set; }
        public string UbigeoDescription { get; internal set; }
    }
}
