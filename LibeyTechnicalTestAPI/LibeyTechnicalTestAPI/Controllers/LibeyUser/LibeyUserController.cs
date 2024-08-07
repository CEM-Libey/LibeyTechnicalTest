using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult FindResponse(string documentNumber)
        {
            var row = _aggregate.FindResponse(documentNumber);
            return Ok(row);
        }
        [HttpPost]
        public IActionResult Create(UserUpdateorCreateCommand command)
        {
            return Ok(_aggregate.Create(command));
        }
        [HttpDelete]
        [Route("{documentNumber}")]
        public IActionResult Delete(string documentNumber)
        {
            return Ok(_aggregate.Delete(documentNumber));
        }
        [HttpGet("ChangeState/{documentNumber}")]
        public IActionResult Patch(string documentNumber)
        { 
            return Ok(_aggregate.State(documentNumber));
        }
        [HttpPut]
        public IActionResult Put(UserUpdateorCreateCommand command)
        {
            return Ok(_aggregate.Update(command));
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_aggregate.Get());
        }
        [HttpGet]
        [Route("getText/{texto}")]
        public IActionResult Get(string? texto) 
        {
            texto = texto == null ? "" : texto;
            return Ok(_aggregate.Get(texto));
        }
    }
}