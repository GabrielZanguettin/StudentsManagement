using StudentsManagement.Entities;
using StudentsManagement.Validations.Common;

namespace StudentsManagement.Validations.Domain.Institutions
{
    public static class InstitutionValidator
    {
        public const int NameMaxLength = 150;

        public static void ValidateName(string name)
        {
            var trimmed = name.Trim();

            StringLengthValidator.ValidateMax(
                nameof(Institution.Name),
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