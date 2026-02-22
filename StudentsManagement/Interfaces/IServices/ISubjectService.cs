using StudentsManagement.Dtos.Subjects;

namespace StudentsManagement.Interfaces.IServices
{
    public interface ISubjectService
    {
        public Task<List<SubjectResponseDto>> GetAll();
        public Task<SubjectResponseDto> GetById(Guid id);
        public Task<SubjectResponseDto> Create(CreateSubjectDto dto);
        public Task<SubjectResponseDto> Update(Guid id, UpdateSubjectDto dto);
        public Task<SubjectResponseDto> Delete(Guid id);
    }
}
