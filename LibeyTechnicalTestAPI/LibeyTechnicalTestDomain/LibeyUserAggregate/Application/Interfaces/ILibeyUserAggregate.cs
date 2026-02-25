using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate

    {
        IEnumerable<LibeyUserResponse> GetAll();
        LibeyUserResponse FindResponse(string documentNumber);
        void Create(UserUpdateorCreateCommand command);
        void Update(string documentNumber, UserUpdateorCreateCommand command); // Nuevo
        void Delete(string documentNumber); // Nuevo
    }
}
