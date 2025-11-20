using AcademicService.Application.DTOs;

namespace AcademicService.Application.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetAllGroupsAsync();
        Task<IEnumerable<GroupDto>> GetActiveGroupsAsync();
        Task<GroupDto?> GetGroupByIdAsync(int id);
        Task<IEnumerable<GroupDto>> GetGroupsByLevelAsync(string level);
        Task<GroupDto> CreateGroupAsync(CreateGroupDto dto);
        Task<GroupDto> UpdateGroupAsync(int id, UpdateGroupDto dto);
        Task<bool> DeleteGroupAsync(int id);
    }
}
