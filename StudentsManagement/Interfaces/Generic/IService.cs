namespace StudentsManagement.Interfaces.Generic
{
    public interface IService<TResponseDto, TCreateDto, TUpdateDto>
    {
        public Task<List<TResponseDto>> GetAll();
        public Task<TResponseDto> GetById(Guid id);
        public Task<TResponseDto> Create(TCreateDto dto);
        public Task<TResponseDto> Update(Guid id, TUpdateDto dto);
        public Task<TResponseDto> Delete(Guid id);
    }
}