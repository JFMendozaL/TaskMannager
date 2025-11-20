using AcademicService.Domain.Entities;

namespace AcademicService.Domain.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<IEnumerable<Group>> GetActiveAsync();
        Task<Group?> GetByIdAsync(int id);
        Task<IEnumerable<Group>> GetByLevelAsync(string level);
        Task<Group> CreateAsync(Group group);
        Task<Group> UpdateAsync(Group group);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
