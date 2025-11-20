using AcademicService.Domain.Entities;

namespace AcademicService.Domain.Interfaces
{
    public interface ITeacherSubjectGroupRepository
    {
        Task<IEnumerable<TeacherSubjectGroup>> GetAllAsync();
        Task<TeacherSubjectGroup?> GetByIdAsync(int id);
        Task<IEnumerable<TeacherSubjectGroup>> GetByTeacherIdAsync(int teacherId);
        Task<IEnumerable<TeacherSubjectGroup>> GetByGroupIdAsync(int groupId);
        Task<IEnumerable<TeacherSubjectGroup>> GetBySubjectIdAsync(int subjectId);
        Task<IEnumerable<TeacherSubjectGroup>> GetByPeriodIdAsync(int periodId);
        Task<TeacherSubjectGroup> CreateAsync(TeacherSubjectGroup assignment);
        Task<TeacherSubjectGroup> UpdateAsync(TeacherSubjectGroup assignment);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int teacherId, int subjectId, int groupId, int periodId);
    }
}
