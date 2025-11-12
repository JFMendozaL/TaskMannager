using TaskService.Domain.Entities;
using TaskStatusEnum = TaskService.Domain.Entities.TaskStatus;
using TaskPriorityEnum = TaskService.Domain.Entities.TaskPriority;

namespace TaskService.Application.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public int CreatedByUserId { get; set; }
    public int? AssignedToUserId { get; set; }
    public TaskStatusEnum Status { get; set; }
    public TaskPriorityEnum Priority { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal? Grade { get; set; }
    public string? Comments { get; set; }
    public int SubmissionCount { get; set; }
}

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CourseId { get; set; }
    public int? AssignedToUserId { get; set; }
    public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;
    public DateTime DueDate { get; set; }
}

public class UpdateTaskDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? AssignedToUserId { get; set; }
    public TaskStatusEnum? Status { get; set; }
    public TaskPriorityEnum? Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal? Grade { get; set; }
    public string? Comments { get; set; }
}

