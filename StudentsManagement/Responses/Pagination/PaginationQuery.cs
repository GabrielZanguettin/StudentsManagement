using StudentsManagement.Dtos.Pagination;

namespace StudentsManagement.Responses.Pagination
{
    public sealed record PaginationQuery
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public SortOrder Order { get; init; }
    }
}