using AcademicService.Domain.Entities;

namespace AcademicService.Domain.Interfaces
{
    public interface IParentStudentRepository
    {
        Task<IEnumerable<ParentStudent>> GetAllAsync();
        Task<ParentStudent?> GetByIdAsync(int id);
        Task<IEnumerable<ParentStudent>> GetByParentIdAsync(int parentId);
        Task<IEnumerable<ParentStudent>> GetByStudentIdAsync(int studentId);
        Task<ParentStudent> CreateAsync(ParentStudent link);
        Task<ParentStudent> UpdateAsync(ParentStudent link);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int parentId, int studentId);
    }
}
