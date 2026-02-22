using StudentsManagement.Dtos.Courses;

namespace StudentsManagement.Interfaces.IServices
{
    public interface ICourseService
    {
        public Task<List<CourseResponseDto>> GetAll();
        public Task<CourseResponseDto> GetById(Guid id);
        public Task<CourseResponseDto> Create(CreateCourseDto dto);
        public Task<CourseResponseDto> Update(Guid id, UpdateCourseDto dto);
        public Task<CourseResponseDto> Delete(Guid id);
    }
}