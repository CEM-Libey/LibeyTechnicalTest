using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO; 
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface IDocumentTypeRepository
    {
        List<DocumentTypeResponse> GetAll();
    }
}
