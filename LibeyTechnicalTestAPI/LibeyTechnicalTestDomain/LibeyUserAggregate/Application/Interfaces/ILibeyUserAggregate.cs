using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate
    {
        LibeyUserResponse FindResponse(string documentNumber);
        List<LibeyUserResponse> FindAll();
        void Create(UserUpdateorCreateCommand command);
        void Update(UserUpdateorCreateCommand libeyUser);
        void Delete(string documentNumber);
    }
}