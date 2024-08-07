using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibeyTechnicalTestAPI.Controllers.Configuration
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationAggregate _aggregate;
        public ConfigurationController(IConfigurationAggregate aggregate)
        {
            _aggregate = aggregate;
        }
        [HttpGet("document_type")]
        public IActionResult GetDocumentType()
        {
            return Ok(_aggregate.GetDocumentType());
        }
        [HttpGet("province/{idRegion}")]
        public IActionResult GetProvince(string idRegion)
        {
            return Ok(_aggregate.GetProvince(idRegion));
        }
        [HttpGet("region")]
        public IActionResult GetRegion()
        {
            return Ok(_aggregate.GetRegion());
        }
        [HttpGet("distrito/{idRegion}/{idProvince}")]
        public IActionResult GetUbigeo(string idRegion, string idProvince)
        {
            return Ok(_aggregate.GetUbigeo(idRegion, idProvince));
        }

    }
}
