using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface IConfigurationAggregate
    {
        List<DocumentType> GetDocumentType();
        List<Region> GetRegion();
        List<Province> GetProvince(string idRegion);
        List<Ubigeo> GetUbigeo(string idRegion, string idProvince);
    }
}
