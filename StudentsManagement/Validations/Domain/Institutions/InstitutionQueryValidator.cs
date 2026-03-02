using StudentsManagement.Dtos.Institutions;
using StudentsManagement.Validations.Domain.Institutions.Models;
using StudentsManagement.Validations.Pagination;

namespace StudentsManagement.Validations.Domain.Institutions
{
    public static class InstitutionQueryValidator
    {
        public static InstitutionQuery Normalize(InstitutionQueryDto dto)
        {
            var pagination = PaginationQueryValidator.Normalize(dto.Page, dto.PageSize, dto.Order);

            var name = dto.Name;
            if (string.IsNullOrWhiteSpace(name))
                name = null;
            else
                name = name.Trim();

            return new InstitutionQuery
            {
                Pagination = pagination,
                Name = name
            };
        }
    }
}
