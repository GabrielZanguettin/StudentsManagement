using StudentsManagement.Dtos.Subjects;
using StudentsManagement.Entities;

namespace StudentsManagement.Appliers.Subjects
{
    public static class SubjectPatchApplier
    {
        public static void ApplySubjectPatch(Subject subject, UpdateSubjectDto dto)
        {
            if (dto.Name is not null)
                subject.Name = dto.Name.Trim();
        }
    }
}