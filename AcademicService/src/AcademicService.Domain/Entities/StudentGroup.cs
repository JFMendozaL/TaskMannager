namespace AcademicService.Domain.Entities
{
    // Tabla de relación: Estudiante-Grupo (Matrícula)
    public class StudentGroup
    {
        public int Id { get; set; }
        
        // Foreign Keys
        public int StudentId { get; set; } // Referencia a UserService
        public int GroupId { get; set; }
        
        public int? ListNumber { get; set; } // Número de lista
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public Group Group { get; set; } = null!;
    }
}
