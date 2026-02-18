using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
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
            var exists = _repository.FindResponse(command.DocumentNumber);
            if (!string.IsNullOrEmpty(exists.DocumentNumber))
            {
                throw new Exception("Usuario ya existe");
            }

            var user = new LibeyUser(
                command.DocumentNumber,
                command.DocumentTypeId,
                command.Name,
                command.FathersLastName,
                command.MothersLastName,
                command.Address,
                command.UbigeoCode,
                command.Phone,
                command.Email,
                command.Password
            );

            _repository.Create(user);
        }
        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var row = _repository.FindResponse(documentNumber);
            return row;
        }
        public void Update(UserUpdateorCreateCommand command)
        {
            var exists = _repository.FindResponse(command.DocumentNumber);
            if (string.IsNullOrEmpty(exists.DocumentNumber))
            {
                throw new Exception("Usuario no encontrado");
            }

            var user = new LibeyUser(
                command.DocumentNumber,
                command.DocumentTypeId,
                command.Name,
                command.FathersLastName,
                command.MothersLastName,
                command.Address,
                command.UbigeoCode,
                command.Phone,
                command.Email,
                command.Password
            );

            _repository.Update(user);
        }
        public void Delete(string documentNumber)
        {
            var exists = _repository.FindResponse(documentNumber);
            if (string.IsNullOrEmpty(exists.DocumentNumber))
            {
                throw new Exception("Usuario no encontrado");
            }

            _repository.Delete(documentNumber);
        }

        public IEnumerable<LibeyUserResponse> ListLibeyUsers()
        {
            return _repository.ListLibeyUsers();
        }
    }
}