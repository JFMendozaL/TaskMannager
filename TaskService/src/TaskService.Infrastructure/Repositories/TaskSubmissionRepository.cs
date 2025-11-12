using Microsoft.EntityFrameworkCore;
using TaskService.Domain.Entities;
using TaskService.Domain.Interfaces;
using TaskService.Infrastructure.Data;

namespace TaskService.Infrastructure.Repositories;

public class TaskSubmissionRepository : ITaskSubmissionRepository
{
    private readonly TaskDbContext _context;

    public TaskSubmissionRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<TaskSubmission?> GetByIdAsync(int id)
    {
        return await _context.TaskSubmissions
            .Include(s => s.Task)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<IEnumerable<TaskSubmission>> GetByTaskIdAsync(int taskId)
    {
        return await _context.TaskSubmissions
            .Include(s => s.Task)
            .Where(s => s.TaskId == taskId)
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskSubmission>> GetByUserIdAsync(int userId)
    {
        return await _context.TaskSubmissions
            .Include(s => s.Task)
            .Where(s => s.SubmittedByUserId == userId)
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync();
    }

    public async Task<TaskSubmission?> GetByTaskAndUserIdAsync(int taskId, int userId)
    {
        return await _context.TaskSubmissions
            .Include(s => s.Task)
            .FirstOrDefaultAsync(s => s.TaskId == taskId && s.SubmittedByUserId == userId);
    }

    public async Task<TaskSubmission> CreateAsync(TaskSubmission submission)
    {
        submission.SubmittedAt = DateTime.UtcNow;
        _context.TaskSubmissions.Add(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    public async Task<TaskSubmission> UpdateAsync(TaskSubmission submission)
    {
        _context.TaskSubmissions.Update(submission);
        await _context.SaveChangesAsync();
        return submission;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var submission = await _context.TaskSubmissions.FindAsync(id);
        if (submission == null)
            return false;

        _context.TaskSubmissions.Remove(submission);
        await _context.SaveChangesAsync();
        return true;
    }
}

