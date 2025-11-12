using TaskService.Application.DTOs;
using TaskService.Domain.Interfaces;

namespace TaskService.Application.Services;

public class TaskSubmissionService : ITaskSubmissionService
{
    private readonly ITaskSubmissionRepository _submissionRepository;
    private readonly ITaskRepository _taskRepository;

    public TaskSubmissionService(
        ITaskSubmissionRepository submissionRepository,
        ITaskRepository taskRepository)
    {
        _submissionRepository = submissionRepository;
        _taskRepository = taskRepository;
    }

    public async Task<ApiResponse<TaskSubmissionDto>> GetSubmissionByIdAsync(int id)
    {
        var submission = await _submissionRepository.GetByIdAsync(id);
        if (submission == null)
        {
            return ApiResponse<TaskSubmissionDto>.ErrorResponse("Entrega no encontrada");
        }

        var dto = MapToDto(submission);
        return ApiResponse<TaskSubmissionDto>.SuccessResponse(dto);
    }

    public async Task<ApiResponse<IEnumerable<TaskSubmissionDto>>> GetSubmissionsByTaskIdAsync(int taskId)
    {
        var submissions = await _submissionRepository.GetByTaskIdAsync(taskId);
        var dtos = submissions.Select(MapToDto);
        return ApiResponse<IEnumerable<TaskSubmissionDto>>.SuccessResponse(dtos);
    }

    public async Task<ApiResponse<IEnumerable<TaskSubmissionDto>>> GetSubmissionsByUserIdAsync(int userId)
    {
        var submissions = await _submissionRepository.GetByUserIdAsync(userId);
        var dtos = submissions.Select(MapToDto);
        return ApiResponse<IEnumerable<TaskSubmissionDto>>.SuccessResponse(dtos);
    }

    public async Task<ApiResponse<TaskSubmissionDto>> GetSubmissionByTaskAndUserIdAsync(int taskId, int userId)
    {
        var submission = await _submissionRepository.GetByTaskAndUserIdAsync(taskId, userId);
        if (submission == null)
        {
            return ApiResponse<TaskSubmissionDto>.ErrorResponse("Entrega no encontrada");
        }

        var dto = MapToDto(submission);
        return ApiResponse<TaskSubmissionDto>.SuccessResponse(dto);
    }

    public async Task<ApiResponse<TaskSubmissionDto>> CreateSubmissionAsync(CreateTaskSubmissionDto dto, int submittedByUserId)
    {
        var task = await _taskRepository.GetByIdAsync(dto.TaskId);
        if (task == null)
        {
            return ApiResponse<TaskSubmissionDto>.ErrorResponse("Tarea no encontrada");
        }

        var existingSubmission = await _submissionRepository.GetByTaskAndUserIdAsync(dto.TaskId, submittedByUserId);
        if (existingSubmission != null)
        {
            return ApiResponse<TaskSubmissionDto>.ErrorResponse("Ya existe una entrega para esta tarea");
        }

        var submission = new Domain.Entities.TaskSubmission
        {
            TaskId = dto.TaskId,
            SubmittedByUserId = submittedByUserId,
            SubmissionContent = dto.SubmissionContent,
            FileUrl = dto.FileUrl
        };

        var createdSubmission = await _submissionRepository.CreateAsync(submission);
        var submissionDto = MapToDto(createdSubmission);
        return ApiResponse<TaskSubmissionDto>.SuccessResponse(submissionDto, "Entrega creada exitosamente");
    }

    public async Task<ApiResponse<TaskSubmissionDto>> GradeSubmissionAsync(int id, GradeTaskSubmissionDto dto, int gradedByUserId)
    {
        var submission = await _submissionRepository.GetByIdAsync(id);
        if (submission == null)
        {
            return ApiResponse<TaskSubmissionDto>.ErrorResponse("Entrega no encontrada");
        }

        submission.Grade = dto.Grade;
        submission.Feedback = dto.Feedback;
        submission.GradedAt = DateTime.UtcNow;
        submission.GradedByUserId = gradedByUserId;

        var updatedSubmission = await _submissionRepository.UpdateAsync(submission);
        var submissionDto = MapToDto(updatedSubmission);
        return ApiResponse<TaskSubmissionDto>.SuccessResponse(submissionDto, "Calificación registrada exitosamente");
    }

    public async Task<ApiResponse<bool>> DeleteSubmissionAsync(int id)
    {
        var submission = await _submissionRepository.GetByIdAsync(id);
        if (submission == null)
        {
            return ApiResponse<bool>.ErrorResponse("Entrega no encontrada");
        }

        var deleted = await _submissionRepository.DeleteAsync(id);
        return ApiResponse<bool>.SuccessResponse(deleted, "Entrega eliminada exitosamente");
    }

    private static TaskSubmissionDto MapToDto(Domain.Entities.TaskSubmission submission)
    {
        return new TaskSubmissionDto
        {
            Id = submission.Id,
            TaskId = submission.TaskId,
            SubmittedByUserId = submission.SubmittedByUserId,
            SubmissionContent = submission.SubmissionContent,
            FileUrl = submission.FileUrl,
            SubmittedAt = submission.SubmittedAt,
            Grade = submission.Grade,
            Feedback = submission.Feedback,
            GradedAt = submission.GradedAt,
            GradedByUserId = submission.GradedByUserId
        };
    }
}

