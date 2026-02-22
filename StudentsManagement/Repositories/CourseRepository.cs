using Microsoft.EntityFrameworkCore;
using StudentsManagement.Data;
using StudentsManagement.Entities;
using StudentsManagement.Interfaces.IRepositories;

namespace StudentsManagement.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            this._context = context;
        }

        public Task<List<Course>> GetAll()
        {
            return _context.Courses
                .AsNoTracking()
                .Include(c => c.CourseSubjects)
                    .ThenInclude(cs => cs.Subject)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public Task<Course?> GetById(Guid id)
        {
            return _context.Courses
                .AsNoTracking()
                .Include(c => c.CourseSubjects)
                    .ThenInclude(cs => cs.Subject)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course> Create(Course course)
        {
            await this._context.Courses.AddAsync(course);
            await this._context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> Update(Course course)
        {
            this._context.Courses.Update(course);
            await this._context.SaveChangesAsync();
            return course;
        }

        public async Task Delete(Course course)
        {
            this._context.Courses.Remove(course);
            await this._context.SaveChangesAsync();
        }

        public async Task SyncSubjects(Guid courseId, IEnumerable<Guid>? subjectIds)
        {
            if (subjectIds is null) return;

            var newIds = NormalizeSubjectIds(subjectIds);

            var currentLinks = await GetCourseSubjectLinks(courseId);
            var currentIds = GetCurrentSubjectIds(currentLinks);

            var linksToRemove = GetLinksToRemove(currentLinks, newIds);
            var linksToAdd = GetLinksToAdd(courseId, newIds, currentIds);

            ApplyCourseSubjectChanges(linksToRemove, linksToAdd);

            await this._context.SaveChangesAsync();
        }

        private static HashSet<Guid> NormalizeSubjectIds(IEnumerable<Guid> subjectIds)
        {
            return subjectIds.ToHashSet();
        }

        private async Task<List<CourseSubject>> GetCourseSubjectLinks(Guid courseId)
        {
            return await this._context.CourseSubjects
                .Where(cs => cs.CourseId == courseId)
                .ToListAsync();
        }

        private static HashSet<Guid> GetCurrentSubjectIds(IEnumerable<CourseSubject> currentLinks)
        {
            return currentLinks
                .Select(cs => cs.SubjectId)
                .ToHashSet();
        }

        private static List<CourseSubject> GetLinksToRemove(
            IEnumerable<CourseSubject> currentLinks,
            HashSet<Guid> newIds)
        {
            return currentLinks
                .Where(cs => !newIds.Contains(cs.SubjectId))
                .ToList();
        }

        private static List<CourseSubject> GetLinksToAdd(
            Guid courseId,
            HashSet<Guid> newIds,
            HashSet<Guid> currentIds)
        {
            return newIds
                .Where(id => !currentIds.Contains(id))
                .Select(id => new CourseSubject
                {
                    CourseId = courseId,
                    SubjectId = id
                })
                .ToList();
        }

        private void ApplyCourseSubjectChanges(
            List<CourseSubject> linksToRemove,
            List<CourseSubject> linksToAdd)
        {
            if (linksToRemove.Count > 0)
                this._context.CourseSubjects.RemoveRange(linksToRemove);

            if (linksToAdd.Count > 0)
                this._context.CourseSubjects.AddRange(linksToAdd);
        }
    }
}
