using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class LibeyUserRepository : ILibeyUserRepository
    {
        private readonly Context _context;
        public LibeyUserRepository(Context context)
        {
            _context = context;
        }
        public void Create(LibeyUser libeyUser)
        {
            _context.LibeyUsers.Add(libeyUser);
            _context.SaveChanges();
        }

        public LibeyUserResponse FindResponse(string documentNumber)
        {

            var q = from libeyUser in _context.LibeyUsers.Where(x => x.DocumentNumber.Equals(documentNumber))
                    select new LibeyUserResponse()
                    {
                        DocumentNumber = libeyUser.DocumentNumber,
                        Active = libeyUser.Active,
                        Address = libeyUser.Address,
                        DocumentTypeId = libeyUser.DocumentTypeId,
                        Email = libeyUser.Email,
                        FathersLastName = libeyUser.FathersLastName,
                        MothersLastName = libeyUser.MothersLastName,
                        Name = libeyUser.Name,
                        Password = libeyUser.Password,
                        Phone = libeyUser.Phone
                    };
            var list = q.ToList();
            if (list.Any()) return list.First();
            else return new LibeyUserResponse();
        }

        // NUEVO AGREGADO

        public List<LibeyUserResponse> GetAll(string? search)
        {

            return _context.LibeyUsers
                .Where(x => x.Active)
                .Select(libeyUser => new LibeyUserResponse
                {
                    DocumentNumber = libeyUser.DocumentNumber,
                    DocumentTypeId = libeyUser.DocumentTypeId,
                    Name = libeyUser.Name,
                    FathersLastName = libeyUser.FathersLastName,
                    MothersLastName = libeyUser.MothersLastName,
                    Address = libeyUser.Address,

                    //RegionCode = libeyUser.RegionCode,
                    //ProvinceCode = libeyUser.ProvinceCode,

                    UbigeoCode = libeyUser.UbigeoCode,
                    Phone = libeyUser.Phone,
                    Email = libeyUser.Email,
                    Password = libeyUser.Password,
                    Active = libeyUser.Active
                }).ToList();
        }

        public void Update(LibeyUser libeyUser)
        {
            var existingUser = _context.LibeyUsers.FirstOrDefault(x => x.DocumentNumber == libeyUser.DocumentNumber);
            if (existingUser != null)
            {
                _context.Entry(existingUser).CurrentValues.SetValues(libeyUser);
                _context.SaveChanges();
            }
        }

        public void Delete(string documentNumber)
        {
            var user = _context.LibeyUsers.FirstOrDefault(x => x.DocumentNumber == documentNumber);
            if (user != null)
            {
                user.GetType().GetProperty("Active")?.SetValue(user, false);
                _context.SaveChanges();
            }
        }
    }
}