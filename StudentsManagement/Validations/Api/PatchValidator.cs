using StudentsManagement.Exceptions;

public static class PatchValidator
{
    public static void EnsureHasAny(params object?[] values)
    {
        if (!values.Any(v => v is not null))
            throw new BadRequestException("Informe ao menos um campo para atualizar.");
    }

    public static void EnsureNotBlank(string fieldName, string? value)
    {
        if (value is not null && string.IsNullOrWhiteSpace(value))
            throw new BadRequestException($"{fieldName} não pode ser vazio.");
    }

    public static void EnsureNotEmptyGuid(string fieldName, Guid? value)
    {
        if (value.HasValue && value.Value == Guid.Empty)
            throw new BadRequestException($"{fieldName} não pode ser Guid.Empty.");
    }

    public static void EnsureNoEmptyGuids(string fieldName, IEnumerable<Guid>? values)
    {
        if (values is null) return;

        if (values.Any(v => v == Guid.Empty))
            throw new BadRequestException($"{fieldName} não pode conter GUID vazio.");
    }
}