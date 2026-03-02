namespace StudentsManagement.Dtos.Students
{
    public sealed record StudentResponseDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string Email { get; init; }
        public StudentCourseItemDto? Course { get; init; }
    }
    public sealed record StudentCourseItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}