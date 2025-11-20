using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using AcademicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicService.Infrastructure.Repositories
{
    public class AcademicPeriodRepository : IAcademicPeriodRepository
    {
        private readonly AcademicDbContext _context;

        public AcademicPeriodRepository(AcademicDbContext context) => _context = context;

        public async Task<IEnumerable<AcademicPeriod>> GetAllAsync() => 
            await _context.AcademicPeriods.OrderByDescending(p => p.StartDate).ToListAsync();

        public async Task<IEnumerable<AcademicPeriod>> GetActiveAsync() => 
            await _context.AcademicPeriods.Where(p => p.IsActive).OrderByDescending(p => p.StartDate).ToListAsync();

        public async Task<AcademicPeriod?> GetByIdAsync(int id) => 
            await _context.AcademicPeriods.FindAsync(id);

        public async Task<AcademicPeriod?> GetCurrentAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.AcademicPeriods
                .Where(p => p.IsActive && p.StartDate <= now && p.EndDate >= now)
                .FirstOrDefaultAsync();
        }

        public async Task<AcademicPeriod> CreateAsync(AcademicPeriod period)
        {
            _context.AcademicPeriods.Add(period);
            await _context.SaveChangesAsync();
            return period;
        }

        public async Task<AcademicPeriod> UpdateAsync(AcademicPeriod period)
        {
            period.UpdatedAt = DateTime.UtcNow;
            _context.AcademicPeriods.Update(period);
            await _context.SaveChangesAsync();
            return period;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var period = await GetByIdAsync(id);
            if (period == null) return false;
            _context.AcademicPeriods.Remove(period);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id) => 
            await _context.AcademicPeriods.AnyAsync(p => p.Id == id);
    }
}
