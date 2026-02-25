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

        // IMPLEMENTADO: Antes lanzaba excepción
        public void Create(UserUpdateorCreateCommand command)
        {
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

        // AGREGADO: Lógica de actualización
        public void Update(string documentNumber, UserUpdateorCreateCommand command)
        {
            var user = _repository.Find(documentNumber);
            if (user != null)
            {
                user.Update(
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
        }

        // AGREGADO: Lógica de eliminación
        public void Delete(string documentNumber)
        {
            var user = _repository.Find(documentNumber);
            if (user != null)
            {
                user.Delete(); // Soft delete (Active = false)
                _repository.Delete(user);
            }
        }
        public IEnumerable<LibeyUserResponse> GetAll()
        {
            return _repository.GetAll();
        }
    }

}
