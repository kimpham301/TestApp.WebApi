using Microsoft.AspNetCore.Mvc;

namespace TestApp.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TestAppController : ControllerBase
{
    private readonly ILogger<TestAppController> _logger;

    public TestAppController(ILogger<TestAppController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("WebApi is ready");
    }
}