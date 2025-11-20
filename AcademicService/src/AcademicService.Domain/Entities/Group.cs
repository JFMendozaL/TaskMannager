namespace AcademicService.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SchoolYear { get; set; } = string.Empty; // Ej: "4to A", "5to B"
        public string Level { get; set; } = string.Empty; // Ej: "Secundaria", "Preparatoria"
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<TeacherSubjectGroup> TeacherSubjectGroups { get; set; } = new List<TeacherSubjectGroup>();
        public ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
    }
}
