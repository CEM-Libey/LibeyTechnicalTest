using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate
    {
        LibeyUserResponse? FindResponse(string documentNumber);
        void Create(UserUpdateorCreateCommand command);
        void Update(UserUpdateorCreateCommand command);
        void Delete(string documentNumber);
        IEnumerable<LibeyUserResponse> GetAll();

        void Desactivate(string documentNumber);
        void Activate(string documentNumber);
    }
}
