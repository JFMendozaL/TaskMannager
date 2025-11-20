namespace AcademicService.Application.DTOs
{
    public class TeacherSubjectGroupDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int AcademicPeriodId { get; set; }
        public DateTime AssignedDate { get; set; }
        public bool IsActive { get; set; }

        // Información expandida (opcional)
        public string? SubjectName { get; set; }
        public string? GroupName { get; set; }
        public string? PeriodName { get; set; }
    }

    public class CreateTeacherSubjectGroupDto
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int AcademicPeriodId { get; set; }
    }

    public class UpdateTeacherSubjectGroupDto
    {
        public bool? IsActive { get; set; }
    }
}
