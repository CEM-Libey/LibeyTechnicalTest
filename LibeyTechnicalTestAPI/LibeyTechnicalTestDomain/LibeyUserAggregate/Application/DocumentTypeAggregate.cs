using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class DocumentTypeAggregate : IDocumentTypeAggregate
    {

        private readonly IDocumentTypeRepository _repository;
        
        public DocumentTypeAggregate(IDocumentTypeRepository repository)
        {
            _repository = repository;
        }
        public List<DocumentTypeResponse> FindResponse()
        {
            var list = _repository.FindResponse();
            return list;
        }
    }
}
