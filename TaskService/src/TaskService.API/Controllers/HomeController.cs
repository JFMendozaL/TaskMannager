using Microsoft.AspNetCore.Mvc;

namespace TaskService.API.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Redirect("/swagger");
    }
}

