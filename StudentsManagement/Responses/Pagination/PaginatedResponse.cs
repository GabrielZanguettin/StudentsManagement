namespace StudentsManagement.Responses.Pagination
{
    public sealed record PaginatedResponse<T>
    {
        public IReadOnlyList<T> Items { get; init; }
        public PaginationMeta Meta { get; init; }
    }
}