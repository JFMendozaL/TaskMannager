using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using AcademicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicService.Infrastructure.Repositories
{
    public class TeacherSubjectGroupRepository : ITeacherSubjectGroupRepository
    {
        private readonly AcademicDbContext _context;

        public TeacherSubjectGroupRepository(AcademicDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeacherSubjectGroup>> GetAllAsync()
        {
            return await _context.TeacherSubjectGroups
                .Include(tsg => tsg.Subject)
                .Include(tsg => tsg.Group)
                .Include(tsg => tsg.AcademicPeriod)
                .ToListAsync();
        }

        public async Task<TeacherSubjectGroup?> GetByIdAsync(int id)
        {
            return await _context.TeacherSubjectGroups
                .Include(tsg => tsg.Subject)
                .Include(tsg => tsg.Group)
                .Include(tsg => tsg.AcademicPeriod)
                .FirstOrDefaultAsync(tsg => tsg.Id == id);
        }

        public async Task<IEnumerable<TeacherSubjectGroup>> GetByTeacherIdAsync(int teacherId)
        {
            return await _context.TeacherSubjectGroups
                .Include(tsg => tsg.Subject)
                .Include(tsg => tsg.Group)
                .Include(tsg => tsg.AcademicPeriod)
                .Where(tsg => tsg.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSubjectGroup>> GetByGroupIdAsync(int groupId)
        {
            return await _context.TeacherSubjectGroups
                .Include(tsg => tsg.Subject)
                .Include(tsg => tsg.Group)
                .Include(tsg => tsg.AcademicPeriod)
                .Where(tsg => tsg.GroupId == groupId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSubjectGroup>> GetBySubjectIdAsync(int subjectId)
        {
            return await _context.TeacherSubjectGroups
                .Include(tsg => tsg.Subject)
                .Include(tsg => tsg.Group)
                .Include(tsg => tsg.AcademicPeriod)
                .Where(tsg => tsg.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TeacherSubjectGroup>> GetByPeriodIdAsync(int periodId)
        {
            return await _context.TeacherSubjectGroups
                .Include(tsg => tsg.Subject)
                .Include(tsg => tsg.Group)
                .Include(tsg => tsg.AcademicPeriod)
                .Where(tsg => tsg.AcademicPeriodId == periodId)
                .ToListAsync();
        }

        public async Task<TeacherSubjectGroup> CreateAsync(TeacherSubjectGroup assignment)
        {
            _context.TeacherSubjectGroups.Add(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<TeacherSubjectGroup> UpdateAsync(TeacherSubjectGroup assignment)
        {
            _context.Entry(assignment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var assignment = await _context.TeacherSubjectGroups.FindAsync(id);
            if (assignment == null)
                return false;

            _context.TeacherSubjectGroups.Remove(assignment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int teacherId, int subjectId, int groupId, int periodId)
        {
            return await _context.TeacherSubjectGroups
                .AnyAsync(tsg => tsg.TeacherId == teacherId 
                    && tsg.SubjectId == subjectId 
                    && tsg.GroupId == groupId 
                    && tsg.AcademicPeriodId == periodId);
        }
    }
}
