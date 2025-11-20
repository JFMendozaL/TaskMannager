using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using AcademicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicService.Infrastructure.Repositories
{
    public class ParentStudentRepository : IParentStudentRepository
    {
        private readonly AcademicDbContext _context;

        public ParentStudentRepository(AcademicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParentStudent>> GetAllAsync()
        {
            return await _context.ParentStudents.ToListAsync();
        }

        public async Task<ParentStudent?> GetByIdAsync(int id)
        {
            return await _context.ParentStudents.FindAsync(id);
        }

        public async Task<IEnumerable<ParentStudent>> GetByParentIdAsync(int parentId)
        {
            return await _context.ParentStudents
                .Where(ps => ps.ParentId == parentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ParentStudent>> GetByStudentIdAsync(int studentId)
        {
            return await _context.ParentStudents
                .Where(ps => ps.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<ParentStudent> CreateAsync(ParentStudent link)
        {
            _context.ParentStudents.Add(link);
            await _context.SaveChangesAsync();
            return link;
        }

        public async Task<ParentStudent> UpdateAsync(ParentStudent link)
        {
            _context.Entry(link).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return link;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var link = await _context.ParentStudents.FindAsync(id);
            if (link == null)
                return false;

            _context.ParentStudents.Remove(link);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int parentId, int studentId)
        {
            return await _context.ParentStudents
                .AnyAsync(ps => ps.ParentId == parentId && ps.StudentId == studentId);
        }
    }
}
