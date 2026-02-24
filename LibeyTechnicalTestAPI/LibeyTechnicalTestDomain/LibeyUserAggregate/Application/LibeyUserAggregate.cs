using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Domain;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application
{
    public class LibeyUserAggregate : ILibeyUserAggregate
    {
        private readonly ILibeyUserRepository _repository;
        private readonly IUbigeoRepository _ubigeoRepository;
        public LibeyUserAggregate(ILibeyUserRepository repository, IUbigeoRepository ubigeoRepository)
        {
            _repository = repository;
            _ubigeoRepository = ubigeoRepository;

        }
        public async Task<BaseResponseDTO> Delete(string documentNumber)
        {

            var exists = await _repository.GetByIdAsync(documentNumber);
            if (exists == null)
            {
                return new BaseResponseDTO($"El documento {documentNumber} no existe", false);
            }

            await _repository.DeleteAsync(exists);
            return new BaseResponseDTO($"Usuario Eliminado", true);

        }

        public Task<LibeyUserDetailResponse> FindResponse(string documentNumber)
        {
            var row = _repository.FindResponse(documentNumber);
            return row;
        }

        public async Task<List<LibeyUserResponse>> libeyUserResponses(LibeyUserQuery query)
        {
            var users = await _repository.GetAllAsync(query.Skip, query.Take, query.DocumentTypeId, query.UbigeoCode, query.email,
                query.documentNumber, query.Name, query.Phone);
            var ubigeosSelect = users.Select(x => x.UbigeoCode).Distinct().ToList();
            var ubigeosInfo = await _ubigeoRepository.ListByCode(ubigeosSelect);
            var ubigeoDict = ubigeosInfo.ToDictionary(x => x.UbigeoCode, x => new { x.ProvinceCode, x.RegionCode });
            var queryd = (from x in users
                          let ubiElement = ubigeoDict.TryGetValue(x.UbigeoCode, out var ubi) ? ubi : null
                          select new LibeyUserResponse
                          {
                              DocumentNumber = x.DocumentNumber,
                              DocumentTypeId = x.DocumentTypeId,
                              Name = x.Name,
                              FathersLastName = x.FathersLastName,
                              MothersLastName = x.MothersLastName,
                              Address = x.Address,
                              UbigeoCode = x.UbigeoCode,
                              RegionCode = ubiElement == null ? "Sin Region" : ubiElement.RegionCode,
                              ProvinceCode = ubiElement == null ? "Sin Provincia" : ubiElement.ProvinceCode,
                              Password = x.Password,
                              Phone = x.Phone,
                              Email = x.Email,
                              Active = x.Active,
                          }
                         ).ToList();
            return queryd;
        }

        public async Task<BaseResponseDTO> Update(UserUpdateorCreateCommand command)
        {

            var exists = await _repository.GetByIdAsync(command.DocumentNumber);
            if (exists == null)
            {
                return new BaseResponseDTO($"El documento {command.DocumentNumber} no existe", false);
            }
            bool hasChanges = exists.DocumentTypeId != command.DocumentTypeId ||
            exists.Name != command.Name ||
            exists.FathersLastName != command.FathersLastName ||
            exists.MothersLastName != command.MothersLastName ||
            exists.Address != command.Address ||
            exists.UbigeoCode != command.UbigeoCode ||
            exists.Phone != command.Phone ||
            exists.Email != command.Email ||
            exists.Password != command.Password;
            if (!hasChanges)
            {
                return new BaseResponseDTO("No se detectaron cambios", false);
            }
            exists.Update(command.DocumentTypeId, command.DocumentNumber, command.FathersLastName, command.MothersLastName, command.Address,
                command.UbigeoCode, command.Phone, command.Email, command.Password);
            await _repository.UpdateAsync(exists);
            return new BaseResponseDTO($"Usuario Actualizado", true);

        }

        public async Task<BaseResponseDTO> Create(UserUpdateorCreateCommand command)
        {

            var exists = await _repository.GetByIdAsync(command.DocumentNumber);
            if (exists != null)
            {
                return new BaseResponseDTO($"El documento {command.DocumentNumber} ya existe", false);
            }
            var create = new LibeyUser(command.DocumentNumber, command.DocumentTypeId, command.Name,
                command.FathersLastName, command.MothersLastName, command.Address, command.UbigeoCode, command.Phone,
                command.Email, command.Password);
            await _repository.AddAsync(create);
            return new BaseResponseDTO($"Usuario Creado", true);
        }

        public async Task<List<ComboDTO>> DocumentTypesCombo()
        {
            var types = await _repository.FindDocumentTypes();
            var combo = (from x in types
                         select new ComboDTO(x.DocumentTypeId.ToString(), x.DocumentTypeDescription)
                         ).ToList();
            return combo;
        }
    }
}