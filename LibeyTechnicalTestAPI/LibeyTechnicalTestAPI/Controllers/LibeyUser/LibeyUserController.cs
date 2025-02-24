using LibeyTechnicalTestDomain.LibeyUserAggregate.Application;
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
            try
            {
                var user = _aggregate.FindResponse(documentNumber);

                if (user == null || string.IsNullOrEmpty(user.DocumentNumber))
                    return NoContent();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el usuario: {ex.Message}");
            }
        }




        [HttpPost]
        public IActionResult Create([FromBody] UserUpdateorCreateCommand command)
        {
            try
            {
                _aggregate.Create(command);
                return Ok(new { message = "El usuario se creó correctamente." });

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el usuario: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _aggregate.GetAll();

                if (users == null || !users.Any())
                    return NoContent();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la lista de usuarios: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserUpdateorCreateCommand command)
        {
            try
            {
                _aggregate.Update(command);
                return Ok(new { message = "El usuario se actualizó correctamente." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el usuario: {ex.Message}");
            }
        }

        [HttpPut("{documentNumber}/state")]

        public IActionResult UpdateUserState(string documentNumber,bool state)
        {
            try
            {
                if (state==true)
                {
                    _aggregate.Activate(documentNumber);
                    return Ok(new { message = "El usuario se activo correctamente." });
                }
                else
                {

                    _aggregate.Desactivate(documentNumber);
                    return Ok(new { message = "El usuario se desactivo correctamente." });
                }

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al desactivar el usuario: {ex.Message}");
            }
        }
       


        [HttpDelete("{documentNumber}")]
        public IActionResult Delete(string documentNumber)
        {
            try
            {
                _aggregate.Delete(documentNumber);
                return Ok(new { message = $"Usuario con número de documento {documentNumber} eliminado correctamente." });  
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el usuario: {ex.Message}");
            }
        }

    }
}