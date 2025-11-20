using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using AcademicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicService.Infrastructure.Repositories
{
    public class StudentGroupRepository : IStudentGroupRepository
    {
        private readonly AcademicDbContext _context;

        public StudentGroupRepository(AcademicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StudentGroup>> GetAllAsync()
        {
            return await _context.StudentGroups
                .Include(sg => sg.Group)
                .ToListAsync();
        }

        public async Task<StudentGroup?> GetByIdAsync(int id)
        {
            return await _context.StudentGroups
                .Include(sg => sg.Group)
                .FirstOrDefaultAsync(sg => sg.Id == id);
        }

        public async Task<IEnumerable<StudentGroup>> GetByStudentIdAsync(int studentId)
        {
            return await _context.StudentGroups
                .Include(sg => sg.Group)
                .Where(sg => sg.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<StudentGroup>> GetByGroupIdAsync(int groupId)
        {
            return await _context.StudentGroups
                .Include(sg => sg.Group)
                .Where(sg => sg.GroupId == groupId)
                .ToListAsync();
        }

        public async Task<StudentGroup?> GetByStudentAndGroupAsync(int studentId, int groupId)
        {
            return await _context.StudentGroups
                .Include(sg => sg.Group)
                .FirstOrDefaultAsync(sg => sg.StudentId == studentId && sg.GroupId == groupId);
        }

        public async Task<StudentGroup> CreateAsync(StudentGroup enrollment)
        {
            _context.StudentGroups.Add(enrollment);
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public async Task<StudentGroup> UpdateAsync(StudentGroup enrollment)
        {
            _context.Entry(enrollment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return enrollment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var enrollment = await _context.StudentGroups.FindAsync(id);
            if (enrollment == null)
                return false;

            _context.StudentGroups.Remove(enrollment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int studentId, int groupId)
        {
            return await _context.StudentGroups
                .AnyAsync(sg => sg.StudentId == studentId && sg.GroupId == groupId);
        }
    }
}
