using StudentsManagement.Dtos.Subjects;
using StudentsManagement.Validations.Domain.Subjects.Models;
using StudentsManagement.Validations.Pagination;

namespace StudentsManagement.Validations.Domain.Subjects
{
    public static class SubjectQueryValidator
    {
        public static SubjectQuery Normalize(SubjectQueryDto dto)
        {
            var pagination = PaginationQueryValidator.Normalize(dto.Page, dto.PageSize, dto.Order);

            var name = dto.Name;
            if (string.IsNullOrWhiteSpace(name))
                name = null;
            else
                name = name.Trim();

            return new SubjectQuery
            {
                Pagination = pagination,
                Name = name
            };
        }
    }
}
