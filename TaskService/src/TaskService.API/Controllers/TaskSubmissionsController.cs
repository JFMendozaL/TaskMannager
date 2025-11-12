using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskService.Application.DTOs;
using TaskService.Application.Services;

namespace TaskService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskSubmissionsController : ControllerBase
{
    private readonly ITaskSubmissionService _submissionService;

    public TaskSubmissionsController(ITaskSubmissionService submissionService)
    {
        _submissionService = submissionService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubmissionById(int id)
    {
        var response = await _submissionService.GetSubmissionByIdAsync(id);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetSubmissionsByTaskId(int taskId)
    {
        var response = await _submissionService.GetSubmissionsByTaskIdAsync(taskId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetSubmissionsByUserId(int userId)
    {
        var response = await _submissionService.GetSubmissionsByUserIdAsync(userId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("task/{taskId}/user/{userId}")]
    public async Task<IActionResult> GetSubmissionByTaskAndUserId(int taskId, int userId)
    {
        var response = await _submissionService.GetSubmissionByTaskAndUserIdAsync(taskId, userId);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubmission([FromBody] CreateTaskSubmissionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Obtener el ID del usuario desde el token JWT
        var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
        if (userId == 0)
            return Unauthorized("Usuario no identificado");

        var response = await _submissionService.CreateSubmissionAsync(dto, userId);
        return response.Success ? CreatedAtAction(nameof(GetSubmissionById), new { id = response.Data!.Id }, response) : BadRequest(response);
    }

    [HttpPost("{id}/grade")]
    public async Task<IActionResult> GradeSubmission(int id, [FromBody] GradeTaskSubmissionDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Obtener el ID del usuario que califica desde el token JWT
        var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
        if (userId == 0)
            return Unauthorized("Usuario no identificado");

        var response = await _submissionService.GradeSubmissionAsync(id, dto, userId);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubmission(int id)
    {
        var response = await _submissionService.DeleteSubmissionAsync(id);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }
}

