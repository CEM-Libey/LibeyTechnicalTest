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

        [HttpGet]
        [Route("all")]
        public IActionResult FindAll()
        {
            var list = _aggregate.FindAll();
            return Ok(list);
        }

        [HttpPost]       
        public IActionResult Create(UserUpdateorCreateCommand command)
        {
             _aggregate.Create(command);
            return Ok(true);
        }

        [HttpPatch]
        public IActionResult Update(UserUpdateorCreateCommand command)
        {
            _aggregate.Update(command);
            return Ok(true);
        }

        [HttpDelete]
        [Route("{documentNumber}")]
        public IActionResult Delete(string documentNumber)
        {
            _aggregate.Delete(documentNumber);
            return Ok(true);
        }
    }
}