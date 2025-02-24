using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibeyTechnicalTestAPI.Controllers.Ubigeo
{
    [ApiController]
    [Route("[controller]")]
    public class UbigeoController : Controller
    {
        private readonly IUbigeoAggregate _ubigeoAggregate;

        public UbigeoController(IUbigeoAggregate ubigeoAggregate)
        {
            _ubigeoAggregate = ubigeoAggregate;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerJerarquiaUbigeos()
        {
            var response = await _ubigeoAggregate.ObtenerJerarquiaUbigeosAsync();
            return Ok(response); 
        }
    }
}
