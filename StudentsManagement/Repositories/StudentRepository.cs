using Microsoft.EntityFrameworkCore;
using StudentsManagement.Data;
using StudentsManagement.Entities;
using StudentsManagement.Interfaces.IRepositories;

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
