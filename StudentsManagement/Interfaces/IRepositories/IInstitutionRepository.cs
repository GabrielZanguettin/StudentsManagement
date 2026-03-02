using StudentsManagement.Entities;
using StudentsManagement.Interfaces.Generic;
using StudentsManagement.Responses.Pagination;
using StudentsManagement.Validations.Domain.Institutions.Models;

namespace StudentsManagement.Interfaces.IRepositories
{
    public interface IInstitutionRepository : IRepository<Institution> 
    {
        public Task<PaginatedResponse<Institution>> GetAllPaginated(InstitutionQuery query);
    }
}