using StudentsManagement.Dtos.Pagination;

namespace StudentsManagement.Dtos.Students
{
    public class StudentQueryDto : PaginationQueryDto
    {
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public Guid? CourseId { get; set; }
    }
}
