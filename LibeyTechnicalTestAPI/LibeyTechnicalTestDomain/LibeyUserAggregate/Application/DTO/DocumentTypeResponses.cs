using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record DocumentTypeResponse
    {
        public int DocumentTypeId { get; init; }
        public string DocumentTypeDescription { get; init; }
    }
}
