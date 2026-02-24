using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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
        public async Task<LibeyUser> AddAsync(LibeyUser entity)
        {
            _context.Set<LibeyUser>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(LibeyUser entity)
        {
            entity.Desactivate();
            _context.Set<LibeyUser>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<LibeyUserDetailResponse> FindResponse(string documentNumber)
        {
            var exists = await _context.LibeyUsers.FirstAsync(x => x.DocumentNumber == documentNumber);
            var type = await _context.DocumentTypes.FirstAsync(x => x.DocumentTypeId == exists.DocumentTypeId);
            var ubigeo = await _context.Ubigeos.FirstAsync(x => x.UbigeoCode == exists.UbigeoCode);
            var province = await _context.Provinces.FirstAsync(x => x.ProvinceCode == ubigeo.ProvinceCode);
            var region = await _context.Regions.FirstAsync(x => x.RegionCode == ubigeo.RegionCode);
            var result = new LibeyUserDetailResponse()
            {
                DocumentNumber = exists.DocumentNumber,
                Active = exists.Active,
                Address = exists.Address,
                DocumentTypeId = exists.DocumentTypeId,
                Email = exists.Email,
                FathersLastName = exists.FathersLastName,
                MothersLastName = exists.MothersLastName,
                Name = exists.Name,
                Password = exists.Password,
                Phone = exists.Phone,
                DocumentTypeDescription = type.DocumentTypeDescription,
                UbigeoCode = exists.UbigeoCode,
                UbigeoDescription = ubigeo.UbigeoDescription,
                ProvinceCode = ubigeo.ProvinceCode,
                RegionCode = ubigeo.RegionCode,
                ProvinceDescription = province.ProvinceDescription,
                RegionDescription = region.RegionDescription

            };
            return result;


        }

        public async Task<List<LibeyUser>> GetAllAsync(int Skip, int Take, int? DocumentTypeId, string? UbigeoCode,
            string? email, string? documentNumber, string? name, string? phone)
        {
            var users = await _context.LibeyUsers.Where(x => x.Active && (DocumentTypeId == null || x.DocumentTypeId == DocumentTypeId) &&
            (string.IsNullOrEmpty(UbigeoCode) || x.UbigeoCode == UbigeoCode) &&
            (string.IsNullOrEmpty(email) || x.Email.Contains(email)) &&
            (string.IsNullOrEmpty(documentNumber) || x.DocumentNumber.Contains(documentNumber)) &&
            (string.IsNullOrEmpty(phone) || x.Phone.Contains(phone)) &&
            (string.IsNullOrEmpty(name) || (x.Name + " " + x.MothersLastName + " " + x.FathersLastName).Contains(name)
            )
            ).Skip(Skip).Take(Take).ToListAsync();

            return users;
        }

        public async Task<LibeyUser> GetByIdAsync(string documentNumber)
        {
            return await _context.Set<LibeyUser>().FindAsync(documentNumber);
        }

        public async Task<int> GetCountAsync(Expression<Func<LibeyUser, bool>> predicate)
        {
            return await _context.Set<LibeyUser>().Where(predicate).CountAsync();
        }

        public async Task<LibeyUser> UpdateAsync(LibeyUser entity)
        {
            _context.Set<LibeyUser>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<DocumentType>> FindDocumentTypes()
        {
            var types = await _context.DocumentTypes.ToListAsync();
            return types;
        }
    }
}