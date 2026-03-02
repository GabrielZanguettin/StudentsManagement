namespace StudentsManagement.Interfaces.Generic
{
    public interface IRepository<TEntity>
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity?> GetById(Guid id);
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}