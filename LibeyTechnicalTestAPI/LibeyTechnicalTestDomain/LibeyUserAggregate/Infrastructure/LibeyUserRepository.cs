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

        public void Delete(string documentNumber)
        {
            var existLibeyUser = _context.LibeyUsers.FirstOrDefault(x => x.DocumentNumber.Equals(documentNumber));

            if (existLibeyUser != null)
            {
                _context.LibeyUsers.Remove(existLibeyUser);

                _context.SaveChanges();
            }
        }

        public List<LibeyUserResponse> FindAll()
        {
            var q = from libeyUser in _context.LibeyUsers
                    join ubigeo in _context.Ubigeos on libeyUser.UbigeoCode equals ubigeo.UbigeoCode
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
                        Phone = libeyUser.Phone,
                        UbigeoCode = libeyUser.UbigeoCode,
                        ProvinceCode = ubigeo.ProvinceCode,
                        RegionCode = ubigeo.RegionCode,
                    };
            var list = q.ToList();
            if (list.Any()) return list;
            else return new List<LibeyUserResponse>();
        }

        public LibeyUserResponse FindResponse(string documentNumber)
        {

            var q = from libeyUser in _context.LibeyUsers
                    join ubigeo in _context.Ubigeos on libeyUser.UbigeoCode equals ubigeo.UbigeoCode
                    where libeyUser.DocumentNumber.Equals(documentNumber)
                    select new LibeyUserResponse()
                    {
                        DocumentNumber = libeyUser.DocumentNumber,
                        DocumentTypeId = libeyUser.DocumentTypeId,
                        Name = libeyUser.Name,
                        FathersLastName = libeyUser.FathersLastName,
                        MothersLastName = libeyUser.MothersLastName,
                        Address = libeyUser.Address,
                        UbigeoCode = libeyUser.UbigeoCode,
                        ProvinceCode = ubigeo.ProvinceCode,
                        RegionCode = ubigeo.RegionCode,
                        Phone = libeyUser.Phone,
                        Email = libeyUser.Email,
                        Password = libeyUser.Password,
                        Active = libeyUser.Active,
                    };
            var list = q.ToList();
            if (list.Any()) return list.First();
            else return new LibeyUserResponse();
        }

        public void Update(LibeyUser libeyUser)
        {
            var existLibeyUser = _context.LibeyUsers.FirstOrDefault(x => x.DocumentNumber.Equals(libeyUser.DocumentNumber));

            if (existLibeyUser != null)
            {
                existLibeyUser.SetDocumentNumber(libeyUser.DocumentNumber);
                existLibeyUser.SetDocumentTypeId(libeyUser.DocumentTypeId);
                existLibeyUser.SetName(libeyUser.Name);
                existLibeyUser.SetFathersLastName(libeyUser.FathersLastName);
                existLibeyUser.SetMothersLastName(libeyUser.MothersLastName);
                existLibeyUser.SetAddress(libeyUser.Address);
                existLibeyUser.SetUbigeoCode(libeyUser.UbigeoCode);
                existLibeyUser.SetPhone(libeyUser.Phone);
                existLibeyUser.SetEmail(libeyUser.Email);
                existLibeyUser.SetPassword(libeyUser.Password);
                existLibeyUser.SetActive(libeyUser.Active);

                _context.SaveChanges();
            }
        }
    }
}