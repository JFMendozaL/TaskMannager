using AcademicService.Application.DTOs;
using AcademicService.Domain.Entities;
using AcademicService.Domain.Interfaces;

namespace AcademicService.Application.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _repository;

        public SubjectService(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<SubjectDto?> GetByIdAsync(int id)
        {
            var subject = await _repository.GetByIdAsync(id);
            return subject == null ? null : MapToDto(subject);
        }

        public async Task<SubjectDto?> GetByCodeAsync(string code)
        {
            var subject = await _repository.GetByCodeAsync(code);
            return subject == null ? null : MapToDto(subject);
        }

        public async Task<IEnumerable<SubjectDto>> GetAllAsync()
        {
            var subjects = await _repository.GetAllAsync();
            return subjects.Select(MapToDto);
        }

        public async Task<IEnumerable<SubjectDto>> GetActiveAsync()
        {
            var subjects = await _repository.GetActiveAsync();
            return subjects.Select(MapToDto);
        }

        public async Task<SubjectDto> CreateAsync(CreateSubjectDto dto)
        {
            // Validar que el código no exista
            if (await _repository.CodeExistsAsync(dto.Code))
            {
                throw new InvalidOperationException($"Ya existe una materia con el código {dto.Code}");
            }

            var subject = new Subject
            {
                Name = dto.Name,
                Description = dto.Description,
                Code = dto.Code.ToUpper(),
                ColorCode = dto.Color,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(subject);
            return MapToDto(created);
        }

        public async Task<SubjectDto> UpdateAsync(int id, UpdateSubjectDto dto)
        {
            var subject = await _repository.GetByIdAsync(id);
            if (subject == null)
            {
                throw new KeyNotFoundException($"Materia con ID {id} no encontrada");
            }

            if (dto.Name != null) subject.Name = dto.Name;
            if (dto.Description != null) subject.Description = dto.Description;
            if (dto.Color != null) subject.ColorCode = dto.Color;
            if (dto.IsActive.HasValue) subject.IsActive = dto.IsActive.Value;
            
            subject.UpdatedAt = DateTime.UtcNow;

            var updated = await _repository.UpdateAsync(subject);
            return MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static SubjectDto MapToDto(Subject subject)
        {
            return new SubjectDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                Code = subject.Code,
                Color = subject.ColorCode ?? string.Empty,
                IsActive = subject.IsActive,
                CreatedAt = subject.CreatedAt,
                UpdatedAt = subject.UpdatedAt
            };
        }
    }
}
