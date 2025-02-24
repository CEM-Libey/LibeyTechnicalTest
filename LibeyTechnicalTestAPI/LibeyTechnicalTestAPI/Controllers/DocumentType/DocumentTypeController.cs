using LibeyTechnicalTestDomain.LibeyUserAggregate.Application;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace LibeyTechnicalTestAPI.Controllers.DocumentType
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly IDocumentTypeAggregate _aggregate;

        public DocumentTypeController(IDocumentTypeAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var documentTypes = _aggregate.GetAll();
            return Ok(documentTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var response = _aggregate.FindResponse(id);
            return response != null ? Ok(response) : NotFound("Tipo de documento no encontrado.");
        }
    }
}