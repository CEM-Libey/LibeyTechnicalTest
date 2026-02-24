using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace LibeyTechnicalTestAPI.Controllers.LibeyUser
{
    [ApiController]
    [Route("[controller]")]
    public class LibeyUserController : Controller
    {
        private readonly ILibeyUserAggregate _aggregate;
        public LibeyUserController(ILibeyUserAggregate aggregate)
        {
            _aggregate = aggregate;
        }
        [HttpGet]
        [Route("{documentNumber}")]
        [ProducesResponseType(typeof(LibeyUserDetailResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<LibeyUserDetailResponse>> FindResponse(string documentNumber)
        {
            var row = await _aggregate.FindResponse(documentNumber);
            return Ok(row);
        }
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseResponseDTO>> Create([FromBody] UserUpdateorCreateCommand command)
        {
            var result = await _aggregate.Create(command);
            return Ok(result);
        }
        [HttpPut]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseResponseDTO>> Update([FromBody] UserUpdateorCreateCommand command)
        {
            var result = await _aggregate.Update(command);
            return Ok(result);
        }
        [HttpDelete("{documentNumber}")]
        [ProducesResponseType(typeof(BaseResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BaseResponseDTO>> Delete(string documentNumber)
        {
            var result = await _aggregate.Delete(documentNumber);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<LibeyUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<LibeyUserResponse>>> List([FromQuery] LibeyUserQuery query)
        {
            var result = await _aggregate.libeyUserResponses(query);
            return Ok(result);
        }
        [HttpGet("typeCombo")]
        [ProducesResponseType(typeof(List<ComboDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ComboDTO>>> TypeCombo()
        {
            var result = await _aggregate.DocumentTypesCombo();
            return Ok(result);
        }
    }
}