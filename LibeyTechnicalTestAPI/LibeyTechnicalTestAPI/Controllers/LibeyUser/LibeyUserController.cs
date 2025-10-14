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
            if (row == null)
            {
                return NotFound($"User with DocumentNumber {documentNumber} No encontrado.");
            }
            return Ok(row);
        }

        [HttpPost]       
        public IActionResult Create(UserUpdateorCreateCommand command)
        {
            try
            {
                _aggregate.Create(command);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // NUEVO AGREGADO

        [HttpGet]
        public IActionResult GetAll([FromQuery] string? search)
        {
            var users = _aggregate.GetAll(search);
            return Ok(users);
        }

        [HttpPut("{documentNumber}")]
        public IActionResult Update(string documentNumber, UserUpdateorCreateCommand command)
        {
            try
            {
                _aggregate.Update(command);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpDelete("{documentNumber}")]
        public IActionResult Delete(string documentNumber)
        {
            try
            {
                _aggregate.Delete(documentNumber);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}