namespace AcademicService.Application.DTOs
{
    public class ParentStudentDto
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public string Relationship { get; set; } = string.Empty;
        public DateTime LinkedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateParentStudentDto
    {
        public int ParentId { get; set; }
        public int StudentId { get; set; }
        public string Relationship { get; set; } = "padre"; // padre, madre, tutor, otro
    }

    public class UpdateParentStudentDto
    {
        public string? Relationship { get; set; }
        public bool? IsActive { get; set; }
    }
}
