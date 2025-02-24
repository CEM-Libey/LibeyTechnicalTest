using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserRepository
    {
        LibeyUserResponse? FindResponse(string documentNumber);
        LibeyUser? GetByDocumentNumber(string documentNumber);
        IEnumerable<LibeyUserResponse> GetAllResponses();
        void Create(LibeyUser libeyUser);
        void Update(LibeyUser libeyUser);
        void Delete(LibeyUser libeyUser);
    }
}
