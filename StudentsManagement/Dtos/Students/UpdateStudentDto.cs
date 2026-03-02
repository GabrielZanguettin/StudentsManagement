namespace StudentsManagement.Dtos.Students
{
    public sealed record UpdateStudentDto
    {
        public string? FirstName { get; init; }
        public string? Email { get; init; }
        public Guid? CourseId { get; set; }
    }
}
