using AcademicService.Application.DTOs;
using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademicService.API.Controllers
{
    [ApiController]
    [Route("api/teacher-assignments")]
    [Produces("application/json")]
    public class TeacherAssignmentsController : ControllerBase
    {
        private readonly ITeacherSubjectGroupRepository _repository;

        public TeacherAssignmentsController(ITeacherSubjectGroupRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todas las asignaciones de profesores
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TeacherSubjectGroupDto>>>> GetAll()
        {
            var assignments = await _repository.GetAllAsync();
            var dtos = assignments.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<TeacherSubjectGroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene una asignación por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TeacherSubjectGroupDto>>> GetById(int id)
        {
            var assignment = await _repository.GetByIdAsync(id);
            if (assignment == null)
                return NotFound(ApiResponse<TeacherSubjectGroupDto>.ErrorResponse("Asignación no encontrada"));

            return Ok(ApiResponse<TeacherSubjectGroupDto>.SuccessResponse(MapToDto(assignment)));
        }

        /// <summary>
        /// Obtiene asignaciones por profesor
        /// </summary>
        [HttpGet("teacher/{teacherId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TeacherSubjectGroupDto>>>> GetByTeacher(int teacherId)
        {
            var assignments = await _repository.GetByTeacherIdAsync(teacherId);
            var dtos = assignments.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<TeacherSubjectGroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene asignaciones por grupo
        /// </summary>
        [HttpGet("group/{groupId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TeacherSubjectGroupDto>>>> GetByGroup(int groupId)
        {
            var assignments = await _repository.GetByGroupIdAsync(groupId);
            var dtos = assignments.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<TeacherSubjectGroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene asignaciones por materia
        /// </summary>
        [HttpGet("subject/{subjectId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TeacherSubjectGroupDto>>>> GetBySubject(int subjectId)
        {
            var assignments = await _repository.GetBySubjectIdAsync(subjectId);
            var dtos = assignments.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<TeacherSubjectGroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene asignaciones por período académico
        /// </summary>
        [HttpGet("period/{periodId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TeacherSubjectGroupDto>>>> GetByPeriod(int periodId)
        {
            var assignments = await _repository.GetByPeriodIdAsync(periodId);
            var dtos = assignments.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<TeacherSubjectGroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Crea una nueva asignación
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<TeacherSubjectGroupDto>>> Create([FromBody] CreateTeacherSubjectGroupDto dto)
        {
            // Verificar si ya existe la asignación
            if (await _repository.ExistsAsync(dto.TeacherId, dto.SubjectId, dto.GroupId, dto.AcademicPeriodId))
                return BadRequest(ApiResponse<TeacherSubjectGroupDto>.ErrorResponse("Ya existe esta asignación"));

            var assignment = new TeacherSubjectGroup
            {
                TeacherId = dto.TeacherId,
                SubjectId = dto.SubjectId,
                GroupId = dto.GroupId,
                AcademicPeriodId = dto.AcademicPeriodId,
                AssignedDate = DateTime.UtcNow,
                IsActive = true
            };

            var created = await _repository.CreateAsync(assignment);

            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<TeacherSubjectGroupDto>.SuccessResponse(MapToDto(created), "Asignación creada exitosamente"));
        }

        /// <summary>
        /// Actualiza una asignación existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TeacherSubjectGroupDto>>> Update(int id, [FromBody] UpdateTeacherSubjectGroupDto dto)
        {
            var assignment = await _repository.GetByIdAsync(id);
            if (assignment == null)
                return NotFound(ApiResponse<TeacherSubjectGroupDto>.ErrorResponse("Asignación no encontrada"));

            if (dto.IsActive.HasValue) assignment.IsActive = dto.IsActive.Value;

            var updated = await _repository.UpdateAsync(assignment);

            return Ok(ApiResponse<TeacherSubjectGroupDto>.SuccessResponse(MapToDto(updated), "Asignación actualizada exitosamente"));
        }

        /// <summary>
        /// Elimina una asignación
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<bool>.ErrorResponse("Asignación no encontrada"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Asignación eliminada exitosamente"));
        }

        private static TeacherSubjectGroupDto MapToDto(TeacherSubjectGroup assignment)
        {
            return new TeacherSubjectGroupDto
            {
                Id = assignment.Id,
                TeacherId = assignment.TeacherId,
                SubjectId = assignment.SubjectId,
                GroupId = assignment.GroupId,
                AcademicPeriodId = assignment.AcademicPeriodId,
                AssignedDate = assignment.AssignedDate,
                IsActive = assignment.IsActive,
                SubjectName = assignment.Subject?.Name,
                GroupName = assignment.Group?.Name,
                PeriodName = assignment.AcademicPeriod?.Name
            };
        }
    }
}
