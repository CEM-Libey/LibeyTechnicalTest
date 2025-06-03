using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserRepository
    {
        LibeyUserResponse FindResponse(string documentNumber);
        void Create(LibeyUser libeyUser);
        void Update(LibeyUser libeyUser);
        void Delete(string documentNumber);
        LibeyUser? GetByDocumentNumber(string documentNumber);
        UserListResponse GetUsers(UserSearchQuery query);
        bool ExistsByDocumentNumber(string documentNumber);
        List<DocumentTypeResponse> GetDocumentTypes();
        List<UbigeoResponse> GetUbigeos();
    }
}
