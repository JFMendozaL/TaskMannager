using AcademicService.Application.DTOs;
using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AcademicService.API.Controllers
{
    [ApiController]
    [Route("api/academic-periods")]
    [Produces("application/json")]
    public class AcademicPeriodsController : ControllerBase
    {
        private readonly IAcademicPeriodRepository _repository;

        public AcademicPeriodsController(IAcademicPeriodRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los períodos académicos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AcademicPeriodDto>>>> GetAll()
        {
            var periods = await _repository.GetAllAsync();
            var dtos = periods.Select(p => new AcademicPeriodDto
            {
                Id = p.Id,
                Name = p.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            });

            return Ok(ApiResponse<IEnumerable<AcademicPeriodDto>>.SuccessResponse(dtos));
        }

        /// <summary>
        /// Obtiene un período académico por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AcademicPeriodDto>>> GetById(int id)
        {
            var period = await _repository.GetByIdAsync(id);
            if (period == null)
                return NotFound(ApiResponse<AcademicPeriodDto>.ErrorResponse("Período académico no encontrado"));

            var dto = new AcademicPeriodDto
            {
                Id = period.Id,
                Name = period.Name,
                StartDate = period.StartDate,
                EndDate = period.EndDate,
                IsActive = period.IsActive,
                CreatedAt = period.CreatedAt,
                UpdatedAt = period.UpdatedAt
            };

            return Ok(ApiResponse<AcademicPeriodDto>.SuccessResponse(dto));
        }

        /// <summary>
        /// Obtiene el período académico actual
        /// </summary>
        [HttpGet("current")]
        public async Task<ActionResult<ApiResponse<AcademicPeriodDto>>> GetCurrent()
        {
            var period = await _repository.GetCurrentAsync();
            if (period == null)
                return NotFound(ApiResponse<AcademicPeriodDto>.ErrorResponse("No hay período académico activo"));

            var dto = new AcademicPeriodDto
            {
                Id = period.Id,
                Name = period.Name,
                StartDate = period.StartDate,
                EndDate = period.EndDate,
                IsActive = period.IsActive,
                CreatedAt = period.CreatedAt,
                UpdatedAt = period.UpdatedAt
            };

            return Ok(ApiResponse<AcademicPeriodDto>.SuccessResponse(dto));
        }


        /// <summary>
        /// Crea un nuevo período académico
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<AcademicPeriodDto>>> Create([FromBody] CreateAcademicPeriodDto dto)
        {
            if (dto.EndDate <= dto.StartDate)
                return BadRequest(ApiResponse<AcademicPeriodDto>.ErrorResponse("La fecha de fin debe ser posterior a la fecha de inicio"));

            var period = new AcademicPeriod
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = false
            };

            var created = await _repository.CreateAsync(period);

            var resultDto = new AcademicPeriodDto
            {
                Id = created.Id,
                Name = created.Name,
                StartDate = created.StartDate,
                EndDate = created.EndDate,
                IsActive = created.IsActive,
                CreatedAt = created.CreatedAt,
                UpdatedAt = created.UpdatedAt
            };

            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<AcademicPeriodDto>.SuccessResponse(resultDto, "Período académico creado exitosamente"));
        }

        /// <summary>
        /// Actualiza un período académico existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<AcademicPeriodDto>>> Update(int id, [FromBody] UpdateAcademicPeriodDto dto)
        {
            var period = await _repository.GetByIdAsync(id);
            if (period == null)
                return NotFound(ApiResponse<AcademicPeriodDto>.ErrorResponse("Período académico no encontrado"));

            if (dto.Name != null) period.Name = dto.Name;
            if (dto.StartDate.HasValue) period.StartDate = dto.StartDate.Value;
            if (dto.EndDate.HasValue) period.EndDate = dto.EndDate.Value;
            if (dto.IsActive.HasValue) period.IsActive = dto.IsActive.Value;
            period.UpdatedAt = DateTime.UtcNow;

            if (period.EndDate <= period.StartDate)
                return BadRequest(ApiResponse<AcademicPeriodDto>.ErrorResponse("La fecha de fin debe ser posterior a la fecha de inicio"));

            var updated = await _repository.UpdateAsync(period);

            var resultDto = new AcademicPeriodDto
            {
                Id = updated.Id,
                Name = updated.Name,
                StartDate = updated.StartDate,
                EndDate = updated.EndDate,
                IsActive = updated.IsActive,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt
            };

            return Ok(ApiResponse<AcademicPeriodDto>.SuccessResponse(resultDto, "Período académico actualizado exitosamente"));
        }

        /// <summary>
        /// Activa un período académico
        /// </summary>
        [HttpPost("{id}/activate")]
        public async Task<ActionResult<ApiResponse<AcademicPeriodDto>>> Activate(int id)
        {
            var period = await _repository.GetByIdAsync(id);
            if (period == null)
                return NotFound(ApiResponse<AcademicPeriodDto>.ErrorResponse("Período académico no encontrado"));

            period.IsActive = true;
            period.UpdatedAt = DateTime.UtcNow;
            var updated = await _repository.UpdateAsync(period);

            var dto = new AcademicPeriodDto
            {
                Id = updated.Id,
                Name = updated.Name,
                StartDate = updated.StartDate,
                EndDate = updated.EndDate,
                IsActive = updated.IsActive,
                CreatedAt = updated.CreatedAt,
                UpdatedAt = updated.UpdatedAt
            };

            return Ok(ApiResponse<AcademicPeriodDto>.SuccessResponse(dto, "Período académico activado exitosamente"));
        }

        /// <summary>
        /// Elimina un período académico
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<bool>.ErrorResponse("Período académico no encontrado"));

            return Ok(ApiResponse<bool>.SuccessResponse(true, "Período académico eliminado exitosamente"));
        }
    }
}
