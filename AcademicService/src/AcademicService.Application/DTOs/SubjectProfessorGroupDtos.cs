namespace AcademicService.Application.DTOs
{
    public class SubjectProfessorGroupDto
    {
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public string ProfessorName { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public int AcademicPeriodId { get; set; }
        public string AcademicPeriodName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateSubjectProfessorGroupDto
    {
        public int ProfessorId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int AcademicPeriodId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class UpdateSubjectProfessorGroupDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class AssignProfessorToSubjectDto
    {
        public int ProfessorId { get; set; }
        public List<int> SubjectIds { get; set; } = new();
        public List<int> GroupIds { get; set; } = new();
        public int AcademicPeriodId { get; set; }
    }
}
