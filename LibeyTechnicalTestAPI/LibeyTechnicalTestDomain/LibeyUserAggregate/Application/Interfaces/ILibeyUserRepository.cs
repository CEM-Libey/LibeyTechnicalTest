using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserRepository
    {
        IEnumerable<LibeyUserResponse> GetAll();
        LibeyUserResponse FindResponse(string documentNumber);
        LibeyUser Find(string documentNumber); // Nuevo: Para obtener la entidad
        void Create(LibeyUser libeyUser);
        void Update(LibeyUser libeyUser); // Nuevo
        void Delete(LibeyUser libeyUser); // Nuevo (o DeleteByDocumentNumber)
    }
}
