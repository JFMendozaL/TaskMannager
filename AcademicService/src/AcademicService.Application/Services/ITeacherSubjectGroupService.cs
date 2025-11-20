using AcademicService.Application.DTOs;

namespace AcademicService.Application.Services
{
    public interface ITeacherSubjectGroupService
    {
        Task<IEnumerable<TeacherSubjectGroupDto>> GetAllAssignmentsAsync();
        Task<TeacherSubjectGroupDto?> GetAssignmentByIdAsync(int id);
        Task<IEnumerable<TeacherSubjectGroupDto>> GetAssignmentsByTeacherAsync(int teacherId);
        Task<IEnumerable<TeacherSubjectGroupDto>> GetAssignmentsByGroupAsync(int groupId);
        Task<IEnumerable<TeacherSubjectGroupDto>> GetAssignmentsBySubjectAsync(int subjectId);
        Task<TeacherSubjectGroupDto> CreateAssignmentAsync(CreateTeacherSubjectGroupDto dto);
        Task<TeacherSubjectGroupDto> UpdateAssignmentAsync(int id, UpdateTeacherSubjectGroupDto dto);
        Task<bool> DeleteAssignmentAsync(int id);
    }
}
