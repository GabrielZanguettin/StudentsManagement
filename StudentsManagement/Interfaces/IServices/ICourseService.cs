using StudentsManagement.Dtos.Courses;
using StudentsManagement.Interfaces.Generic;
using StudentsManagement.Responses.Pagination;

namespace StudentsManagement.Interfaces.IServices
{
    public interface ICourseService : IService<CourseResponseDto, CreateCourseDto, UpdateCourseDto> 
    {
        public Task<PaginatedResponse<CourseResponseDto>> GetAllPaginated(CourseQueryDto dto);
    }
}