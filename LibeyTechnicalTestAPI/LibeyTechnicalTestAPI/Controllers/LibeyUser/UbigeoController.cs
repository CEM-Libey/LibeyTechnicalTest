using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LibeyTechnicalTestAPI.Controllers.LibeyUser
{
    [ApiController]
    [Route("[controller]")]
    public class UbigeoController : Controller
    {
        private readonly IUbigeoAggregate _aggregate;
        public UbigeoController(IUbigeoAggregate aggregate)
        {
            _aggregate = aggregate;
        }
        [HttpGet]
        [Route("byprovinces/{ProvinceCode}")]
        [ProducesResponseType(typeof(List<ComboDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ComboDTO>>> ListUbigeos(string ProvinceCode)
        {
            var row = await _aggregate.comboUbigeo(ProvinceCode);
            return Ok(row);
        }

        [HttpGet]
        [Route("byregions/{RegionCode}")]
        [ProducesResponseType(typeof(List<ComboDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ComboDTO>>> ListProvinces(string RegionCode)
        {
            var row = await _aggregate.comboProvince(RegionCode);
            return Ok(row);
        }
        [HttpGet]
        [Route("regionList")]
        [ProducesResponseType(typeof(List<ComboDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ComboDTO>>> ListRegions()
        {
            var row = await _aggregate.comboRegion();
            return Ok(row);
        }
    }
}
