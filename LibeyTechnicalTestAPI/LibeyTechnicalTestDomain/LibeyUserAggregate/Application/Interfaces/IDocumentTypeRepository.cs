using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface IDocumentTypeRepository
    {
        DocumentTypeResponse? FindResponse(string id);
        DocumentType? GetByDocumentTypeId(string id);
        IEnumerable<DocumentTypeResponse> GetAllResponses();
    }
}
