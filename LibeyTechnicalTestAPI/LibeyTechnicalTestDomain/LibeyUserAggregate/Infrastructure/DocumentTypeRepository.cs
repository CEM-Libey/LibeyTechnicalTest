using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly Context _context;
        public DocumentTypeRepository(Context context)
        {
            _context = context;
        }

        public DocumentTypeResponse? FindResponse(string id)
        {
            return _context.DocumentType
                 .Where(dt => dt.DocumentTypeId.ToString() == id)
                 .Select(dt => new DocumentTypeResponse
                 {
                     Id = dt.DocumentTypeId,
                     Descripcion = dt.DocumentTypeDescription
                 })
                 .FirstOrDefault();
        }

        public IEnumerable<DocumentTypeResponse> GetAllResponses()
        {
            return _context.DocumentType
                 .OrderBy(dt => dt.DocumentTypeId)
                 .Select(dt => new DocumentTypeResponse
                 {
                     Id = dt.DocumentTypeId,
                     Descripcion = dt.DocumentTypeDescription
                 })
                 .ToList();
        }

        public DocumentType? GetByDocumentTypeId(string id)
        {

            return _context.DocumentType.FirstOrDefault(dt => dt.DocumentTypeId.ToString() == id);

        }
    }
}