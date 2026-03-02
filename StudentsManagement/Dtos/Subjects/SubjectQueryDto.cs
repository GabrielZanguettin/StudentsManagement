using StudentsManagement.Dtos.Pagination;

namespace StudentsManagement.Dtos.Subjects
{
    public class SubjectQueryDto : PaginationQueryDto
    {
        public string? Name { get; set; }
    }
}
