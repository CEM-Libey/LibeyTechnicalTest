using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class ConfigurationAggregate : IConfigurationAggregate
    {
        private readonly IConfigurationRepository _repository;
        public ConfigurationAggregate(IConfigurationRepository repository)
        {
            _repository = repository;
        }
        public List<DocumentType> GetDocumentType()
        {
            return _repository.GetDocumentType();
        }

        public List<Province> GetProvince(string idRegion)
        {
           return _repository.GetProvince(idRegion);
        }

        public List<Region> GetRegion()
        {
           return _repository.GetRegion();
        }

        public List<Ubigeo> GetUbigeo(string idRegion, string idProvince)
        {
           return _repository.GetUbigeo(idRegion, idProvince);
        }
    }
}
