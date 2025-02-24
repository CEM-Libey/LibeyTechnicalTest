using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface IDocumentTypeAggregate
    {
        DocumentTypeResponse? FindResponse(string id);
        IEnumerable<DocumentTypeResponse> GetAll();
    }
}
