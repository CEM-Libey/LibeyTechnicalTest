using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO
{
    public record LibeyUserQuery(int Skip, int Take, int? DocumentTypeId, string? UbigeoCode, string? email, string? documentNumber, string? Name, string? Phone);

}
