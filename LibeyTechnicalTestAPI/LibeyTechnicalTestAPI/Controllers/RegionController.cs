using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RegionController : ControllerBase
{
    private readonly IRegionRepository _repository;
    public RegionController(IRegionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _repository.GetAll();
        return Ok(result);
    }
}
