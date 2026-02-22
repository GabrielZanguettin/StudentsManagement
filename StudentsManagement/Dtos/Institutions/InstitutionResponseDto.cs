namespace StudentsManagement.Dtos.Institutions
{
    public sealed record InstitutionResponseDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public List<InstitutionCourseItemDto> Courses { get; init; }
    }
    public sealed record InstitutionCourseItemDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
    }
}