using StudentsManagement.Entities;
using StudentsManagement.Interfaces.Generic;
using StudentsManagement.Responses.Pagination;
using StudentsManagement.Validations.Domain.Courses.Models;

namespace StudentsManagement.Interfaces.IRepositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        public Task<PaginatedResponse<Course>> GetAllPaginated(CourseQuery query);
        public Task SyncSubjects(Guid courseId, IEnumerable<Guid>? subjectIds);
    }
}