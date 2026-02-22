namespace StudentsManagement.Dtos.Courses
{
    public sealed record CourseResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public Guid InstitutionId { get; init; }
        public List<CourseSubjectItemDto> Subjects { get; init; }
    }

    public sealed record CourseSubjectItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}