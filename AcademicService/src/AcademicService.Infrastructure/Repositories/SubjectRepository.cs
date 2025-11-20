using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using AcademicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicService.Infrastructure.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AcademicDbContext _context;

        public SubjectRepository(AcademicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Subject>> GetAllAsync()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<IEnumerable<Subject>> GetActiveAsync()
        {
            return await _context.Subjects.Where(s => s.IsActive).ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(int id)
        {
            return await _context.Subjects.FindAsync(id);
        }

        public async Task<Subject?> GetByCodeAsync(string code)
        {
            return await _context.Subjects.FirstOrDefaultAsync(s => s.Code == code);
        }

        public async Task<Subject> CreateAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject> UpdateAsync(Subject subject)
        {
            subject.UpdatedAt = DateTime.UtcNow;
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
            return subject;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var subject = await GetByIdAsync(id);
            if (subject == null) return false;

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Subjects.AnyAsync(s => s.Id == id);
        }

        public async Task<bool> CodeExistsAsync(string code, int? excludeId = null)
        {
            return await _context.Subjects.AnyAsync(s => s.Code == code && s.Id != excludeId);
        }
    }
}
