using AcademicService.Application.DTOs;

namespace AcademicService.Application.Services
{
    public interface ISubjectService
    {
        Task<SubjectDto?> GetByIdAsync(int id);
        Task<SubjectDto?> GetByCodeAsync(string code);
        Task<IEnumerable<SubjectDto>> GetAllAsync();
        Task<IEnumerable<SubjectDto>> GetActiveAsync();
        Task<SubjectDto> CreateAsync(CreateSubjectDto dto);
        Task<SubjectDto> UpdateAsync(int id, UpdateSubjectDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
