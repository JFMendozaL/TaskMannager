namespace AcademicService.Domain.Entities
{
    /// <summary>
    /// Tabla de relación: Asigna un profesor a una materia en un grupo específico para un período académico
    /// </summary>
    public class SubjectProfessorGroup
    {
        public int Id { get; set; }
        
        // Foreign Keys
        public int ProfessorId { get; set; } // FK a UserService (Profesor)
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public int AcademicPeriodId { get; set; }

        // Metadata
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relaciones
        public virtual Subject Subject { get; set; } = null!;
        public virtual Group Group { get; set; } = null!;
        public virtual AcademicPeriod AcademicPeriod { get; set; } = null!;
    }
}
