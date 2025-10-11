using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record RegionResponse
    {
        public string RegionCode { get; init; }
        public string RegionDescription { get; init; }
    }
}
