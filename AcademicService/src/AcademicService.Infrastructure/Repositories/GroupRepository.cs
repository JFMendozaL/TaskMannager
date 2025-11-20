using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using AcademicService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AcademicService.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AcademicDbContext _context;

        public GroupRepository(AcademicDbContext context) => _context = context;

        public async Task<IEnumerable<Group>> GetAllAsync() => await _context.Groups.ToListAsync();
        public async Task<IEnumerable<Group>> GetActiveAsync() => await _context.Groups.Where(g => g.IsActive).ToListAsync();
        public async Task<Group?> GetByIdAsync(int id) => await _context.Groups.FindAsync(id);
        public async Task<IEnumerable<Group>> GetByLevelAsync(string level) => await _context.Groups.Where(g => g.Level == level).ToListAsync();

        public async Task<Group> CreateAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> UpdateAsync(Group group)
        {
            group.UpdatedAt = DateTime.UtcNow;
            _context.Groups.Update(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var group = await GetByIdAsync(id);
            if (group == null) return false;
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id) => await _context.Groups.AnyAsync(g => g.Id == id);
    }
}
