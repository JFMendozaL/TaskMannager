using AcademicService.Application.DTOs;
using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademicService.API.Controllers
{
    [ApiController]
    [Route("api/subjects")]
    [Produces("application/json")]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectRepository _repository;

        public SubjectsController(ISubjectRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todas las materias
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<SubjectDto>>>> GetAll()
        {
            var subjects = await _repository.GetAllAsync();
            var dtos = subjects.Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Code = s.Code,
                Color = s.ColorCode ?? "#3B82F6",
                IsActive = s.IsActive,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt
            });

            return Ok(ApiResponse<IEnumerable<SubjectDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene una materia por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<SubjectDto>>> GetById(int id)
        {
            var subject = await _repository.GetByIdAsync(id);
            if (subject == null)
                return NotFound(ApiResponse<SubjectDto>.ErrorResponse("Materia no encontrada"));

            var dto = new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                Code = subject.Code,
                Color = subject.ColorCode ?? "#3B82F6",
                IsActive = subject.IsActive,
                CreatedAt = subject.CreatedAt,
                UpdatedAt = subject.UpdatedAt
            };

            return Ok(ApiResponse<SubjectDto>.SuccessResponse(dto));
        }

        /// <summary>
        /// Crea una nueva materia
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<SubjectDto>>> Create([FromBody] CreateSubjectDto dto)
        {
            if (await _repository.CodeExistsAsync(dto.Code))
                return BadRequest(ApiResponse<SubjectDto>.ErrorResponse("El código de materia ya existe"));

            var subject = new Subject
            {
                Name = dto.Name,
                Description = dto.Description,
                Code = dto.Code,
                ColorCode = dto.Color
            };

            var created = await _repository.CreateAsync(subject);

            var resultDto = new SubjectDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Code = created.Code,
                Color = created.ColorCode ?? "#3B82F6",
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, 
                ApiResponse<SubjectDto>.SuccessResponse(resultDto, "Materia creada exitosamente"));
        }

        /// <summary>
        /// Actualiza una materia existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<SubjectDto>>> Update(int id, [FromBody] UpdateSubjectDto dto)
        {
            var subject = await _repository.GetByIdAsync(id);
            if (subject == null)
                return NotFound(ApiResponse<SubjectDto>.ErrorResponse("Materia no encontrada"));

            if (dto.Name != null) subject.Name = dto.Name;
            if (dto.Description != null) subject.Description = dto.Description;
            if (dto.Color != null) subject.ColorCode = dto.Color;
            if (dto.IsActive.HasValue) subject.IsActive = dto.IsActive.Value;
            subject.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(subject);

            var resultDto = new SubjectDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Description = updated.Description,
                Code = updated.Code,
                Color = updated.ColorCode ?? "#3B82F6",
                IsActive = updated.IsActive,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt
            };

            return Ok(ApiResponse<SubjectDto>.SuccessResponse(resultDto, "Materia actualizada exitosamente"));
        }

        /// <summary>
        /// Elimina una materia
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<bool>.ErrorResponse("Materia no encontrada"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Materia eliminada exitosamente"));
        }
    }
}
