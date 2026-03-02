using StudentsManagement.Dtos.Subjects;
using StudentsManagement.Interfaces.Generic;
using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Interfaces.IServices
{
    public interface ISubjectService : IService<SubjectResponseDto, CreateSubjectDto, UpdateSubjectDto> 
    {
        public Task<PaginatedResponse<SubjectResponseDto>> GetAllPaginated(SubjectQueryDto dto);
    }
}