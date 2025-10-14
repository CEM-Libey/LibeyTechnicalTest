using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ProvinceController : ControllerBase
{
    private readonly IProvinceRepository _repository;
    public ProvinceController(IProvinceRepository repository)
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
