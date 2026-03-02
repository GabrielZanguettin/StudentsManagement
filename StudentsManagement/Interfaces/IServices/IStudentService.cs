using StudentsManagement.Dtos.Students;
using StudentsManagement.Interfaces.Generic;
using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Interfaces.IServices
{
    public interface IStudentService : IService<StudentResponseDto, CreateStudentDto, UpdateStudentDto> 
    {
        public Task<PaginatedResponse<StudentResponseDto>> GetAllPaginated(StudentQueryDto dto);
    }
}