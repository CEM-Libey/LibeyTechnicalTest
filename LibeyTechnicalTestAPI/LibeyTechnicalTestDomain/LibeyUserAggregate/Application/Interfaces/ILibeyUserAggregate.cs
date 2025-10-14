using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate
    {
        LibeyUserResponse FindResponse(string documentNumber);
        void Create(UserUpdateorCreateCommand command);

        // Métodos agregados para CRUD completo
        List<LibeyUserResponse> GetAll(string? search);
        void Update(UserUpdateorCreateCommand command);
        void Delete(string documentNumber);
    }
}