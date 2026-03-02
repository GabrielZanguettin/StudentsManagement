namespace StudentsManagement.Dtos.Courses
{
    public sealed record UpdateCourseDto
    {
        public string? Name { get; init; }
        public Guid? InstitutionId { get; init; }
        public IReadOnlyList<Guid>? SubjectIds { get; init; }
    }
}