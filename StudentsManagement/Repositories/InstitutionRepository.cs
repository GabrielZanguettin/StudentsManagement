using Microsoft.EntityFrameworkCore;
using StudentsManagement.Data;
using StudentsManagement.Dtos.Pagination;
using StudentsManagement.Entities;
using StudentsManagement.Helpers;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Responses.Pagination;
using StudentsManagement.Validations.Domain.Institutions.Models;

namespace StudentsManagement.Repositories
{
    public class InstitutionRepository : IInstitutionRepository
    {
        private readonly DataContext _context;

        public InstitutionRepository(DataContext context)
        {
            this._context = context;
        }

        public Task<List<Institution>> GetAll()
        {
            return this._context.Institutions
                .AsNoTracking()
                .Include(i => i.Courses)
                .OrderBy(i => i.Name)
                .ToListAsync();
        }

        public async Task<PaginatedResponse<Institution>> GetAllPaginated(InstitutionQuery query)
        {
            var baseQuery = _context.Institutions.AsNoTracking().AsQueryable();

            if (query.Name is not null)
            {
                baseQuery = baseQuery.Where(i => i.Name.Contains(query.Name));
            }

            var total = await baseQuery.CountAsync();

            baseQuery = query.Pagination.Order == SortOrder.Asc
                ? baseQuery.OrderBy(i => i.Name)
                : baseQuery.OrderByDescending(i => i.Name);

            var skip = (query.Pagination.Page - 1) * query.Pagination.PageSize;

            var data = await baseQuery
                .Skip(skip)
                .Take(query.Pagination.PageSize)
                .Include(i => i.Courses)
                .ToListAsync();

            return PaginationBuilder.Create(data, query.Pagination, total);
        }

        public Task<Institution?> GetById(Guid id)
        {
            return this._context.Institutions
                .AsNoTracking()
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Institution> Create(Institution institution)
        {
            await this._context.Institutions.AddAsync(institution);
            await this._context.SaveChangesAsync();
            return institution;
        }

        public async Task<Institution> Update(Institution institution)
        {
            this._context.Institutions.Update(institution);
            await this._context.SaveChangesAsync();
            return institution;
        }

        public async Task Delete(Institution institution)
        {
            this._context.Institutions.Remove(institution);
            await this._context.SaveChangesAsync();
        }
    }
}
