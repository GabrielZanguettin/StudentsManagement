using Microsoft.EntityFrameworkCore;
using StudentsManagement.Data;
using StudentsManagement.Entities;
using StudentsManagement.Interfaces.IRepositories;

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
