using AcademicService.Application.DTOs;

namespace AcademicService.Application.Services
{
    public interface IAcademicPeriodService
    {
        Task<IEnumerable<AcademicPeriodDto>> GetAllPeriodsAsync();
        Task<IEnumerable<AcademicPeriodDto>> GetActivePeriodsAsync();
        Task<AcademicPeriodDto?> GetPeriodByIdAsync(int id);
        Task<AcademicPeriodDto?> GetCurrentPeriodAsync();
        Task<AcademicPeriodDto> CreatePeriodAsync(CreateAcademicPeriodDto dto);
        Task<AcademicPeriodDto> UpdatePeriodAsync(int id, UpdateAcademicPeriodDto dto);
        Task<bool> DeletePeriodAsync(int id);
    }
}
