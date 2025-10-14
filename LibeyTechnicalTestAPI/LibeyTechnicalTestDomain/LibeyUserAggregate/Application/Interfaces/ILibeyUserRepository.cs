using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserRepository
    {
        LibeyUserResponse FindResponse(string documentNumber);
        void Create(LibeyUser libeyUser);


        // Métodos agregados para CRUD completo
        List<LibeyUserResponse> GetAll(string? search);
        void Update(LibeyUser libeyUser);
        void Delete(string documentNumber);
            
    }
}
