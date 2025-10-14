using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class LibeyUserAggregate : ILibeyUserAggregate
    {
        private readonly ILibeyUserRepository _repository;
        public LibeyUserAggregate(ILibeyUserRepository repository)
        {
            _repository = repository;
        }
        public void Create(UserUpdateorCreateCommand command)
        {
            var existingUser = _repository.FindResponse(command.DocumentNumber);
            if (existingUser != null && !string.IsNullOrEmpty(existingUser.DocumentNumber)) { 
                throw new Exception($"User con DocumentNumber {command.DocumentNumber} already exists.");
            }

            var libeyUser = new LibeyUser(command.DocumentNumber, command.DocumentTypeId, command.Name, command.FathersLastName, command.MothersLastName,
                command.Address, command.UbigeoCode, command.Phone, command.Email, command.Password);
            _repository.Create(libeyUser);
        }
        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var row = _repository.FindResponse(documentNumber);
            return row;
        }

        // NUEVO AGREGADO
        public List<LibeyUserResponse> GetAll(string? search)
        {
            var rows = _repository.GetAll(search);
            return rows;
        }

        public void Update(UserUpdateorCreateCommand command)
        {
            var existingUser = _repository.FindResponse(command.DocumentNumber);
            if(existingUser == null || string.IsNullOrEmpty(existingUser.DocumentNumber))
            {
                throw new Exception($"User con DocumentNumber {command.DocumentNumber} does not exist.");
            }

            LibeyUser libeyUser = new LibeyUser(
                command.DocumentNumber,
                command.DocumentTypeId,
                command.Name,
                command.FathersLastName,
                command.MothersLastName,
                command.Address,
                command.UbigeoCode,
                command.Phone,
                command.Email,
                command.Password);
            _repository.Update(libeyUser);
        }

        public void Delete(string documentNumber)
        {
            var existingUser = _repository.FindResponse(documentNumber);
            if (existingUser == null || string.IsNullOrEmpty(existingUser.DocumentNumber))
            {
                throw new Exception($"User con DocumentNumber {documentNumber} does not exist.");
            }
            _repository.Delete(documentNumber);
        }
    }
}