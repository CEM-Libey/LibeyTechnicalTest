using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Common;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate
    {
        LibeyUserResponse FindResponse(string documentNumber);
        List<LibeyUserResponse> Get();
        List<LibeyUserResponse> Get(string texto);
        ResponseMessage Create(UserUpdateorCreateCommand command);
        ResponseMessage Update(UserUpdateorCreateCommand command);
        ResponseMessage Delete(string documentNumber);
        ResponseMessage State(string documentNumber);
    }
}