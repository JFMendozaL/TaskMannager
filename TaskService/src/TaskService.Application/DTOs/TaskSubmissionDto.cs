namespace TaskService.Application.DTOs;

public class TaskSubmissionDto
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int SubmittedByUserId { get; set; }
    public string SubmissionContent { get; set; } = string.Empty;
    public string? FileUrl { get; set; }
    public DateTime SubmittedAt { get; set; }
    public decimal? Grade { get; set; }
    public string? Feedback { get; set; }
    public DateTime? GradedAt { get; set; }
    public int? GradedByUserId { get; set; }
}

public class CreateTaskSubmissionDto
{
    public int TaskId { get; set; }
    public string SubmissionContent { get; set; } = string.Empty;
    public string? FileUrl { get; set; }
}

public class GradeTaskSubmissionDto
{
    public decimal Grade { get; set; }
    public string? Feedback { get; set; }
}

