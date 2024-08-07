using Azure;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Common;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserRepository
    {
        LibeyUserResponse FindResponse(string documentNumber);
        List<LibeyUserResponse> Get();
        List<LibeyUserResponse> Get(string texto);
        ResponseMessage Create(LibeyUser libeyUser);
        ResponseMessage Update(LibeyUser libeyUser);
        ResponseMessage Delete(string documentNumber);
        ResponseMessage State(string documentNumber);
    }
}
