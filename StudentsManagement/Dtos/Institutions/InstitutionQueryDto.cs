using StudentsManagement.Dtos.Pagination;

namespace StudentsManagement.Dtos.Institutions
{
    public class InstitutionQueryDto : PaginationQueryDto
    {
        public string? Name { get; set; }
    }
}
