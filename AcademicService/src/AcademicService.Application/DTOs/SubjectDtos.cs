namespace AcademicService.Application.DTOs
{
    public class SubjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateSubjectDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Color { get; set; } = "#3B82F6";
    }

    public class UpdateSubjectDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public bool? IsActive { get; set; }
    }
}
