namespace AcademicService.Domain.Entities
{
    // Tabla de relación: Padre-Estudiante
    public class ParentStudent
    {
        public int Id { get; set; }
        
        // Foreign Keys
        public int ParentId { get; set; } // Referencia a UserService
        public int StudentId { get; set; } // Referencia a UserService
        
        public string Relationship { get; set; } = string.Empty; // padre, madre, tutor, otro
        public DateTime LinkedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
