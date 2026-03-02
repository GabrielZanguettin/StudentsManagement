using StudentsManagement.Entities;
using StudentsManagement.Interfaces.Generic;
using StudentsManagement.Responses.Pagination;
using StudentsManagement.Validations.Domain.Students.Models;

namespace StudentsManagement.Interfaces.IRepositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        public Task<PaginatedResponse<Student>> GetAllPaginated(StudentQuery query);
        public Task<Student?> GetByEmail(string email);
    }
}