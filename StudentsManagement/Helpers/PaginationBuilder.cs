using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Helpers
{
    public static class PaginationBuilder
    {
        public static PaginationMeta CreateMeta(PaginationQuery query, int total)
        {
            var totalPages = (int)Math.Ceiling(total / (double)query.PageSize);

            return new PaginationMeta
            {
                Page = query.Page,
                PageSize = query.PageSize,
                Total = total,
                TotalPages = totalPages,
                HasPrevPage = query.Page > 1,
                HasNextPage = query.Page < totalPages
            };
        }

        public static PaginatedResponse<T> Create<T>(
            IReadOnlyList<T> items,
            PaginationQuery query,
            int total)
        {
            var meta = CreateMeta(query, total);
            return new PaginatedResponse<T>
            {
                Items = items,
                Meta = meta
            };
        }
    }
}
