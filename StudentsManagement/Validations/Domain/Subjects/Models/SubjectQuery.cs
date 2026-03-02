using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Validations.Domain.Subjects.Models
{
    public sealed record SubjectQuery
    {
        public PaginationQuery Pagination { get; init; }
        public string? Name { get; init; }
    }
}
