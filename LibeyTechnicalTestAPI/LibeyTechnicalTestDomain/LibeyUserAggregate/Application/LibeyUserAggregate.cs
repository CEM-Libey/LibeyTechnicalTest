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
            var existingUser = _repository.GetByDocumentNumber(command.DocumentNumber);

            if (existingUser != null)
                throw new InvalidOperationException($"Ya existe un usuario con el número de documento {command.DocumentNumber}.");

            var newUser = new LibeyUser(
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


            _repository.Create(newUser);
        }



        public void Delete(string documentNumber)
        {

            var existingUser = _repository.GetByDocumentNumber(documentNumber)
                                 ?? throw new KeyNotFoundException($"No se encontró un usuario con el número de documento {documentNumber}.");
            
           
            _repository.Delete(existingUser);
        }

      
        public LibeyUserResponse? FindResponse(string documentNumber)
        {
            return _repository.FindResponse(documentNumber);
        }


        public IEnumerable<LibeyUserResponse> GetAll()
        {
            return _repository.GetAllResponses();
        }


        public void Update(UserUpdateorCreateCommand command)
        {

            var existingUser = _repository.GetByDocumentNumber(command.DocumentNumber)
                                 ?? throw new KeyNotFoundException($"Usuario con documento {command.DocumentNumber} no encontrado.");

            existingUser.UpdateLibeyUser(
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

            _repository.Update(existingUser);
        }

        public void Desactivate(string documentNumber)
        {
            var existingUser = _repository.GetByDocumentNumber(documentNumber)
                             ?? throw new KeyNotFoundException($"No se encontró un usuario con el número de documento {documentNumber}.");
            existingUser.DeactivateUser();
            _repository.Update(existingUser);
        }

        public void Activate(string documentNumber)
        {
            var existingUser = _repository.GetByDocumentNumber(documentNumber)
                              ?? throw new KeyNotFoundException($"No se encontró un usuario con el número de documento {documentNumber}.");
            existingUser.ActivateUser();
            _repository.Update(existingUser);
        }

    }
}