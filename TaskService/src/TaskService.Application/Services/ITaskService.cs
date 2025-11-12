using TaskService.Application.DTOs;

namespace TaskService.Application.Services;

public interface ITaskService
{
    Task<ApiResponse<TaskDto>> GetTaskByIdAsync(int id);
    Task<ApiResponse<IEnumerable<TaskDto>>> GetAllTasksAsync();
    Task<ApiResponse<IEnumerable<TaskDto>>> GetTasksByCourseIdAsync(int courseId);
    Task<ApiResponse<IEnumerable<TaskDto>>> GetTasksByUserIdAsync(int userId);
    Task<ApiResponse<IEnumerable<TaskDto>>> GetTasksByAssignedUserIdAsync(int userId);
    Task<ApiResponse<TaskDto>> CreateTaskAsync(CreateTaskDto dto, int createdByUserId);
    Task<ApiResponse<TaskDto>> UpdateTaskAsync(int id, UpdateTaskDto dto);
    Task<ApiResponse<bool>> DeleteTaskAsync(int id);
}

