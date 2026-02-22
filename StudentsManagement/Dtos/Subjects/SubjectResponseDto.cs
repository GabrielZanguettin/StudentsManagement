namespace StudentsManagement.Dtos.Subjects
{
    public sealed record SubjectResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<SubjectCourseItemDto> Courses { get; init; }
    }
    public sealed record SubjectCourseItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}