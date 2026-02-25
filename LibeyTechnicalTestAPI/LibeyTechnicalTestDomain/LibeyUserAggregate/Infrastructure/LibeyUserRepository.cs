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

        // AGREGADO: Buscar la entidad para poder editarla
        public LibeyUser Find(string documentNumber)
        {
            return _context.LibeyUsers.FirstOrDefault(x => x.DocumentNumber == documentNumber);
        }

        // AGREGADO: Actualizar
        public void Update(LibeyUser libeyUser)
        {
            _context.LibeyUsers.Update(libeyUser);
            _context.SaveChanges();
        }

        // AGREGADO: Eliminar (físico o lógico, aquí actualizamos el estado)
        public void Delete(LibeyUser libeyUser)
        {
            _context.LibeyUsers.Update(libeyUser); // Si es soft delete
            // _context.LibeyUsers.Remove(libeyUser); // Si fuera delete físico
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
                        Phone = libeyUser.Phone,
                        UbigeoCode = libeyUser.UbigeoCode // Faltaba mapear esto
                    };
            var list = q.ToList();
            if (list.Any()) return list.First();
            else return null; // Mejor retornar null si no existe para manejar 404
        }
        public IEnumerable<LibeyUserResponse> GetAll()
        {
            return _context.LibeyUsers
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
                    Password = "", // Nunca envíes la contraseña al frontend
                    Phone = libeyUser.Phone,
                    UbigeoCode = libeyUser.UbigeoCode
                }).ToList();
        }
    }
}
