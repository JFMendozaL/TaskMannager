using TaskService.Application.DTOs;
using TaskService.Domain.Entities;
using TaskService.Domain.Interfaces;
using TaskEntity = TaskService.Domain.Entities.Task;
using TaskStatusEnum = TaskService.Domain.Entities.TaskStatus;
using TaskPriorityEnum = TaskService.Domain.Entities.TaskPriority;

namespace TaskService.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<ApiResponse<TaskDto>> GetTaskByIdAsync(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            return ApiResponse<TaskDto>.ErrorResponse("Tarea no encontrada");
        }

        var dto = MapToDto(task);
        return ApiResponse<TaskDto>.SuccessResponse(dto);
    }

    public async Task<ApiResponse<IEnumerable<TaskDto>>> GetAllTasksAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();
        var dtos = tasks.Select(MapToDto);
        return ApiResponse<IEnumerable<TaskDto>>.SuccessResponse(dtos);
    }

    public async Task<ApiResponse<IEnumerable<TaskDto>>> GetTasksByCourseIdAsync(int courseId)
    {
        var tasks = await _taskRepository.GetByCourseIdAsync(courseId);
        var dtos = tasks.Select(MapToDto);
        return ApiResponse<IEnumerable<TaskDto>>.SuccessResponse(dtos);
    }

    public async Task<ApiResponse<IEnumerable<TaskDto>>> GetTasksByUserIdAsync(int userId)
    {
        var tasks = await _taskRepository.GetByUserIdAsync(userId);
        var dtos = tasks.Select(MapToDto);
        return ApiResponse<IEnumerable<TaskDto>>.SuccessResponse(dtos);
    }

    public async Task<ApiResponse<IEnumerable<TaskDto>>> GetTasksByAssignedUserIdAsync(int userId)
    {
        var tasks = await _taskRepository.GetByAssignedUserIdAsync(userId);
        var dtos = tasks.Select(MapToDto);
        return ApiResponse<IEnumerable<TaskDto>>.SuccessResponse(dtos);
    }

    public async Task<ApiResponse<TaskDto>> CreateTaskAsync(CreateTaskDto dto, int createdByUserId)
    {
        var task = new TaskEntity
        {
            Title = dto.Title,
            Description = dto.Description,
            CourseId = dto.CourseId,
            CreatedByUserId = createdByUserId,
            AssignedToUserId = dto.AssignedToUserId,
            Priority = dto.Priority,
            DueDate = dto.DueDate,
            Status = TaskStatusEnum.Pending
        };

        var createdTask = await _taskRepository.CreateAsync(task);
        var taskDto = MapToDto(createdTask);
        return ApiResponse<TaskDto>.SuccessResponse(taskDto, "Tarea creada exitosamente");
    }

    public async Task<ApiResponse<TaskDto>> UpdateTaskAsync(int id, UpdateTaskDto dto)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            return ApiResponse<TaskDto>.ErrorResponse("Tarea no encontrada");
        }

        if (!string.IsNullOrEmpty(dto.Title))
            task.Title = dto.Title;
        if (!string.IsNullOrEmpty(dto.Description))
            task.Description = dto.Description;
        if (dto.AssignedToUserId.HasValue)
            task.AssignedToUserId = dto.AssignedToUserId;
        if (dto.Status.HasValue)
            task.Status = dto.Status.Value;
        if (dto.Priority.HasValue)
            task.Priority = dto.Priority.Value;
        if (dto.DueDate.HasValue)
            task.DueDate = dto.DueDate.Value;
        if (dto.Grade.HasValue)
            task.Grade = dto.Grade;
        if (dto.Comments != null)
            task.Comments = dto.Comments;

        var updatedTask = await _taskRepository.UpdateAsync(task);
        var taskDto = MapToDto(updatedTask);
        return ApiResponse<TaskDto>.SuccessResponse(taskDto, "Tarea actualizada exitosamente");
    }

    public async Task<ApiResponse<bool>> DeleteTaskAsync(int id)
    {
        var exists = await _taskRepository.ExistsAsync(id);
        if (!exists)
        {
            return ApiResponse<bool>.ErrorResponse("Tarea no encontrada");
        }

        var deleted = await _taskRepository.DeleteAsync(id);
        return ApiResponse<bool>.SuccessResponse(deleted, "Tarea eliminada exitosamente");
    }

    private static TaskDto MapToDto(TaskEntity task)
    {
        return new TaskDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            CourseId = task.CourseId,
            CreatedByUserId = task.CreatedByUserId,
            AssignedToUserId = task.AssignedToUserId,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt,
            CompletedAt = task.CompletedAt,
            Grade = task.Grade,
            Comments = task.Comments,
            SubmissionCount = task.Submissions?.Count ?? 0
        };
    }
}

