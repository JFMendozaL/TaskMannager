using Microsoft.EntityFrameworkCore;
using TaskService.Domain.Entities;
using TaskEntity = TaskService.Domain.Entities.Task;

namespace TaskService.Infrastructure.Data;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TaskSubmission> TaskSubmissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Task
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.Priority).HasConversion<int>();
            entity.Property(e => e.Grade).HasPrecision(5, 2);
            
            entity.HasIndex(e => e.CourseId);
            entity.HasIndex(e => e.CreatedByUserId);
            entity.HasIndex(e => e.AssignedToUserId);
        });

        // Configuración de TaskSubmission
        modelBuilder.Entity<TaskSubmission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SubmissionContent).IsRequired().HasMaxLength(5000);
            entity.Property(e => e.FileUrl).HasMaxLength(500);
            entity.Property(e => e.Grade).HasPrecision(5, 2);
            
            entity.HasOne(e => e.Task)
                .WithMany(t => t.Submissions)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasIndex(e => e.TaskId);
            entity.HasIndex(e => e.SubmittedByUserId);
        });
    }
}

