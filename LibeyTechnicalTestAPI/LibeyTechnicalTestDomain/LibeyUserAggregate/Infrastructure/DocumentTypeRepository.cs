using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class DocumentTypeRepository : IDocumentTypeRepository
{
    private readonly Context _context;
    public DocumentTypeRepository(Context context)
    {
        _context = context;
    }
    public List<DocumentTypeResponse> GetAll()
    {
        return _context.DocumentTypes
            .Select(dt => new DocumentTypeResponse
            {
                DocumentTypeId = dt.DocumentTypeId,
                DocumentTypeDescription = dt.DocumentTypeDescription
            }).ToList();
    }
}
