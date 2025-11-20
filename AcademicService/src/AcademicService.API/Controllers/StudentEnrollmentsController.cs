using AcademicService.Application.DTOs;
using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademicService.API.Controllers
{
    [ApiController]
    [Route("api/student-enrollments")]
    [Produces("application/json")]
    public class StudentEnrollmentsController : ControllerBase
    {
        private readonly IStudentGroupRepository _repository;

        public StudentEnrollmentsController(IStudentGroupRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todas las matrículas
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<StudentGroupDto>>>> GetAll()
        {
            var enrollments = await _repository.GetAllAsync();
            var dtos = enrollments.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<StudentGroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene una matrícula por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<StudentGroupDto>>> GetById(int id)
        {
            var enrollment = await _repository.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound(ApiResponse<StudentGroupDto>.ErrorResponse("Matrícula no encontrada"));

            return Ok(ApiResponse<StudentGroupDto>.SuccessResponse(MapToDto(enrollment)));
        }

        /// <summary>
        /// Obtiene matrículas por estudiante
        /// </summary>
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StudentGroupDto>>>> GetByStudent(int studentId)
        {
            var enrollments = await _repository.GetByStudentIdAsync(studentId);
            var dtos = enrollments.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<StudentGroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene matrículas por grupo
        /// </summary>
        [HttpGet("group/{groupId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<StudentGroupDto>>>> GetByGroup(int groupId)
        {
            var enrollments = await _repository.GetByGroupIdAsync(groupId);
            var dtos = enrollments.Select(MapToDto);

            return Ok(ApiResponse<IEnumerable<StudentGroupDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Matricula un estudiante en un grupo
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<StudentGroupDto>>> Create([FromBody] CreateStudentGroupDto dto)
        {
            // Verificar si ya existe la matrícula
            if (await _repository.ExistsAsync(dto.StudentId, dto.GroupId))
                return BadRequest(ApiResponse<StudentGroupDto>.ErrorResponse("El estudiante ya está matriculado en este grupo"));

            var enrollment = new StudentGroup
            {
                StudentId = dto.StudentId,
                GroupId = dto.GroupId,
                ListNumber = dto.ListNumber,
                EnrollmentDate = DateTime.UtcNow,
                IsActive = true
            };

            var created = await _repository.CreateAsync(enrollment);

            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<StudentGroupDto>.SuccessResponse(MapToDto(created), "Estudiante matriculado exitosamente"));
        }

        /// <summary>
        /// Actualiza una matrícula
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<StudentGroupDto>>> Update(int id, [FromBody] UpdateStudentGroupDto dto)
        {
            var enrollment = await _repository.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound(ApiResponse<StudentGroupDto>.ErrorResponse("Matrícula no encontrada"));

            if (dto.ListNumber.HasValue) enrollment.ListNumber = dto.ListNumber.Value;
            if (dto.IsActive.HasValue) enrollment.IsActive = dto.IsActive.Value;

            var updated = await _repository.UpdateAsync(enrollment);

            return Ok(ApiResponse<StudentGroupDto>.SuccessResponse(MapToDto(updated), "Matrícula actualizada exitosamente"));
        }

        /// <summary>
        /// Elimina una matrícula (dar de baja al estudiante)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<bool>.ErrorResponse("Matrícula no encontrada"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Matrícula eliminada exitosamente"));
        }

        private static StudentGroupDto MapToDto(StudentGroup enrollment)
        {
            return new StudentGroupDto
            {
                Id = enrollment.Id,
                StudentId = enrollment.StudentId,
                GroupId = enrollment.GroupId,
                ListNumber = enrollment.ListNumber,
                EnrollmentDate = enrollment.EnrollmentDate,
                IsActive = enrollment.IsActive,
                GroupName = enrollment.Group?.Name
            };
        }
    }
}
