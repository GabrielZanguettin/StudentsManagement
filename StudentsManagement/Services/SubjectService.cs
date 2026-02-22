using StudentsManagement.Appliers.Subjects;
using StudentsManagement.Dtos.Subjects;
using StudentsManagement.Entities;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Common;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Interfaces.IServices;
using StudentsManagement.Mappings.Subjects;
using StudentsManagement.Validations.Common;
using StudentsManagement.Validations.Domain.Subjects;

namespace StudentsManagement.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            this._subjectRepository = subjectRepository;
        }

        public async Task<List<SubjectResponseDto>> GetAll()
        {
            var subjects = await this._subjectRepository.GetAll();

            return subjects
                .Select(s => s.ToSubjectResponseDto())
                .ToList();
        }

        public async Task<SubjectResponseDto> GetById(Guid id)
        {
            var subject = await this._subjectRepository.GetById(id);
            if (subject is null)
                throw NotFoundException.For(EntityName.Subject, id);

            return subject.ToSubjectResponseDto();
        }

        public async Task<SubjectResponseDto> Create(CreateSubjectDto dto)
        {
            var missing = RequiredFieldsValidator.GetMissing(
                (nameof(dto.Name), string.IsNullOrWhiteSpace(dto.Name))
            );

            if (missing.Length > 0)
                throw BadRequestException.RequiredFields(missing);

            var name = dto.Name.Trim();

            SubjectValidator.ValidateCreate(name);

            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            var created = await this._subjectRepository.Create(subject);

            return await this.GetById(created.Id);
        }

        public async Task<SubjectResponseDto> Update(Guid id, UpdateSubjectDto dto)
        {
            var subject = await this._subjectRepository.GetById(id);
            if (subject is null)
                throw NotFoundException.For(EntityName.Subject, id);

            PatchValidator.EnsureHasAny(dto.Name);
            PatchValidator.EnsureNotBlank(nameof(dto.Name), dto.Name);

            SubjectValidator.ValidateUpdate(dto.Name);

            SubjectPatchApplier.ApplySubjectPatch(subject, dto);

            var updatedSubject = await this._subjectRepository.Update(subject);

            return await this.GetById(updatedSubject.Id);
        }

        public async Task<SubjectResponseDto> Delete(Guid id)
        {
            var subject = await this._subjectRepository.GetById(id);
            if (subject is null)
                throw NotFoundException.For(EntityName.Subject, id);

            await this._subjectRepository.Delete(subject);
            return subject.ToSubjectResponseDto();
        }
    }
}
