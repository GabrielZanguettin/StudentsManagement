using StudentsManagement.Entities;
using StudentsManagement.Interfaces.Generic;
using StudentsManagement.Responses.Pagination;
using StudentsManagement.Validations.Domain.Subjects.Models;

namespace StudentsManagement.Interfaces.IRepositories
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        public Task<PaginatedResponse<Subject>> GetAllPaginated(SubjectQuery query);
        public Task<HashSet<Guid>> GetExistingIds(IEnumerable<Guid> ids);
    }
}