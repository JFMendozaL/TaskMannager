using Microsoft.AspNetCore.Mvc;

namespace AcademicService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                service = "AcademicService",
                version = "1.0",
                status = "Running",
                endpoints = new[]
                {
                    "/api/subjects",
                    "/api/groups",
                    "/api/academic-periods",
                    "/api/teacher-assignments",
                    "/api/student-enrollments",
                    "/api/parent-links"
                }
            });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
        }
    }
}
