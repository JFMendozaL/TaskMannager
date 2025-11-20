using AcademicService.Domain.Entities;

namespace AcademicService.Domain.Interfaces
{
    public interface IStudentGroupRepository
    {
        Task<IEnumerable<StudentGroup>> GetAllAsync();
        Task<StudentGroup?> GetByIdAsync(int id);
        Task<IEnumerable<StudentGroup>> GetByStudentIdAsync(int studentId);
        Task<IEnumerable<StudentGroup>> GetByGroupIdAsync(int groupId);
        Task<StudentGroup?> GetByStudentAndGroupAsync(int studentId, int groupId);
        Task<StudentGroup> CreateAsync(StudentGroup enrollment);
        Task<StudentGroup> UpdateAsync(StudentGroup enrollment);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int studentId, int groupId);
    }
}
