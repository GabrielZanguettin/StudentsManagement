namespace StudentsManagement.Responses.Pagination
{
    public sealed record PaginationMeta
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int Total { get; init; }
        public int TotalPages { get; init; }
        public bool HasNextPage { get; init; }
        public bool HasPrevPage { get; init; }
    }
}