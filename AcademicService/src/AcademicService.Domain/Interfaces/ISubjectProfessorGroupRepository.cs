using AcademicService.Domain.Entities;

namespace AcademicService.Domain.Interfaces
{
    public interface ISubjectProfessorGroupRepository
    {
        Task<IEnumerable<SubjectProfessorGroup>> GetAllAsync();
        Task<SubjectProfessorGroup?> GetByIdAsync(int id);
        Task<IEnumerable<SubjectProfessorGroup>> GetByProfessorIdAsync(int professorId);
        Task<IEnumerable<SubjectProfessorGroup>> GetByGroupIdAsync(int groupId);
        Task<IEnumerable<SubjectProfessorGroup>> GetBySubjectIdAsync(int subjectId);
        Task<IEnumerable<SubjectProfessorGroup>> GetByAcademicPeriodIdAsync(int periodId);
        Task<IEnumerable<SubjectProfessorGroup>> GetByProfessorAndPeriodAsync(int professorId, int periodId);
        Task<SubjectProfessorGroup> CreateAsync(SubjectProfessorGroup assignment);
        Task<SubjectProfessorGroup> UpdateAsync(SubjectProfessorGroup assignment);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int professorId, int subjectId, int groupId, int periodId);
    }
}
