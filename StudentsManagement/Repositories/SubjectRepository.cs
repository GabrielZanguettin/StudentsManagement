using Microsoft.EntityFrameworkCore;
using StudentsManagement.Data;
using StudentsManagement.Dtos.Pagination;
using StudentsManagement.Entities;
using StudentsManagement.Helpers;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Responses.Pagination;
using StudentsManagement.Validations.Domain.Subjects.Models;

namespace StudentsManagement.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            this._context = context;
        }

        public Task<List<Subject>> GetAll()
        {
            return this._context.Subjects
                .AsNoTracking()
                .Include(s => s.CourseSubjects)
                    .ThenInclude(cs => cs.Course)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<PaginatedResponse<Subject>> GetAllPaginated(SubjectQuery query)
        {
            var baseQuery = _context.Subjects.AsNoTracking().AsQueryable();

            if (query.Name is not null)
            {
                baseQuery = baseQuery.Where(s => s.Name.Contains(query.Name));
            }

            var total = await baseQuery.CountAsync();

            baseQuery = query.Pagination.Order == SortOrder.Asc
                ? baseQuery.OrderBy(s => s.Name)
                : baseQuery.OrderByDescending(s => s.Name);

            var skip = (query.Pagination.Page - 1) * query.Pagination.PageSize;

            var data = await baseQuery
                .Skip(skip)
                .Take(query.Pagination.PageSize)
                .Include(s => s.CourseSubjects)
                    .ThenInclude(cs => cs.Course)
                .ToListAsync();

            return PaginationBuilder.Create(data, query.Pagination, total);
        }

        public Task<Subject?> GetById(Guid id)
        {
            return this._context.Subjects
                .AsNoTracking()
                .Include(s => s.CourseSubjects)
                    .ThenInclude(cs => cs.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<HashSet<Guid>> GetExistingIds(IEnumerable<Guid> ids)
        {
            var normalizedIds = ids.Distinct().ToList();

            if (normalizedIds.Count == 0)
                return new HashSet<Guid>();

            var existingIds = await this._context.Subjects
                .AsNoTracking()
                .Where(s => normalizedIds.Contains(s.Id))
                .Select(s => s.Id)
                .ToListAsync();

            return existingIds.ToHashSet();
        }

        public async Task<Subject> Create(Subject subject)
        {
            await this._context.Subjects.AddAsync(subject);
            await this._context.SaveChangesAsync();
            return subject;
        }

        public async Task<Subject> Update(Subject subject)
        {
            this._context.Subjects.Update(subject);
            await this._context.SaveChangesAsync();
            return subject;
        }

        public async Task Delete(Subject subject)
        {
            this._context.Subjects.Remove(subject);
            await this._context.SaveChangesAsync();
        }
    }
}
