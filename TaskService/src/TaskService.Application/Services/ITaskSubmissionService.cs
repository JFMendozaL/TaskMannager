using TaskService.Application.DTOs;

namespace TaskService.Application.Services;

public interface ITaskSubmissionService
{
    Task<ApiResponse<TaskSubmissionDto>> GetSubmissionByIdAsync(int id);
    Task<ApiResponse<IEnumerable<TaskSubmissionDto>>> GetSubmissionsByTaskIdAsync(int taskId);
    Task<ApiResponse<IEnumerable<TaskSubmissionDto>>> GetSubmissionsByUserIdAsync(int userId);
    Task<ApiResponse<TaskSubmissionDto>> GetSubmissionByTaskAndUserIdAsync(int taskId, int userId);
    Task<ApiResponse<TaskSubmissionDto>> CreateSubmissionAsync(CreateTaskSubmissionDto dto, int submittedByUserId);
    Task<ApiResponse<TaskSubmissionDto>> GradeSubmissionAsync(int id, GradeTaskSubmissionDto dto, int gradedByUserId);
    Task<ApiResponse<bool>> DeleteSubmissionAsync(int id);
}

