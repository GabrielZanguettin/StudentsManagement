using StudentsManagement.Dtos.Students;
using StudentsManagement.Entities;

namespace StudentsManagement.Appliers.Students
{
    public static class StudentPatchApplier
    {
        public static void ApplyStudentPatch(Student student, UpdateStudentDto dto)
        {
            if (dto.FirstName is not null)
                student.FirstName = dto.FirstName.Trim();

            if (dto.Email is not null)
                student.Email = dto.Email.Trim();

            if (dto.CourseId.HasValue)
                student.CourseId = dto.CourseId.Value;
        }
    }
}