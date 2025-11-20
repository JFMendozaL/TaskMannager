using AcademicService.Application.DTOs;

namespace AcademicService.Application.Services
{
    public interface IParentStudentService
    {
        Task<IEnumerable<ParentStudentDto>> GetAllLinksAsync();
        Task<ParentStudentDto?> GetLinkByIdAsync(int id);
        Task<IEnumerable<ParentStudentDto>> GetLinksByParentAsync(int parentId);
        Task<IEnumerable<ParentStudentDto>> GetLinksByStudentAsync(int studentId);
        Task<ParentStudentDto> CreateLinkAsync(CreateParentStudentDto dto);
        Task<ParentStudentDto> UpdateLinkAsync(int id, UpdateParentStudentDto dto);
        Task<bool> DeleteLinkAsync(int id);
    }
}
