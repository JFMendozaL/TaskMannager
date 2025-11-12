using TaskService.Domain.Entities;

namespace TaskService.Domain.Interfaces;

public interface ITaskSubmissionRepository
{
    Task<TaskSubmission?> GetByIdAsync(int id);
    Task<IEnumerable<TaskSubmission>> GetByTaskIdAsync(int taskId);
    Task<IEnumerable<TaskSubmission>> GetByUserIdAsync(int userId);
    Task<TaskSubmission?> GetByTaskAndUserIdAsync(int taskId, int userId);
    Task<TaskSubmission> CreateAsync(TaskSubmission submission);
    Task<TaskSubmission> UpdateAsync(TaskSubmission submission);
    Task<bool> DeleteAsync(int id);
}

