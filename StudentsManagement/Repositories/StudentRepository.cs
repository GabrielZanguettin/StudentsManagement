using Microsoft.EntityFrameworkCore;
using StudentsManagement.Data;
using StudentsManagement.Dtos.Pagination;
using StudentsManagement.Entities;
using StudentsManagement.Helpers;
using StudentsManagement.Interfaces.IRepositories;
using StudentsManagement.Responses.Pagination;
using StudentsManagement.Validations.Domain.Students.Models;

namespace StudentsManagement.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            this._context = context;
        }

        public Task<List<Student>> GetAll()
        {
            return this._context.Students
                .AsNoTracking()
                .Include(s => s.Course)
                .OrderBy(s => s.FirstName)
                .ToListAsync();
        }

        public async Task<PaginatedResponse<Student>> GetAllPaginated(StudentQuery query)
        {
            var baseQuery = _context.Students.AsNoTracking().AsQueryable();

            if (query.FirstName is not null)
            {
                baseQuery = baseQuery.Where(s => s.FirstName.Contains(query.FirstName));
            }

            if (query.Email is not null)
            {
                baseQuery = baseQuery.Where(s => s.Email.Contains(query.Email));
            }

            if (query.CourseId is not null)
                baseQuery = baseQuery.Where(s => s.CourseId == query.CourseId.Value);

            var total = await baseQuery.CountAsync();

            baseQuery = query.Pagination.Order == SortOrder.Asc
                ? baseQuery.OrderBy(s => s.FirstName)
                : baseQuery.OrderByDescending(s => s.FirstName);

            var skip = (query.Pagination.Page - 1) * query.Pagination.PageSize;

            var data = await baseQuery
                .Skip(skip)
                .Take(query.Pagination.PageSize)
                .Include(s => s.Course)
                .ToListAsync();

            return PaginationBuilder.Create(data, query.Pagination, total);
        }

        public Task<Student?> GetById(Guid id)
        {
            return this._context.Students
                .AsNoTracking()
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<Student?> GetByEmail(string email)
        {
            return this._context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<Student> Create(Student student)
        {
            await this._context.Students.AddAsync(student);
            await this._context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> Update(Student student)
        {
            this._context.Students.Update(student);
            await this._context.SaveChangesAsync();
            return student;
        }

        public async Task Delete(Student student)
        {
            this._context.Students.Remove(student);
            await this._context.SaveChangesAsync();
        }
    }
}
