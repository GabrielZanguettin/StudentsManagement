using StudentsManagement.Dtos.Courses;
using StudentsManagement.Entities;

namespace StudentsManagement.Mappings.Courses
{
    public static class CourseMappings
    {
        public static CourseResponseDto ToCourseResponseDto(this Course course)
        {
            return new CourseResponseDto
            {
                Id = course.Id,
                Name = course.Name,
                InstitutionId = course.InstitutionId,
                Subjects = course.CourseSubjects
                    .Where(cs => cs.Subject is not null)
                    .Select(cs => new CourseSubjectItemDto
                    {
                        Id = cs.Subject.Id,
                        Name = cs.Subject.Name
                    })
                    .OrderBy(s => s.Name)
                    .ToList()
            };
        }
    }
}