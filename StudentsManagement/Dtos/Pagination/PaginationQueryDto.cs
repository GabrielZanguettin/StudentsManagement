namespace StudentsManagement.Dtos.Pagination
{
    public class PaginationQueryDto
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public SortOrder Order { get; set; }
    }

    public enum SortOrder
    {
        Asc,
        Desc
    }
}