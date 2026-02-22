using StudentsManagement.Entities;

namespace StudentsManagement.Interfaces.IRepositories
{
    public interface IStudentRepository
    {
        public Task<List<Student>> GetAll();
        public Task<Student?> GetById(Guid id);
        public Task<Student?> GetByEmail(string email);
        public Task<Student> Create(Student student);
        public Task<Student> Update(Student student);
        public Task Delete(Student student);
    }
}
