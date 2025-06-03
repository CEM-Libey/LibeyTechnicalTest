using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibeyTechnicalTestAPI.Controllers.LibeyUser
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibeyUserController : ControllerBase
    {
        private readonly ILibeyUserAggregate _aggregate;
        
        public LibeyUserController(ILibeyUserAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        /// <summary>
        /// Obtiene un usuario por número de documento
        /// </summary>
        [HttpGet("{documentNumber}")]
        public IActionResult GetUser(string documentNumber)
        {
            try
            {
                var user = _aggregate.FindResponse(documentNumber);
                if (string.IsNullOrEmpty(user.DocumentNumber))
                    return NotFound($"User with document number {documentNumber} not found");
                
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene una lista paginada de usuarios con filtros
        /// </summary>
        [HttpGet]
        public IActionResult GetUsers([FromQuery] UserSearchQuery query)
        {
            try
            {
                var result = _aggregate.GetUsers(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserUpdateorCreateCommand command)
        {
            try
            {
                _aggregate.Create(command);
                return CreatedAtAction(nameof(GetUser), new { documentNumber = command.DocumentNumber }, 
                    new { message = "User created successfully", documentNumber = command.DocumentNumber });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un usuario existente
        /// </summary>
        [HttpPut("{documentNumber}")]
        public IActionResult UpdateUser(string documentNumber, [FromBody] UserUpdateorCreateCommand command)
        {
            try
            {
                if (documentNumber != command.DocumentNumber)
                    return BadRequest(new { message = "Document number in URL must match the one in the request body" });

                _aggregate.Update(command);
                return Ok(new { message = "User updated successfully", documentNumber });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        /// <summary>
        /// Elimina (desactiva) un usuario
        /// </summary>
        [HttpDelete("{documentNumber}")]
        public IActionResult DeleteUser(string documentNumber)
        {
            try
            {
                _aggregate.Delete(documentNumber);
                return Ok(new { message = "User deactivated successfully", documentNumber });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", details = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la lista de tipos de documento
        /// </summary>
        [HttpGet("document-types")]
        public IActionResult GetDocumentTypes()
        {
            try
            {
                var documentTypes = _aggregate.GetDocumentTypes();
                return Ok(documentTypes);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Obtiene la lista de ubigeos (regiones, provincias, distritos)
        /// </summary>
        [HttpGet("ubigeos")]
        public IActionResult GetUbigeos()
        {
            try
            {
                var ubigeos = _aggregate.GetUbigeos();
                return Ok(ubigeos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}