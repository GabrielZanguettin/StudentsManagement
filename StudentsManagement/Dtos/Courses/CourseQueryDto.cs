using StudentsManagement.Dtos.Pagination;

namespace StudentsManagement.Dtos.Courses
{
    public class CourseQueryDto : PaginationQueryDto
    {
        public string? Name { get; set; }
        public Guid? InstitutionId { get; set; }
    }
}