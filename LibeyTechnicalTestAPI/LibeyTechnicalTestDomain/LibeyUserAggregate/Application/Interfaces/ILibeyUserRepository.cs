using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using System.Linq.Expressions;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserRepository
    {
        Task<LibeyUserDetailResponse> FindResponse(string documentNumber);
        Task<LibeyUser> AddAsync(LibeyUser entity);
        Task<LibeyUser> UpdateAsync(LibeyUser entity);
        Task DeleteAsync(LibeyUser entity);
        Task<int> GetCountAsync(Expression<Func<LibeyUser, bool>> predicate);
        Task<List<LibeyUser>> GetAllAsync(int Skip, int Take, int? DocumentTypeId, string? UbigeoCode, string? email, string? documentNumber, string? name, string? phone);
        Task<LibeyUser> GetByIdAsync(string documentNumber);
        Task<List<DocumentType>> FindDocumentTypes();
    }
}
