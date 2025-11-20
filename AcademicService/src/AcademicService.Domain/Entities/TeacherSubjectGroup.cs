namespace AcademicService.Domain.Entities
{
    // Tabla de relación: Profesor-Materia-Grupo
    public class TeacherSubjectGroup
    {
        public int Id { get; set; }
        
        // Foreign Keys
        public int TeacherId { get; set; } // Referencia a UserService
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int AcademicPeriodId { get; set; }
        
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Subject Subject { get; set; } = null!;
        public Group Group { get; set; } = null!;
        public AcademicPeriod AcademicPeriod { get; set; } = null!;
    }
}
