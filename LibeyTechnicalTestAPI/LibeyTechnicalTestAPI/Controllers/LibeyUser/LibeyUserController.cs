using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(row.DocumentNumber))
            {
                return NotFound();
            }
            return Ok(row);
        }
        [HttpPost]       
        public IActionResult Create([FromBody] UserUpdateorCreateCommand command)
        {
             _aggregate.Create(command);
            return Ok(true);
        }
        [HttpPut]
        public IActionResult Update([FromBody] UserUpdateorCreateCommand command)
        {
            _aggregate.Update(command);
            return Ok(true);
        }
        [HttpDelete("{documentNumber}")]
        public IActionResult Delete(string documentNumber)
        {
            _aggregate.Delete(documentNumber);
            return Ok(true);
        }   
        [HttpGet]
        [Route("listLibeyUsers")]
        public IActionResult ListLibeyUsers()
        {
            var list = _aggregate.ListLibeyUsers();
            return Ok(list);
        }
    }
}