using StudentsManagement.Dtos.Subjects;
using StudentsManagement.Entities;

namespace StudentsManagement.Mappings.Subjects
{
    public static class SubjectMappings
    {
        public static SubjectResponseDto ToSubjectResponseDto(this Subject subject)
        {
            return new SubjectResponseDto
            {
                Id = subject.Id,
                Name = subject.Name,
                Courses = subject.CourseSubjects
                    .Where(cs => cs.Course is not null)
                    .Select(cs => new SubjectCourseItemDto
                    {
                        Id = cs.Course.Id,
                        Name = cs.Course.Name
                    })
                    .OrderBy(c => c.Name)
                    .ToList()
            };
        }
    }
}