using AcademicService.Application.DTOs;
using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademicService.API.Controllers
{
    [ApiController]
    [Route("api/groups")]
    [Produces("application/json")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _repository;

        public GroupsController(IGroupRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los grupos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<GroupDto>>>> GetAll()
        {
            var groups = await _repository.GetAllAsync();
            var dtos = groups.Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name,
                SchoolYear = g.SchoolYear,
                Level = g.Level,
                IsActive = g.IsActive,
                CreatedAt = g.CreatedAt,
                UpdatedAt = g.UpdatedAt,
                StudentCount = 0 // Puedes implementar el conteo después
            });

            return Ok(ApiResponse<IEnumerable<GroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene un grupo por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GroupDto>>> GetById(int id)
        {
            var group = await _repository.GetByIdAsync(id);
            if (group == null)
                return NotFound(ApiResponse<GroupDto>.ErrorResponse("Grupo no encontrado"));

            var dto = new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                SchoolYear = group.SchoolYear,
                Level = group.Level,
                IsActive = group.IsActive,
                CreatedAt = group.CreatedAt,
                UpdatedAt = group.UpdatedAt,
                StudentCount = 0
            };

            return Ok(ApiResponse<GroupDto>.SuccessResponse(dto));
        }

        /// <summary>
        /// Obtiene grupos por nivel
        /// </summary>
        [HttpGet("level/{level}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<GroupDto>>>> GetByLevel(string level)
        {
            var groups = await _repository.GetByLevelAsync(level);
            var dtos = groups.Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name,
                SchoolYear = g.SchoolYear,
                Level = g.Level,
                IsActive = g.IsActive,
                CreatedAt = g.CreatedAt,
                UpdatedAt = g.UpdatedAt,
                StudentCount = 0
            });

            return Ok(ApiResponse<IEnumerable<GroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene grupos activos
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<ApiResponse<IEnumerable<GroupDto>>>> GetActive()
        {
            var groups = await _repository.GetActiveAsync();
            var dtos = groups.Select(g => new GroupDto
            {
                Id = g.Id,
                Name = g.Name,
                SchoolYear = g.SchoolYear,
                Level = g.Level,
                IsActive = g.IsActive,
                CreatedAt = g.CreatedAt,
                UpdatedAt = g.UpdatedAt,
                StudentCount = 0
            });

            return Ok(ApiResponse<IEnumerable<GroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Crea un nuevo grupo
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<GroupDto>>> Create([FromBody] CreateGroupDto dto)
        {
            var group = new Group
            {
                Name = dto.Name,
                SchoolYear = dto.SchoolYear,
                Level = dto.Level,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(group);

            var resultDto = new GroupDto
            {
                Id = created.Id,
                Name = created.Name,
                SchoolYear = created.SchoolYear,
                Level = created.Level,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt,
                StudentCount = 0
            };

            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<GroupDto>.SuccessResponse(resultDto, "Grupo creado exitosamente"));
        }

        /// <summary>
        /// Actualiza un grupo existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GroupDto>>> Update(int id, [FromBody] UpdateGroupDto dto)
        {
            var group = await _repository.GetByIdAsync(id);
            if (group == null)
                return NotFound(ApiResponse<GroupDto>.ErrorResponse("Grupo no encontrado"));

            if (dto.Name != null) group.Name = dto.Name;
            if (dto.SchoolYear != null) group.SchoolYear = dto.SchoolYear;
            if (dto.Level != null) group.Level = dto.Level;
            if (dto.IsActive.HasValue) group.IsActive = dto.IsActive.Value;
            group.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(group);

            var resultDto = new GroupDto
            {
                Id = updated.Id,
                Name = updated.Name,
                SchoolYear = updated.SchoolYear,
                Level = updated.Level,
                IsActive = updated.IsActive,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt,
                StudentCount = 0
            };

            return Ok(ApiResponse<GroupDto>.SuccessResponse(resultDto, "Grupo actualizado exitosamente"));
        }

        /// <summary>
        /// Elimina un grupo
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<bool>.ErrorResponse("Grupo no encontrado"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Grupo eliminado exitosamente"));
        }
    }
}
