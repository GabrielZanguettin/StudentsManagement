using StudentsManagement.Entities;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Common;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Validations.Common;

namespace StudentsManagement.Validations.Domain.Courses
{
    public static class CourseValidator
    {
        public const int NameMaxLength = 150;

        public static void ValidateName(string name)
        {
            var trimmed = name.Trim();

            StringLengthValidator.ValidateMax(nameof(Course.Name), trimmed, NameMaxLength);
        }

        public static async Task ValidateInstitutionAsync(
            Guid institutionId,
            IInstitutionRepository institutionRepository)
        {
            var institution = await institutionRepository.GetById(institutionId);

            if (institution is null)
                throw NotFoundException.For(EntityName.Institution, institutionId);
        }

        public static async Task ValidateSubjectsAsync(
            IEnumerable<Guid>? subjectIds,
            ISubjectRepository subjectRepository)
        {
            if (subjectIds is null) return;

            var ids = subjectIds.Distinct().ToList();
            if (ids.Count == 0) return;

            var existingIds = await subjectRepository.GetExistingIds(ids);

            var missingId = ids.FirstOrDefault(id => !existingIds.Contains(id));

            if (missingId != Guid.Empty)
                throw NotFoundException.For(EntityName.Subject, missingId);
        }

        public static async Task ValidateCreateAsync(
            string name,
            Guid institutionId,
            IEnumerable<Guid>? subjectIds,
            IInstitutionRepository institutionRepository,
            ISubjectRepository subjectRepository)
        {
            ValidateName(name);
            await ValidateInstitutionAsync(institutionId, institutionRepository);
            await ValidateSubjectsAsync(subjectIds, subjectRepository);
        }

        public static async Task ValidateUpdateAsync(
            string? name,
            Guid? institutionId,
            IEnumerable<Guid>? subjectIds,
            IInstitutionRepository institutionRepository,
            ISubjectRepository subjectRepository)
        {
            if (name is not null)
                ValidateName(name);

            if (institutionId.HasValue)
                await ValidateInstitutionAsync(institutionId.Value, institutionRepository);

            if (subjectIds is not null)
                await ValidateSubjectsAsync(subjectIds, subjectRepository);
        }
    }
}