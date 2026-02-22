using StudentsManagement.Entities;
using StudentsManagement.Exceptions;
using StudentsManagement.Extensions.Common;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Validations.Common;

namespace StudentsManagement.Validations.Domain.Students
{
    public static class StudentValidator
    {
        public const int FirstNameMaxLength = 50;
        public const string RequiredEmailDomain = "@faculdade.edu";

        public static void ValidateFirstName(string firstName)
        {
            var trimmed = firstName.Trim();

            StringLengthValidator.ValidateMax(
                nameof(Student.FirstName),
                trimmed,
                FirstNameMaxLength
            );
        }

        public static void ValidateEmail(string email)
        {
            var trimmed = email.Trim();

            if (!trimmed.EndsWith(RequiredEmailDomain, StringComparison.OrdinalIgnoreCase))
                throw new BadRequestException(
                    $"O e-mail deve terminar com {RequiredEmailDomain}."
                );
        }

        public static async Task ValidateEmailUniquenessAsync(
            string email,
            IStudentRepository studentRepository)
        {
            var trimmedEmail = email.Trim();

            var existingStudent = await studentRepository.GetByEmail(trimmedEmail);

            if (existingStudent is not null)
                throw new BadRequestException("Este e-mail já está cadastrado.");
        }

        public static async Task ValidateCourseAsync(Guid courseId, ICourseRepository courseRepository)
        {
            var course = await courseRepository.GetById(courseId);

            if (course is null)
                throw NotFoundException.For(EntityName.Course, courseId);
        }

        public static async Task ValidateCreateAsync(
            string firstName,
            string email,
            Guid courseId,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository)
        {
            ValidateFirstName(firstName);
            ValidateEmail(email);
            await ValidateEmailUniquenessAsync(email, studentRepository);
            await ValidateCourseAsync(courseId, courseRepository);
        }

        public static async Task ValidateUpdateAsync(
            string? firstName,
            string? email,
            Guid? courseId,
            ICourseRepository courseRepository,
            IStudentRepository studentRepository)
        {
            if (firstName is not null)
                ValidateFirstName(firstName);

            if (email is not null)
            {
                ValidateEmail(email);
                await ValidateEmailUniquenessAsync(email, studentRepository);
            }

            if (courseId.HasValue)
                await ValidateCourseAsync(courseId.Value, courseRepository);
        }
    }
}