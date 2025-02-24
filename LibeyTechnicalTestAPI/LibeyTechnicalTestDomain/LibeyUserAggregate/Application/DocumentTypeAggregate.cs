using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class DocumentTypeAggregate : IDocumentTypeAggregate
    {
        private readonly IDocumentTypeRepository _repository;
        public DocumentTypeAggregate(IDocumentTypeRepository repository)
        {
            _repository = repository;
        }

        DocumentTypeResponse? IDocumentTypeAggregate.FindResponse(string id)
        {
            return _repository.FindResponse(id);
        }


        IEnumerable<DocumentTypeResponse> IDocumentTypeAggregate.GetAll()
        {
            return _repository.GetAllResponses();
        }

    }
}