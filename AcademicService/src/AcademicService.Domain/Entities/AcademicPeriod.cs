namespace AcademicService.Domain.Entities
{
    public class AcademicPeriod
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Ej: "Bimestre 1", "Trimestre 2"
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<TeacherSubjectGroup> TeacherSubjectGroups { get; set; } = new List<TeacherSubjectGroup>();
    }
}
