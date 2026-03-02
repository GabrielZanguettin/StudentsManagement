using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Validations.Domain.Institutions.Models
{
    public sealed record InstitutionQuery
    {
        public PaginationQuery Pagination { get; init; }
        public string? Name { get; init; }
    }
}
