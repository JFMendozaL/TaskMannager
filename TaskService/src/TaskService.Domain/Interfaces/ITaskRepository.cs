using TaskService.Domain.Entities;
using TaskEntity = TaskService.Domain.Entities.Task;

namespace TaskService.Domain.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TaskEntity>> GetAllAsync();
    Task<IEnumerable<TaskEntity>> GetByCourseIdAsync(int courseId);
    Task<IEnumerable<TaskEntity>> GetByUserIdAsync(int userId);
    Task<IEnumerable<TaskEntity>> GetByAssignedUserIdAsync(int userId);
    Task<TaskEntity> CreateAsync(TaskEntity task);
    Task<TaskEntity> UpdateAsync(TaskEntity task);
    System.Threading.Tasks.Task<bool> DeleteAsync(int id);
    System.Threading.Tasks.Task<bool> ExistsAsync(int id);
}

