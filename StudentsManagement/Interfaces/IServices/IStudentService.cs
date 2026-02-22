using StudentsManagement.Dtos.Students;

namespace StudentsManagement.Interfaces.IServices
{
    public interface IStudentService
    {
        public Task<List<StudentResponseDto>> GetAll();
        public Task<StudentResponseDto> GetById(Guid id);
        public Task<StudentResponseDto> Create(CreateStudentDto dto);
        public Task<StudentResponseDto> Update(Guid id, UpdateStudentDto dto);
        public Task<StudentResponseDto> Delete(Guid id);
    }
}
