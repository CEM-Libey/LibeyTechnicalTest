using Azure;
using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Common;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class LibeyUserAggregate : ILibeyUserAggregate
    {
        private readonly ILibeyUserRepository _repository;
        public LibeyUserAggregate(ILibeyUserRepository repository)
        {
            _repository = repository;
        }
        public ResponseMessage Create(UserUpdateorCreateCommand command)
        {
             LibeyUser libeyUser = new LibeyUser(
                command.documentNumber,
                command.documentTypeId,
                command.name,
                command.fathersLastName,
                command.mothersLastName,
                command.address,
                command.ubigeoCode,
                command.RegionCode,
                command.ProvinceCode,
                command.phone,
                command.email,
                
                new Encriptation().Encript(command.password)
                );
            return _repository.Create(libeyUser);
        }
        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var row = _repository.FindResponse(documentNumber);
            return row;
        }

        public ResponseMessage Delete(string documentNumber)
        {
            return _repository.Delete(documentNumber);
        }

        public List<LibeyUserResponse> Get()
        {
            return _repository.Get();
        }

        public List<LibeyUserResponse> Get(string texto)
        {
            return _repository.Get(texto);
        }

        public ResponseMessage State(string documentNumber)
        {
            return _repository.State(documentNumber);
        }

        public ResponseMessage Update(UserUpdateorCreateCommand command)
        {
            LibeyUser libeyUser = new LibeyUser(
                command.documentNumber,
                command.documentTypeId,
                command.name,
                command.fathersLastName,
                command.mothersLastName,
                command.address,
                command.ubigeoCode,
                command.RegionCode,
                command.ProvinceCode,
                command.phone,
                command.email,
                new Encriptation().Encript(command.password)    
                );
            return _repository.Update(libeyUser);
        }
    }
}