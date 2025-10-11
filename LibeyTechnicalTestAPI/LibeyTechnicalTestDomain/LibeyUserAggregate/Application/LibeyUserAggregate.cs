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
                command.Password
            );

            _repository.Create(libeyUser);
        }

        public void Delete(string documentNumber)
        {
            _repository.Delete(documentNumber);
        }

        public List<LibeyUserResponse> FindAll()
        {
            var list = _repository.FindAll();
            return list;
        }

        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var row = _repository.FindResponse(documentNumber);
            return row;
        }

        public void Update(UserUpdateorCreateCommand libeyUser)
        {
            LibeyUser updateUser = new LibeyUser(
                libeyUser.DocumentNumber,
                libeyUser.DocumentTypeId,
                libeyUser.Name,
                libeyUser.FathersLastName,
                libeyUser.MothersLastName,
                libeyUser.Address,
                libeyUser.UbigeoCode,
                libeyUser.Phone,
                libeyUser.Email,
                libeyUser.Password
            );

            _repository.Update(updateUser);
        }
    }
}