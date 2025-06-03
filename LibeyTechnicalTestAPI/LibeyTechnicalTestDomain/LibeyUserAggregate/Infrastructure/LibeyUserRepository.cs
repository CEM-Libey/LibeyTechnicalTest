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

        public void Update(LibeyUser libeyUser)
        {
            _context.LibeyUsers.Update(libeyUser);
            _context.SaveChanges();
        }

        public void Delete(string documentNumber)
        {
            var user = _context.LibeyUsers.FirstOrDefault(x => x.DocumentNumber == documentNumber);
            if (user != null)
            {
                user.Deactivate();
                _context.SaveChanges();
            }
        }

        public LibeyUser? GetByDocumentNumber(string documentNumber)
        {
            return _context.LibeyUsers.FirstOrDefault(x => x.DocumentNumber == documentNumber);
        }

        public bool ExistsByDocumentNumber(string documentNumber)
        {
            return _context.LibeyUsers.Any(x => x.DocumentNumber == documentNumber);
        }

        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var query = from libeyUser in _context.LibeyUsers.Where(x => x.DocumentNumber.Equals(documentNumber))
                        join ubigeo in _context.Ubigeos on libeyUser.UbigeoCode equals ubigeo.UbigeoCode into ubigeoGroup
                        from ubigeo in ubigeoGroup.DefaultIfEmpty()
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
                            RegionCode = ubigeo != null ? ubigeo.RegionCode : "",
                            ProvinceCode = ubigeo != null ? ubigeo.ProvinceCode : ""
                        };
            
            var list = query.ToList();
            if (list.Any()) return list.First();
            else return new LibeyUserResponse();
        }

        public UserListResponse GetUsers(UserSearchQuery query)
        {
            var queryable = _context.LibeyUsers.AsQueryable();

            // Aplicar filtros
            if (!string.IsNullOrWhiteSpace(query.DocumentNumber))
                queryable = queryable.Where(x => x.DocumentNumber.Contains(query.DocumentNumber));

            if (!string.IsNullOrWhiteSpace(query.Name))
                queryable = queryable.Where(x => x.Name.Contains(query.Name) || 
                                                x.FathersLastName.Contains(query.Name) || 
                                                x.MothersLastName.Contains(query.Name));

            if (!string.IsNullOrWhiteSpace(query.Email))
                queryable = queryable.Where(x => x.Email.Contains(query.Email));

            if (query.Active.HasValue)
                queryable = queryable.Where(x => x.Active == query.Active.Value);

            // Contar total
            var totalCount = queryable.Count();

            // Aplicar paginación
            var users = queryable
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(libeyUser => new LibeyUserResponse()
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
                    RegionCode = "",
                    ProvinceCode = ""
                })
                .ToList();

            return new UserListResponse
            {
                Users = users,
                TotalCount = totalCount,
                Page = query.Page,
                PageSize = query.PageSize
            };
        }

        public List<DocumentTypeResponse> GetDocumentTypes()
        {
            // Tipos de documento estáticos para el ejemplo
            return new List<DocumentTypeResponse>
            {
                new DocumentTypeResponse { Id = 1, Description = "DNI" },
                new DocumentTypeResponse { Id = 2, Description = "Carnet de Extranjería" },
                new DocumentTypeResponse { Id = 3, Description = "Pasaporte" },
                new DocumentTypeResponse { Id = 4, Description = "RUC" }
            };
        }

        public List<UbigeoResponse> GetUbigeos()
        {
            try
            {
                var ubigeos = (from u in _context.Ubigeos
                              join p in _context.Provinces on u.ProvinceCode equals p.ProvinceCode
                              join r in _context.Regions on u.RegionCode equals r.RegionCode
                              select new UbigeoResponse
                              {
                                  UbigeoCode = u.UbigeoCode,
                                  ProvinceCode = u.ProvinceCode,
                                  RegionCode = u.RegionCode,
                                  Description = r.RegionDescription + " - " + p.ProvinceDescription + " - " + u.UbigeoCode,
                                  ProvinceDescription = p.ProvinceDescription,
                                  RegionDescription = r.RegionDescription
                              })
                              .OrderBy(x => x.RegionDescription)
                              .ThenBy(x => x.ProvinceDescription)
                              .ToList();

                return ubigeos;
            }
            catch
            {
                // Fallback en caso de error con la consulta
                return new List<UbigeoResponse>
                {
                    new UbigeoResponse 
                    { 
                        UbigeoCode = "150101", 
                        ProvinceCode = "1501", 
                        RegionCode = "15",
                        Description = "Lima - Lima - Lima",
                        ProvinceDescription = "Lima",
                        RegionDescription = "Lima"
                    }
                };
            }
        }
    }
}