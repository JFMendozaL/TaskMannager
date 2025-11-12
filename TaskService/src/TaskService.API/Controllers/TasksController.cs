using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskService.Application.DTOs;
using TaskService.Application.Services;

namespace TaskService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [AllowAnonymous] // Temporalmente permitido para pruebas
    public async Task<IActionResult> GetAllTasks()
    {
        var response = await _taskService.GetAllTasksAsync();
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id}")]
    [AllowAnonymous] // Temporalmente permitido para pruebas
    public async Task<IActionResult> GetTaskById(int id)
    {
        var response = await _taskService.GetTaskByIdAsync(id);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetTasksByCourseId(int courseId)
    {
        var response = await _taskService.GetTasksByCourseIdAsync(courseId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetTasksByUserId(int userId)
    {
        var response = await _taskService.GetTasksByUserIdAsync(userId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("assigned/{userId}")]
    public async Task<IActionResult> GetTasksByAssignedUserId(int userId)
    {
        var response = await _taskService.GetTasksByAssignedUserIdAsync(userId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [AllowAnonymous] // Temporalmente permitido para pruebas
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto, [FromQuery] int? createdByUserId = null)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Obtener el ID del usuario desde el token JWT o desde el parámetro de consulta
        var userId = createdByUserId ?? int.Parse(User.FindFirst("UserId")?.Value ?? "1");
        
        // Si no hay usuario autenticado y no se proporciona userId, usar 1 por defecto para pruebas
        if (userId == 0)
            userId = 1;

        var response = await _taskService.CreateTaskAsync(dto, userId);
        return response.Success ? CreatedAtAction(nameof(GetTaskById), new { id = response.Data!.Id }, response) : BadRequest(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await _taskService.UpdateTaskAsync(id, dto);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var response = await _taskService.DeleteTaskAsync(id);
        if (!response.Success)
            return NotFound(response);
        return Ok(response);
    }
}

