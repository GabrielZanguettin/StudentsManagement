using StudentsManagement.Entities;

namespace StudentsManagement.Interfaces.IRepositories
{
    public interface ICourseRepository
    {
        public Task<List<Course>> GetAll();
        public Task<Course?> GetById(Guid id);
        public Task<Course> Create(Course course);
        public Task<Course> Update(Course course);
        public Task SyncSubjects(Guid courseId, IEnumerable<Guid>? subjectIds);
        public Task Delete(Course course);
    }
}