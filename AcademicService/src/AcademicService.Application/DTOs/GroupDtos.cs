namespace AcademicService.Application.DTOs
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SchoolYear { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int StudentCount { get; set; }
    }

    public class CreateGroupDto
    {
        public string Name { get; set; } = string.Empty;
        public string SchoolYear { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
    }

    public class UpdateGroupDto
    {
        public string? Name { get; set; }
        public string? SchoolYear { get; set; }
        public string? Level { get; set; }
        public bool? IsActive { get; set; }
    }
}
