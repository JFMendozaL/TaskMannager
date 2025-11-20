using AcademicService.Domain.Entities;

namespace AcademicService.Domain.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetAllAsync();
        Task<IEnumerable<Subject>> GetActiveAsync();
        Task<Subject?> GetByIdAsync(int id);
        Task<Subject?> GetByCodeAsync(string code);
        Task<Subject> CreateAsync(Subject subject);
        Task<Subject> UpdateAsync(Subject subject);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> CodeExistsAsync(string code, int? excludeId = null);
    }
}
