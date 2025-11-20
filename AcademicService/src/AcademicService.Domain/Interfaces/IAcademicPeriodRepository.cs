using AcademicService.Domain.Entities;

namespace AcademicService.Domain.Interfaces
{
    public interface IAcademicPeriodRepository
    {
        Task<IEnumerable<AcademicPeriod>> GetAllAsync();
        Task<IEnumerable<AcademicPeriod>> GetActiveAsync();
        Task<AcademicPeriod?> GetByIdAsync(int id);
        Task<AcademicPeriod?> GetCurrentAsync();
        Task<AcademicPeriod> CreateAsync(AcademicPeriod period);
        Task<AcademicPeriod> UpdateAsync(AcademicPeriod period);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
