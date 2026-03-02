using StudentsManagement.Dtos.Pagination;
using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Validations.Pagination
{
    public static class PaginationQueryValidator
    {
        private const int DefaultPage = 1;
        private const int DefaultPageSize = 10;
        private const int MaxPageSize = 100;

        public static PaginationQuery Normalize(int page, int pageSize, SortOrder order)
        {
            if (page <= 0) page = DefaultPage;
            if (pageSize <= 0) pageSize = DefaultPageSize;
            if (pageSize > MaxPageSize) pageSize = MaxPageSize;

            return new PaginationQuery
            {
                Page = page,
                PageSize = pageSize,
                Order = order
            };
        }
    }
}