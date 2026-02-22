using StudentsManagement.Dtos.Institutions;

namespace StudentsManagement.Interfaces.IServices
{
    public interface IInstitutionService
    {
        public Task<List<InstitutionResponseDto>> GetAll();
        public Task<InstitutionResponseDto> GetById(Guid id);
        public Task<InstitutionResponseDto> Create(CreateInstitutionDto dto);
        public Task<InstitutionResponseDto> Update(Guid id, UpdateInstitutionDto dto);
        public Task<InstitutionResponseDto> Delete(Guid id);
    }
}
