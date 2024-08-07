
using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Common;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain.Configuration;
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
        public ResponseMessage Create(LibeyUser libeyUser)
        {
            var validateLibeyUser=_context.LibeyUsers.FirstOrDefault(u => u.DocumentNumber == libeyUser.DocumentNumber);

            if (validateLibeyUser!=null)
            {
                
            }
            try
            {
                _context.LibeyUsers.Add(libeyUser);
                _context.SaveChanges();
                return new ResponseMessage(MessageType.success.ToString(), new configuration().Add("LibeyUser"));
            }
            catch (Exception)
            {
                return new ResponseMessage(MessageType.error.ToString(), new configuration().ErrorAdd("LibeyUser"));
            }
           
        }
        public LibeyUserResponse FindResponse(string documentNumber)
        {
            var q = from libeyUser in _context.LibeyUsers
                    join region in _context.Region on libeyUser.RegionCode equals region.RegionCode
                    join province in _context.Province on libeyUser.ProvinceCode equals province.ProvinceCode
                    join ubigeo in _context.Ubigeo on libeyUser.UbigeoCode equals ubigeo.UbigeoCode
                    where libeyUser.DocumentNumber == documentNumber
                    
                    //.Where(x => x.DocumentNumber.Equals(documentNumber))
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
                        UbigeoCode = ubigeo.UbigeoCode,
                        ProvinceCode = province.ProvinceCode,
                        RegionCode = region.RegionCode
                    };
            var list = q.ToList();
            if (list.Any()) return list.First();
            else return new LibeyUserResponse();
        }
        public ResponseMessage Delete(string documentNumber)
        {
            var validateLibeyUser = _context.LibeyUsers.FirstOrDefault(u => u.DocumentNumber == documentNumber);
            if (validateLibeyUser == null)
            {
                return new ResponseMessage(MessageType.error.ToString(), new configuration().ErrorDelete("LibeyUser"));
            }
            try
            {
                _context.LibeyUsers.Remove(validateLibeyUser);
                _context.SaveChanges();
                return new ResponseMessage(MessageType.success.ToString(), new configuration().Delete("LibeyUser"));
            }
            catch (Exception)
            {
                return new ResponseMessage(MessageType.error.ToString(), new configuration().ErrorDelete("LibeyUser"));
            }
            
        }
        public List<LibeyUserResponse> Get()
        {
            var q = from libeyUser in _context.LibeyUsers
                    join region in _context.Region on libeyUser.RegionCode equals region.RegionCode
                    join province in _context.Province on libeyUser.ProvinceCode equals province.ProvinceCode
                    join ubigeo in _context.Ubigeo on libeyUser.UbigeoCode equals ubigeo.UbigeoCode
        
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
                        UbigeoCode = ubigeo.UbigeoDescription,
                        ProvinceCode = province.ProvinceDescription,
                        RegionCode = region.RegionDescription
                    };
            var list = q.ToList();
            return list;
        }

        public List<LibeyUserResponse> Get(string texto)
        {

            var q = from libeyUser in _context.LibeyUsers
                    join region in _context.Region on libeyUser.RegionCode equals region.RegionCode
                    join province in _context.Province on libeyUser.ProvinceCode equals province.ProvinceCode
                    join ubigeo in _context.Ubigeo on libeyUser.UbigeoCode equals ubigeo.UbigeoCode
                    where EF.Functions.Like(libeyUser.Name + libeyUser.DocumentNumber + libeyUser.Email + libeyUser.FathersLastName + libeyUser.MothersLastName, "%" + texto + "%")
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
                        UbigeoCode = ubigeo.UbigeoDescription,
                        ProvinceCode = province.ProvinceDescription,
                        RegionCode = region.RegionDescription
                    };
            var list = q.ToList();
            return list;

        }

        public ResponseMessage State(string documentNumber)
        {
            var validateLibeyUser = _context.LibeyUsers.FirstOrDefault(u => u.DocumentNumber == documentNumber);
            if (validateLibeyUser == null)
            {
                return new ResponseMessage(MessageType.error.ToString(), new configuration().ErrorChangeState("LibeyUser"));
            }
            try
            {
                validateLibeyUser.Active = validateLibeyUser.Active == false ? true : false;
                _context.SaveChanges();
                return new ResponseMessage(MessageType.success.ToString(), new configuration().ChangeState("LibeyUser"));

            }
            catch (Exception)
            {
                return new ResponseMessage(MessageType.error.ToString(), new configuration().ErrorChangeState("LibeyUser"));
            }
           

        }
        public ResponseMessage Update(LibeyUser libeyUser)
        {
            var validateLibeyUser = _context.LibeyUsers.FirstOrDefault(u => u.DocumentNumber == libeyUser.DocumentNumber);
            if (validateLibeyUser == null)
            {
                return new ResponseMessage(MessageType.error.ToString(), new configuration().ErrorUpdate("LibeyUser"));
            }
            try
            {
                validateLibeyUser.DocumentTypeId = libeyUser.DocumentTypeId;
                validateLibeyUser.Name = libeyUser.Name;
                validateLibeyUser.FathersLastName = libeyUser.FathersLastName;
                validateLibeyUser.MothersLastName = libeyUser.MothersLastName;
                validateLibeyUser.Address = libeyUser.Address;
                validateLibeyUser.UbigeoCode = libeyUser.UbigeoCode;
                validateLibeyUser.Phone = libeyUser.Phone;
                validateLibeyUser.Email = libeyUser.Email;
                validateLibeyUser.Password = libeyUser.Password;
                _context.SaveChanges();
                return new ResponseMessage(MessageType.success.ToString(), new configuration().Update("LibeyUser"));
            }
            catch (Exception)
            {
                return new ResponseMessage(MessageType.error.ToString(), new configuration().ErrorUpdate("LibeyUser"));
            }
           

        }
    }
}