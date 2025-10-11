using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly Context _context;

        public DocumentTypeRepository(Context context)
        {
            _context = context;
        }

        public List<DocumentTypeResponse> FindResponse()
        {
            var q = from documentType in _context.DocumentTypes
                    select new DocumentTypeResponse()
                    {
                        DocumentTypeId = documentType.DocumentTypeId,
                        DocumentTypeDescription = documentType.DocumentTypeDescription,
                    };

            var list = q.ToList();

            if (list.Any()) return list;
            else return new List<DocumentTypeResponse>();
        }
    }
}
