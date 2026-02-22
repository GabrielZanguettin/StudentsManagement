using StudentsManagement.Entities;

namespace StudentsManagement.Interfaces.IRepositories
{
    public interface IInstitutionRepository
    {
        public Task<List<Institution>> GetAll();
        public Task<Institution?> GetById(Guid id);
        public Task<Institution> Create(Institution institution);
        public Task<Institution> Update(Institution institution);
        public Task Delete(Institution institution);
    }
}
