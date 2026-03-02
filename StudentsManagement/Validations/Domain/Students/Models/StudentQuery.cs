using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Validations.Domain.Students.Models
{
    public sealed record StudentQuery
    {
        public PaginationQuery Pagination { get; init; }
        public string? FirstName { get; init; }
        public string? Email { get; set; }
        public Guid? CourseId { get; set; }
    }
}
