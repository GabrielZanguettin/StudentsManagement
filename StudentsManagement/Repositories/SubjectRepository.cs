using Microsoft.EntityFrameworkCore;
using StudentsManagement.Data;
using StudentsManagement.Entities;
using StudentsManagement.Interfaces.IRepositories;

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
