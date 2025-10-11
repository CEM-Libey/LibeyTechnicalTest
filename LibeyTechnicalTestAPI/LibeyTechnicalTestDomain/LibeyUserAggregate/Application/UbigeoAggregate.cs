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
        private readonly IUbigeoRepository _repository;
        public UbigeoAggregate(IUbigeoRepository repository)
        {
            _repository = repository;
        }

        public List<UbigeoResponse> FindResponse(string provinceCode, string RegionCode)
        {
            var list = _repository.FindResponse(provinceCode, RegionCode);
            return list;
        }
    }
}
