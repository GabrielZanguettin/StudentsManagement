using StudentsManagement.Dtos.Courses;
using StudentsManagement.Entities;

namespace StudentsManagement.Appliers.Courses
{
    public static class CoursePatchApplier
    {
        public static void ApplyCoursePatch(Course course, UpdateCourseDto dto)
        {
            if (dto.Name is not null)
                course.Name = dto.Name.Trim();

            if (dto.InstitutionId.HasValue)
                course.InstitutionId = dto.InstitutionId.Value;
        }
    }
}