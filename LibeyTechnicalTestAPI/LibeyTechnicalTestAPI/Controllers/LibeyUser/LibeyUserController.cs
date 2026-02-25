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
            if (row == null) return NotFound();
            return Ok(row);
        }

        [HttpPost]
        public IActionResult Create(UserUpdateorCreateCommand command)
        {
            _aggregate.Create(command);
            return Ok(new { success = true });
        }

        // AGREGADO: Endpoint para actualizar
        [HttpPut]
        [Route("{documentNumber}")]
        public IActionResult Update(string documentNumber, UserUpdateorCreateCommand command)
        {
            if (documentNumber != command.DocumentNumber)
                return BadRequest("Document number mismatch");

            _aggregate.Update(documentNumber, command);
            return Ok(new { success = true });
        }

        // AGREGADO: Endpoint para eliminar
        [HttpDelete]
        [Route("{documentNumber}")]
        public IActionResult Delete(string documentNumber)
        {
            _aggregate.Delete(documentNumber);
            return Ok(new { success = true });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _aggregate.GetAll();
            return Ok(users);
        }
    }
}
