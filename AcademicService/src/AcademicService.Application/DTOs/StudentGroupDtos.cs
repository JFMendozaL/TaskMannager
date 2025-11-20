namespace AcademicService.Application.DTOs
{
    public class StudentGroupDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public int? ListNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsActive { get; set; }

        // Información expandida (opcional)
        public string? GroupName { get; set; }
    }

    public class CreateStudentGroupDto
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        public int? ListNumber { get; set; }
    }

    public class UpdateStudentGroupDto
    {
        public int? ListNumber { get; set; }
        public bool? IsActive { get; set; }
    }
}
