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
            // Validaciones de negocio
            if (_repository.ExistsByDocumentNumber(command.DocumentNumber))
                throw new InvalidOperationException($"User with document number {command.DocumentNumber} already exists");

            // Crear entidad de dominio
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

            // Persistir
            _repository.Create(user);
        }

        public void Update(UserUpdateorCreateCommand command)
        {
            var user = _repository.GetByDocumentNumber(command.DocumentNumber);
            if (user == null)
                throw new InvalidOperationException($"User with document number {command.DocumentNumber} not found");

            // Usar método de dominio para actualizar
            user.UpdateProfile(
                command.Name,
                command.FathersLastName,
                command.MothersLastName,
                command.Address,
                command.UbigeoCode,
                command.Phone,
                command.Email
            );

            if (command.Password != user.Password)
            {
                user.ChangePassword(command.Password);
            }

            _repository.Update(user);
        }

        public void Delete(string documentNumber)
        {
            var user = _repository.GetByDocumentNumber(documentNumber);
            if (user == null)
                throw new InvalidOperationException($"User with document number {documentNumber} not found");

            // Soft delete - desactivar usuario
            user.Deactivate();
            _repository.Update(user);
        }

        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var row = _repository.FindResponse(documentNumber);
            return row;
        }

        public UserListResponse GetUsers(UserSearchQuery query)
        {
            return _repository.GetUsers(query);
        }

        public List<DocumentTypeResponse> GetDocumentTypes()
        {
            return _repository.GetDocumentTypes();
        }

        public List<UbigeoResponse> GetUbigeos()
        {
            return _repository.GetUbigeos();
        }
    }
} 