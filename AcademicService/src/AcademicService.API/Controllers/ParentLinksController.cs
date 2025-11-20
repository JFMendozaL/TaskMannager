using AcademicService.Application.DTOs;
using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademicService.API.Controllers
{
    [ApiController]
    [Route("api/parent-links")]
    [Produces("application/json")]
    public class ParentLinksController : ControllerBase
    {
        private readonly IParentStudentRepository _repository;

        public ParentLinksController(IParentStudentRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los vínculos padre-estudiante
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ParentStudentDto>>>> GetAll()
        {
            var links = await _repository.GetAllAsync();
            var dtos = links.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<ParentStudentDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene un vínculo por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ParentStudentDto>>> GetById(int id)
        {
            var link = await _repository.GetByIdAsync(id);
            if (link == null)
                return NotFound(ApiResponse<ParentStudentDto>.ErrorResponse("Vínculo no encontrado"));

            return Ok(ApiResponse<ParentStudentDto>.SuccessResponse(MapToDto(link)));
        }

        /// <summary>
        /// Obtiene vínculos por padre
        /// </summary>
        [HttpGet("parent/{parentId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ParentStudentDto>>>> GetByParent(int parentId)
        {
            var links = await _repository.GetByParentIdAsync(parentId);
            var dtos = links.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<ParentStudentDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene vínculos por estudiante
        /// </summary>
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ParentStudentDto>>>> GetByStudent(int studentId)
        {
            var links = await _repository.GetByStudentIdAsync(studentId);
            var dtos = links.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<ParentStudentDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Crea un vínculo padre-estudiante
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ParentStudentDto>>> Create([FromBody] CreateParentStudentDto dto)
        {
            // Verificar si ya existe el vínculo
            if (await _repository.ExistsAsync(dto.ParentId, dto.StudentId))
                return BadRequest(ApiResponse<ParentStudentDto>.ErrorResponse("El vínculo ya existe"));

            var link = new ParentStudent
            {
                ParentId = dto.ParentId,
                StudentId = dto.StudentId,
                Relationship = dto.Relationship,
                LinkedDate = DateTime.UtcNow,
                IsActive = true
            };

            var created = await _repository.CreateAsync(link);

            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ParentStudentDto>.SuccessResponse(MapToDto(created), "Vínculo creado exitosamente"));
        }

        /// <summary>
        /// Actualiza un vínculo
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ParentStudentDto>>> Update(int id, [FromBody] UpdateParentStudentDto dto)
        {
            var link = await _repository.GetByIdAsync(id);
            if (link == null)
                return NotFound(ApiResponse<ParentStudentDto>.ErrorResponse("Vínculo no encontrado"));

            if (dto.Relationship != null) link.Relationship = dto.Relationship;
            if (dto.IsActive.HasValue) link.IsActive = dto.IsActive.Value;

            var updated = await _repository.UpdateAsync(link);

            return Ok(ApiResponse<ParentStudentDto>.SuccessResponse(MapToDto(updated), "Vínculo actualizado exitosamente"));
        }

        /// <summary>
        /// Elimina un vínculo
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<bool>.ErrorResponse("Vínculo no encontrado"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Vínculo eliminado exitosamente"));
        }

        private static ParentStudentDto MapToDto(ParentStudent link)
        {
            return new ParentStudentDto
            {
                Id = link.Id,
                ParentId = link.ParentId,
                StudentId = link.StudentId,
                Relationship = link.Relationship,
                LinkedDate = link.LinkedDate,
                IsActive = link.IsActive
            };
        }
    }
}
