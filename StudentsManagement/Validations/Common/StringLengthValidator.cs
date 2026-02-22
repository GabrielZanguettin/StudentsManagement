using StudentsManagement.Exceptions;

namespace StudentsManagement.Validations.Common
{
    public static class StringLengthValidator
    {
        public static void ValidateMax(string fieldName, string value, int maxLength)
        {
            var trimmed = value.Trim();

            if (trimmed.Length > maxLength)
                throw new BadRequestException($"{fieldName} deve ter no máximo {maxLength} caracteres.");
        }

        public static void ValidateMaxIfProvided(string fieldName, string? value, int maxLength)
        {
            if (value is null)
                return;

            ValidateMax(fieldName, value, maxLength);
        }
    }
}