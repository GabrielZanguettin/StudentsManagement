using StudentsManagement.Dtos.Students;
using StudentsManagement.Entities;

namespace StudentsManagement.Mappings.Students
{
    public static class StudentMappings
    {
        public static StudentResponseDto ToStudentResponseDto(this Student student)
        {
            return new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                Email = student.Email,
                Course = student.Course is null
                    ? null
                    : new StudentCourseItemDto
                    {
                        Id = student.Course.Id,
                        Name = student.Course.Name
                    }
            };
        }
    }
}