using StudentsManagement.Dtos.Courses;
using StudentsManagement.Validations.Domain.Courses.Models;
using StudentsManagement.Validations.Pagination;

namespace StudentsManagement.Validations.Domain.Courses
{
    public static class CourseQueryValidator
    {
        public static CourseQuery Normalize(CourseQueryDto dto)
        {
            var pagination = PaginationQueryValidator.Normalize(dto.Page, dto.PageSize, dto.Order);

            var name = dto.Name;
            if (string.IsNullOrWhiteSpace(name))
                name = null;
            else
                name = name.Trim();

            var institutionId = dto.InstitutionId;
            if (institutionId.HasValue && institutionId.Value == Guid.Empty)
                institutionId = null;

            return new CourseQuery
            {
                Pagination = pagination,
                Name = name,
                InstitutionId = institutionId
            };
        }
    }
}
