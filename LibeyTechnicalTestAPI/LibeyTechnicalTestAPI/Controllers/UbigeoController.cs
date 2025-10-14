using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class UbigeoController : ControllerBase
{
    private readonly IUbigeoRepository _repository;
    public UbigeoController(IUbigeoRepository repository)
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

 