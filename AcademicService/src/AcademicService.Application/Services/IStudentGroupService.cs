using AcademicService.Application.DTOs;

namespace AcademicService.Application.Services
{
    public interface IStudentGroupService
    {
        Task<IEnumerable<StudentGroupDto>> GetAllEnrollmentsAsync();
        Task<StudentGroupDto?> GetEnrollmentByIdAsync(int id);
        Task<IEnumerable<StudentGroupDto>> GetEnrollmentsByStudentAsync(int studentId);
        Task<IEnumerable<StudentGroupDto>> GetEnrollmentsByGroupAsync(int groupId);
        Task<StudentGroupDto> CreateEnrollmentAsync(CreateStudentGroupDto dto);
        Task<StudentGroupDto> UpdateEnrollmentAsync(int id, UpdateStudentGroupDto dto);
        Task<bool> DeleteEnrollmentAsync(int id);
    }
}
