using StudentsManagement.Entities;

namespace StudentsManagement.Interfaces.IRepositories
{
    public interface ISubjectRepository
    {
        public Task<List<Subject>> GetAll();
        public Task<Subject?> GetById(Guid id);
        public Task<HashSet<Guid>> GetExistingIds(IEnumerable<Guid> ids);
        public Task<Subject> Create(Subject subject);
        public Task<Subject> Update(Subject subject);
        public Task Delete(Subject subject);
    }
}
