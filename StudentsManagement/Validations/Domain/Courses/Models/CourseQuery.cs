using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Validations.Domain.Courses.Models
{
    public sealed record CourseQuery
    {
        public PaginationQuery Pagination { get; init; }
        public string? Name { get; init; }
        public Guid? InstitutionId { get; init; }
    }
}