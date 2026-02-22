using StudentsManagement.Entities;
using StudentsManagement.Validations.Common;

namespace StudentsManagement.Validations.Domain.Subjects
{
    public static class SubjectValidator
    {
        public const int NameMaxLength = 150;

        public static void ValidateName(string name)
        {
            var trimmed = name.Trim();

            StringLengthValidator.ValidateMax(
                nameof(Subject.Name),
                trimmed,
                NameMaxLength
            );
        }

        public static void ValidateCreate(string name)
        {
            ValidateName(name);
        }

        public static void ValidateUpdate(string? name)
        {
            if (name is not null)
                ValidateName(name);
        }
    }
}
