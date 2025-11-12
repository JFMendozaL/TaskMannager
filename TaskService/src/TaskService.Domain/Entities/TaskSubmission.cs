namespace TaskService.Domain.Entities;

public class TaskSubmission
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int SubmittedByUserId { get; set; }
    public string SubmissionContent { get; set; } = string.Empty;
    public string? FileUrl { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public decimal? Grade { get; set; }
    public string? Feedback { get; set; }
    public DateTime? GradedAt { get; set; }
    public int? GradedByUserId { get; set; }
    
    // Relación
    public Task Task { get; set; } = null!;
}

