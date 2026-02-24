using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces
{
    public interface ILibeyUserAggregate
    {
        Task<LibeyUserDetailResponse> FindResponse(string documentNumber);
        Task<BaseResponseDTO> Create(UserUpdateorCreateCommand command);
        Task<BaseResponseDTO> Update(UserUpdateorCreateCommand command);
        Task<BaseResponseDTO> Delete(string documentNumber);
        Task<List<LibeyUserResponse>> libeyUserResponses(LibeyUserQuery query);
        Task<List<ComboDTO>> DocumentTypesCombo();
    }
}