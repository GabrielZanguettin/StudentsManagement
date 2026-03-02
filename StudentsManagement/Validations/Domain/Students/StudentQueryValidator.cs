using StudentsManagement.Dtos.Students;
using StudentsManagement.Validations.Domain.Students.Models;
using StudentsManagement.Validations.Pagination;

namespace StudentsManagement.Validations.Domain.Students
{
    public static class StudentQueryValidator
    {
        public static StudentQuery Normalize(StudentQueryDto dto)
        {
            var pagination = PaginationQueryValidator.Normalize(dto.Page, dto.PageSize, dto.Order);

            var firstName = dto.FirstName;
            if (string.IsNullOrWhiteSpace(firstName))
                firstName = null;
            else
                firstName = firstName.Trim();

            var email = dto.Email;
            if (string.IsNullOrWhiteSpace(email))
                email = null;
            else
                email = email.Trim();

            var courseId = dto.CourseId;
            if (courseId.HasValue && courseId.Value == Guid.Empty)
                courseId = null;

            return new StudentQuery
            {
                Pagination = pagination,
                FirstName = firstName,
                Email = email,
                CourseId = courseId
            };
        }
    }
}
