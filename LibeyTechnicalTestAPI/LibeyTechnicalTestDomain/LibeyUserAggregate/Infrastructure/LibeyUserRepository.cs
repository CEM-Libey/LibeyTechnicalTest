using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
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

        public void Delete(LibeyUser libeyUser)
        {
            _context.LibeyUsers.Remove(libeyUser);
            _context.SaveChanges();
        }

        public LibeyUser? GetByDocumentNumber(string documentNumber)
        {
            return _context.LibeyUsers.FirstOrDefault(u => u.DocumentNumber == documentNumber);
        }

        public LibeyUserResponse? FindResponse(string documentNumber)
        {
            return _context.LibeyUsers
                   .Include(u => u.DocumentType)
                   .Include(u => u.Ubigeo)
                       .ThenInclude(ub => ub.Province)
                   .Include(u => u.Ubigeo)
                       .ThenInclude(ub => ub.Region)
                   .Where(u => u.DocumentNumber == documentNumber)
                   .Select(u => new LibeyUserResponse
                   {
                       DocumentNumber = u.DocumentNumber,
                       Active = u.Active,
                       Address = u.Address,
                       DocumentTypeId = u.DocumentTypeId,
                       DocumentTypeDescription = u.DocumentType.DocumentTypeDescription,
                       Email = u.Email,
                       FathersLastName = u.FathersLastName,
                       MothersLastName = u.MothersLastName,
                       Name = u.Name,
                       Password = u.Password,
                       Phone = u.Phone,
                       RegionCode = u.Ubigeo.RegionCode,
                       RegionDescription = u.Ubigeo.Region.RegionDescription,
                       ProvinceCode = u.Ubigeo.ProvinceCode,
                       ProvinceDescription = u.Ubigeo.Province.ProvinceDescription,
                       UbigeoCode = u.UbigeoCode,
                       UbigeoDescription = u.Ubigeo.UbigeoDescription
                   })
                   .FirstOrDefault();
        }
        public IEnumerable<LibeyUserResponse> GetAllResponses()
        {
            return _context.LibeyUsers
                   .Include(u => u.DocumentType)
                   .Include(u => u.Ubigeo)
                       .ThenInclude(ub => ub.Province)
                   .Include(u => u.Ubigeo)
                       .ThenInclude(ub => ub.Region)
                   .Select(u => new LibeyUserResponse
                   {
                       DocumentNumber = u.DocumentNumber,
                       Active = u.Active,
                       Address = u.Address,
                       DocumentTypeId = u.DocumentTypeId,
                       DocumentTypeDescription = u.DocumentType.DocumentTypeDescription,
                       Email = u.Email,
                       FathersLastName = u.FathersLastName,
                       MothersLastName = u.MothersLastName,
                       Name = u.Name,
                       Password = u.Password,
                       Phone = u.Phone,
                       RegionCode = u.Ubigeo.RegionCode,
                       RegionDescription = u.Ubigeo.Region.RegionDescription,
                       ProvinceCode = u.Ubigeo.ProvinceCode,
                       ProvinceDescription = u.Ubigeo.Province.ProvinceDescription,
                       UbigeoCode = u.UbigeoCode,
                       UbigeoDescription = u.Ubigeo.UbigeoDescription
                   })
                   .ToList();
        }


        public void Update(LibeyUser libeyUser)
        {
            _context.LibeyUsers.Update(libeyUser);
            _context.SaveChanges();
        }
    }
}