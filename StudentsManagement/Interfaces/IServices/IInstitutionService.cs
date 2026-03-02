using StudentsManagement.Dtos.Institutions;
using StudentsManagement.Interfaces.Generic;
using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Interfaces.IServices
{
    public interface IInstitutionService : IService<InstitutionResponseDto, CreateInstitutionDto, UpdateInstitutionDto> 
    {
        public Task<PaginatedResponse<InstitutionResponseDto>> GetAllPaginated(InstitutionQueryDto dto);
    }
}