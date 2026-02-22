using StudentsManagement.Dtos.Institutions;
using StudentsManagement.Entities;

namespace StudentsManagement.Mappings.Institutions
{
    public static class InstitutionMappings
    {
        public static InstitutionResponseDto ToInstitutionResponseDto(this Institution institution)
        {
            return new InstitutionResponseDto
            {
                Id = institution.Id,
                Name = institution.Name,
                Courses = institution.Courses
                    .Select(c => new InstitutionCourseItemDto
                    {
                        Id = c.Id,
                        Name = c.Name
                    })
                    .OrderBy(c => c.Name)
                    .ToList()
            };
        }
    }
}