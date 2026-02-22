using StudentsManagement.Dtos.Institutions;
using StudentsManagement.Entities;

namespace StudentsManagement.Appliers.Institutions
{
    public static class InstitutionPatchApplier
    {
        public static void ApplyInstitutionPatch(Institution institution, UpdateInstitutionDto dto)
        {
            if (dto.Name is not null)
                institution.Name = dto.Name.Trim();
        }
    }
}